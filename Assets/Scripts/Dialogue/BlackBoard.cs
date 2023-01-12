using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    public static BlackBoard instance;

    private void Awake() {
        instance = this;

        foreach (var item in settings) {
            SettingsWithValue.Add(item, false);
        }
    }

    public int CurrentIndex;

    public List<string> settings = new();

    [HideInInspector] public Dictionary<string, bool> SettingsWithValue = new();

    private void OnEnable() {
        EventManager<string>.Subscribe(EventType.SET_SETTING, ToggleSetting);
        EventManager.Subscribe(EventType.UP_GLOBAL_INDEX, UpIndex);
    }
    private void OnDisable() {
        EventManager<string>.Unsubscribe(EventType.SET_SETTING, ToggleSetting);
        EventManager.Unsubscribe(EventType.UP_GLOBAL_INDEX, UpIndex);
    }

    public void ToggleSetting(string settingName) {
        if (SettingsWithValue.ContainsKey(settingName)) {
            SettingsWithValue[settingName] = true;
        }
    }

    public void UpIndex() {
        CurrentIndex++;
    }
}