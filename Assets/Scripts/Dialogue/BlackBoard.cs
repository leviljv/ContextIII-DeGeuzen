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

    private Dictionary<string, bool> SettingsWithValue = new();


}