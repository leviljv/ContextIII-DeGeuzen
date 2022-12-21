using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour {
    [Header("Reference")]
    public GameObject DialogueSystemObject;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI nameText;
    public Image Portrait;
    public GameObject buttonContainer;
    public GameObject buttonPanel;
    public GameObject buttonPrefab;

    [Header("CommandSettings")]
    public char CommandChar;
    public char OptionChar;
    public char SectionChar;
    public char AutoNextChar;

    [Header("Visual Settings")]
    public float TimeBetweenChars;

    private DialogFunctionality funcs = new();
    private Dictionary<string, string[]> Files = new();
    private List<char> Commands = new();
    private string[] currentDialog;
    private int index;

    void Awake() {
        funcs.Owner = this;
        funcs.Init();

        var DialogFiles = Resources.LoadAll<TextAsset>("DialogFiles/");

        if (DialogFiles.Length < 1) {
            Debug.LogError("No Files");
            return;
        }

        foreach (var item in DialogFiles) {
            Files.Add(item.name, PrepFile(item));
        }

        Commands.Add(CommandChar);
        Commands.Add(OptionChar);
    }

    private void OnEnable() {
        EventManager<string>.Subscribe(EventType.ON_DIALOG_STARTED, SetDialog);
    }
    private void OnDisable() {
        EventManager<string>.Unsubscribe(EventType.ON_DIALOG_STARTED, SetDialog);
    }

    private string[] PrepFile(TextAsset file) {
        return file.ToString().Replace("\n\r\n", "\n").Split("\n");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
            NextLine();
        }
    }

    private void SetDialog(string DialogName) {
        if (Files.ContainsKey(DialogName)) {
            index = 0;
            currentDialog = Files[DialogName];
            DialogueSystemObject.SetActive(true);
            EventManager<bool>.Invoke(EventType.SET_INTERACTION_STATE, true);
            NextLine();
        }
        else
            Debug.LogError("No File named " + DialogName + " found!");
    }

    private void NextLine() {
        if (currentDialog == null)
            return;

        if (currentDialog.Length - 1 < index) {
            currentDialog = null;
            mainText.text = "";
            nameText.text = "";
            DialogueSystemObject.SetActive(false);
            EventManager<bool>.Invoke(EventType.SET_INTERACTION_STATE, false);
            index = 0;
            return;
        }

        TimeBetweenChars = .05f;

        var command = CheckCommand(currentDialog[index], CommandChar);
        if (command != null) {
            CallCommand(command);

            index++;
            NextLine();
            return;
        }

        var option = CheckCommand(currentDialog[index], OptionChar);
        if (option != null) {
            DisplayOptions(currentDialog);
            return;
        }

        var section = CheckCommand(currentDialog[index], SectionChar);
        if (section != null) {
            var line = currentDialog[index].Trim().Split(" ");

            if (line[1] == "jump") {
                JumpToSection(line[2]);
                return;
            }

            if(line[1] == "end") 
                return;

            index++;
            NextLine();
            return;
        }

        DisplayLine();

        index++;
    }

    private void DisplayLine() {
        StopAllCoroutines();
        StartCoroutine(DisplayText(currentDialog[index].Trim()));
    }

    private string[] CheckCommand(string line, char commandChar) {
        var tmp = line.Trim().ToCharArray();

        if (tmp.Length < 1)
            return null;

        if (tmp[0] == commandChar) {
            return line.Split(" ");
        }

        return null;
    }

    private void CallCommand(string[] command) {
        if (float.TryParse(command[2], out var floatParse))
            EventManager<float>.Invoke(ParseEnum<EventType>(command[1]), floatParse);
        else if (bool.TryParse(command[2], out var boolParse))
            EventManager<bool>.Invoke(ParseEnum<EventType>(command[1]), boolParse);
        else 
            EventManager<string>.Invoke(ParseEnum<EventType>(command[1]), command[2].Trim());
    }

    private void DisplayOptions(string[] file) {
        buttonPanel.SetActive(true);

        List<GameObject> buttons = new();

        while (file[index].Trim().ToCharArray()[0] == '@') {
            var tmpButton = Instantiate(buttonPrefab, buttonContainer.transform);
            var text = file[index].Trim().Split(" ", 2)[1];
            tmpButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;

            index++;
            
            if (CheckCommand(file[index], SectionChar) != null) {
                if (file[index].Trim().Split(" ")[1] == "jump") {
                    string sectionName = file[index].Trim().Split(" ")[2];
                    tmpButton.GetComponent<Button>().onClick.AddListener(() => JumpToSection(sectionName));
                    index++;
                }
            }

            buttons.Add(tmpButton);
        }
        
        for (int i = 0; i < buttons.Count; i++) {
            var rect = buttons[i].GetComponent<RectTransform>();
            var containerRect = buttonContainer.GetComponent<RectTransform>();

            if (buttons.Count % 2 != 0)
                rect.localPosition = new Vector2(0, (containerRect.sizeDelta.y / buttons.Count) * ((buttons.Count - 1 - i) - (buttons.Count) / 2));
            else
                rect.localPosition = new Vector2(0, (containerRect.sizeDelta.y / buttons.Count) * ((buttons.Count - 1 - i) - (buttons.Count) / 2) + (containerRect.sizeDelta.y / 2) / buttons.Count);

            rect.sizeDelta = new Vector2(rect.sizeDelta.x, (containerRect.sizeDelta.y / buttons.Count - 1));
        }
    }

    private void RemoveOptions() {
        buttonPanel.SetActive(false);

        for (int i = buttonContainer.transform.childCount - 1; i >= 0; i--) {
            Destroy(buttonContainer.transform.GetChild(i).gameObject);
        }
    }

    private void JumpToSection(string SectionName) {
        RemoveOptions();

        for (int i = 0; i < currentDialog.Length; i++) {
            if (CheckCommand(currentDialog[i], SectionChar) != null) {
                var line = currentDialog[i].Trim().Split(" ");
                if (line[1] == "start" && line[2] == SectionName) {
                    index = i;
                    NextLine();
                    return;
                }
            }
        }
    }

    private IEnumerator DisplayText(string text) {
        List<char> charList = new();

        var frontAndBack = text.Split(": ", 2);
        var name = frontAndBack[0];
        nameText.text = name;
        var sentence = frontAndBack[1];

        for (int i = 0; i < sentence.Length; i++) {
            if(sentence[i] == CommandChar) {
                List<char> command = new();

                command.Add(sentence[i]);
                i++;

                while (sentence[i] != CommandChar) {
                    command.Add(sentence[i]);
                    i++;
                }

                command.Add(sentence[i]);
                i += 2;

                CallCommand(new string(command.ToArray()).Split(" "));
            }

            if(sentence[i] == '<') {
                List<char> stylePartOne = new();
                List<char> TextBetween = new();
                List<char> stylePartTwo = new();

                stylePartOne.Add(sentence[i]);
                i++;
                while (sentence[i] != '>') {
                    stylePartOne.Add(sentence[i]);
                    i++;
                }
                stylePartOne.Add(sentence[i]);
                i++;

                while (sentence[i] != '<') {
                    TextBetween.Add(sentence[i]);
                    i++;
                }

                stylePartTwo.Add(sentence[i]);
                i++;
                while (sentence[i] != '>') {
                    stylePartTwo.Add(sentence[i]);
                    i++;
                }
                stylePartTwo.Add(sentence[i]);
                i++;

                List<char> tmp = new();
                for (int j = 0; j < TextBetween.Count; j++) {
                    tmp.Add(TextBetween[j]);

                    var Final = new List<char>(charList);
                    Final.AddRange(stylePartOne);
                    Final.AddRange(tmp);
                    Final.AddRange(stylePartTwo);

                    mainText.text = new string(Final.ToArray());
                    //Do Typewriter Noise

                    yield return new WaitForSeconds(TimeBetweenChars);
                }

                charList.AddRange(stylePartOne);
                charList.AddRange(TextBetween);
                charList.AddRange(stylePartTwo);
            }

            if(i >= sentence.Length) 
                continue;

            charList.Add(sentence[i]);
            mainText.text = new string(charList.ToArray());
            //Do Typewriter Noise

            yield return new WaitForSeconds(TimeBetweenChars);
        }

        var autoSkip = CheckCommand(currentDialog[index], AutoNextChar);
        if (autoSkip != null) {
            index++;
            NextLine();
        }
    }
    
    private T ParseEnum<T>(string value) {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}