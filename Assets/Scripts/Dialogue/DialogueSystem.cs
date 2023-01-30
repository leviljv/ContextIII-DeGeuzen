using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour {
    [Header("Reference")]
    public GameObject DialogueSystemObject;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI nameText;
    public GameObject PortraitObject;
    public Image portrait;
    [HideInInspector] public Image Portrait => portrait ??= PortraitObject.GetComponent<Image>();
    public GameObject buttonContainer;
    public GameObject buttonPanel;
    public GameObject buttonPrefab;
    public AudioManager Amanager;

    [Header("CommandSettings")]
    public char CommandChar;
    public char OptionChar;
    public char SectionChar;
    public char AutoNextChar;

    [Header("Visual Settings")]
    public float TimeBetweenChars;
    [HideInInspector] public float currentTimeBetweenChars;

    private DialogFunctionality funcs = new();
    private Dictionary<string, string[]> Files = new();
    private string[] currentDialog;
    private int index;
    private bool IsWriting;

    [HideInInspector] public GameObject CurrentInteracting;

    void Awake() {
        funcs.Owner = this;
        funcs.Init();

        Amanager.Init();

        currentTimeBetweenChars = TimeBetweenChars;

        var tmp = Resources.LoadAll<TextAsset>("Dialogue/DialogFiles/");

        foreach (var item in tmp) {
            Files.Add(item.name, PrepFile(item));
        }
    }

    private void OnEnable() {
        EventManager<string>.Subscribe(EventType.ON_DIALOG_STARTED, SetDialog);
        EventManager<GameObject>.Subscribe(EventType.ON_DIALOG_STARTED, SetObject);
        EventManager.Subscribe(EventType.RESET_DIALOG, ResetDialog);
    }
    private void OnDisable() {
        EventManager<string>.Unsubscribe(EventType.ON_DIALOG_STARTED, SetDialog);
        EventManager<GameObject>.Unsubscribe(EventType.ON_DIALOG_STARTED, SetObject);
        EventManager.Unsubscribe(EventType.RESET_DIALOG, ResetDialog);
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
            DialogueSystemObject.SetActive(true);
            IsWriting = false;
            index = 0;
            currentDialog = Files[DialogName];
            EventManager<bool>.Invoke(EventType.SET_INTERACTION_STATE, true);

            RetryOnError();
        }
        else
            Debug.LogError("No File named " + DialogName + " found!");
    }

    private void RetryOnError() {
        try {
            NextLine();
        }
        catch {
            Debug.Log("AAAAHHH, Portrait went WRONGY SADDDDD :(:(:(");

            Invoke(nameof(RetryOnError), .001f);
        }
    }

    private void SetObject(GameObject Object) {
        CurrentInteracting = Object;
    }

    public void ResetDialog() {
        StopAllCoroutines();
        currentDialog = null;
        CurrentInteracting = null;
        mainText.text = "";
        nameText.text = "";
        index = 0;
        IsWriting = false;
        RemoveOptions();
        DialogueSystemObject.SetActive(false);
        EventManager<bool>.Invoke(EventType.SET_INTERACTION_STATE, false);
        EventManager.Invoke(EventType.ON_DIALOG_ENDED);
    }

    private void NextLine() {
        if (currentDialog == null)
            return;

        if (IsWriting) {
            FullLine(currentDialog[index - 1].Trim());
            return;
        }

        if (currentDialog.Length - 1 < index) {
            ResetDialog();
            return;
        }

        currentTimeBetweenChars = TimeBetweenChars;

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

            if(line[1] == "end" || line[1] == "wait")
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
        if(command.Length < 3) 
            EventManager.Invoke(ParseEnum<EventType>(command[1].ToUpper()));
        else if (float.TryParse(command[2].ToLower(), out var floatParse))
            EventManager<float>.Invoke(ParseEnum<EventType>(command[1].ToUpper()), floatParse);
        else if (bool.TryParse(command[2].ToLower(), out var boolParse))
            EventManager<bool>.Invoke(ParseEnum<EventType>(command[1].ToUpper()), boolParse);
        else
            EventManager<string>.Invoke(ParseEnum<EventType>(command[1].ToUpper()), command[2].Trim());
    }

    private void DisplayOptions(string[] file) {
        buttonPanel.SetActive(true);

        List<GameObject> buttons = new();

        while (file[index].Trim().ToCharArray()[0] == '@') {
            var tmpButton = Instantiate(buttonPrefab, buttonContainer.transform);
            var text = file[index].Trim().Split(" ", 2)[1];
            tmpButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
            tmpButton.GetComponent<Button>().onClick.AddListener(() => RemoveOptions());

            index++;

            string SectionName = "";

            if (CheckCommand(file[index], SectionChar) != null) {
                var tmp = file[index].Trim().Split(" ");

                if (tmp[1] != "jump")
                    Debug.LogError("Command after % has to be 'jump' for options");

                if (tmp[2] == "if") {
                    if (BlackBoard.instance.SettingsWithValue.ContainsKey(tmp[3])) {
                        if (BlackBoard.instance.SettingsWithValue[tmp[3]]) {
                            SectionName = tmp[4];
                        }
                        else {
                            SectionName = tmp[6];
                        }
                    }
                    else {
                        Debug.LogError("Condition: " + tmp[3] + " does not Exist.");
                        SectionName = tmp[6];
                    }
                }
                else
                    SectionName = tmp[2];
            }
            
            if(SectionName != "")
                tmpButton.GetComponent<Button>().onClick.AddListener(() => JumpToSection(SectionName));

            index++;
            buttons.Add(tmpButton);
        }
        
        for (int i = 0; i < buttons.Count; i++) {
            var parentRect = buttonContainer.GetComponent<RectTransform>();
            var rect = buttons[i].GetComponent<RectTransform>();

            if(buttons.Count % 2 != 0)
                rect.localPosition = new Vector2(0, (parentRect.sizeDelta.y / buttons.Count) * ((buttons.Count - 1 - i) - (buttons.Count) / 2));
            else
                rect.localPosition = new Vector2(0, (parentRect.sizeDelta.y / buttons.Count) * ((buttons.Count - 1 - i) - (buttons.Count) / 2) + (parentRect.sizeDelta.y / 2) / buttons.Count);

            rect.sizeDelta = new Vector2(parentRect.sizeDelta.x, (parentRect.sizeDelta.y / buttons.Count - 1));
        }
    }

    private void RemoveOptions() {
        buttonPanel.SetActive(false);

        for (int i = buttonContainer.transform.childCount - 1; i >= 0; i--) {
            Destroy(buttonContainer.transform.GetChild(i).gameObject);
        }
    }

    private void JumpToSection(string SectionName) {
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
        IsWriting = true;

        yield return new WaitForEndOfFrame();

        List<char> charList = new();

        var frontAndBack = text.Split(": ", 2);

        if (frontAndBack.Length < 2) {
            index++;
            NextLine();
            IsWriting = false;
            yield return null;
        }

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
                    TyperwriterNoise();

                    yield return new WaitForSeconds(currentTimeBetweenChars);
                }

                charList.AddRange(stylePartOne);
                charList.AddRange(TextBetween);
                charList.AddRange(stylePartTwo);
            }

            if(i >= sentence.Length) 
                continue;

            charList.Add(sentence[i]);
            mainText.text = new string(charList.ToArray());
            TyperwriterNoise();

            yield return new WaitForSeconds(currentTimeBetweenChars);
        }

        string[] autoSkip = null;

        if (index < currentDialog.Length - 1) {
            autoSkip = CheckCommand(currentDialog[index], AutoNextChar);
        }

        if (autoSkip != null) {
            index++;
            IsWriting = false;
            NextLine();
        }

        IsWriting = false;
    }

    private void FullLine(string text) {
        StopAllCoroutines();

        List<char> charList = new();

        var frontAndBack = text.Split(": ", 2);

        if (frontAndBack.Length < 2) {
            index++;
            NextLine();
            IsWriting = false;
            return;
        }

        var name = frontAndBack[0];
        nameText.text = name;
        var sentence = frontAndBack[1];

        int i = 0;

        while (i < sentence.Length) {
            if (sentence[i] == CommandChar) {
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

            charList.Add(sentence[i]);

            i++;

            if (i >= sentence.Length)
                break;
        }

        mainText.text = new string(charList.ToArray());

        string[] autoSkip = null;

        if (index < currentDialog.Length - 1) {
            autoSkip = CheckCommand(currentDialog[index], AutoNextChar);
        }
        if (autoSkip != null) {
            index++;
            IsWriting = false;
            NextLine();
        }

        IsWriting = false;
    }

    private void TyperwriterNoise() {
        var tmp = UnityEngine.Random.Range(1, 20);

        //if(tmp < 7)
            //Amanager.PlayAudio(tmp.ToString());
    }

    private T ParseEnum<T>(string value) {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}