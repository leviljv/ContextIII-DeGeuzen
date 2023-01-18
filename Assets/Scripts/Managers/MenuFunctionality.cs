using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuFunctionality : MonoBehaviour
{
    [Header("Toggle SettingsMenu")]
    public GameObject SettingsToggle;

    public AudioMixer mixer;

    public bool SettingsMenuCurrentlyActive = false;

    [Header("Toggle Settings")]
    public GameObject FullscreenCheck;
    public GameObject BloodCheck;
    public GameObject MarkerCheck;

    public bool isFullscreen = false;
    public bool ShowBlood = false;
    public bool ShowMarkers = false;

    public void NewGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetMusicVolume(float value) {
        mixer.SetFloat("MusicVol", Mathf.Log10(value) * 20);
    }
    public void SetEffectsVolume(float value) {
        mixer.SetFloat("SFXVol", Mathf.Log10(value) * 20);
    }

    public void ToggleSettingsMenu() {
        SettingsMenuCurrentlyActive = !SettingsMenuCurrentlyActive;

        SettingsToggle.SetActive(SettingsMenuCurrentlyActive);
    }

    public void ToggleFullscreen() {
        isFullscreen = !isFullscreen;

        FullscreenCheck.SetActive(isFullscreen);
        SetSettings();
    }

    public void ToggleQuestMarkers() {
        ShowMarkers = !ShowMarkers;

        MarkerCheck.SetActive(ShowMarkers);
        SetSettings();
    }

    public void ToggleBlood() {
        ShowBlood = !ShowBlood;

        BloodCheck.SetActive(ShowBlood);
        SetSettings();
    }

    public void SetSettings() {
        BlackBoard.instance.GlobalSettings.BloodShown = ShowBlood;
        BlackBoard.instance.GlobalSettings.QuestMarkersShown = ShowMarkers;
        BlackBoard.instance.GlobalSettings.Fullscreen = isFullscreen;
    }

    public void Quit() {
        Application.Quit();

        Debug.Log("Quit");
    }
}