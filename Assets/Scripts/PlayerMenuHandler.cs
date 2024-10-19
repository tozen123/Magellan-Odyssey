using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; // For handling AudioMixer

public class PlayerMenuHandler : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button menuButton;
    [SerializeField] private Button resumeMenuButton;
    [SerializeField] private Button exitButton;

    [Header("References")]
    [SerializeField] private GameObject menuSettingPanel;
    [SerializeField] private AudioMixer mainAudioMixer;  // Reference to the Main Audio Mixer

    [Header("Volume Settings")]
    [SerializeField] private Slider volumeSlider;
    private const string VolumeParameter = "MasterVolume";  // Make sure this matches the exposed AudioMixer parameter

    private bool isMenuToggledOn = false;

    void Start()
    {
        menuButton.onClick.AddListener(MenuSettingsToggleState);
        resumeMenuButton.onClick.AddListener(MenuSettingsToggleState);
        exitButton.onClick.AddListener(GameExit);

        // Load saved volume level
        if (PlayerPrefs.HasKey("VolumeLevel"))
        {
            float savedVolume = PlayerPrefs.GetFloat("VolumeLevel");
            volumeSlider.value = savedVolume;
            SetVolume(savedVolume);
        }
        else
        {
            volumeSlider.value = 1f;  // Default volume
            SetVolume(1f);
        }

        // Add listener for volume slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void MenuSettingsToggleState()
    {
        SoundEffectManager.PlayButtonClick2();
        isMenuToggledOn = !isMenuToggledOn;
        if (isMenuToggledOn)
        {
            Time.timeScale = 0;
            menuSettingPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            menuSettingPanel.SetActive(false);
        }
    }

    void GameExit()
    {
        SoundEffectManager.PlayButtonClick2();
        Time.timeScale = 1;
        DialogMessagePromptAction.Instance
            .SetTitle("Exit Confirmation")
            .SetMessage("Are you sure you want to exit? All progress will be lost in this level.")
            .OnPositive(FinalExit)
            .OnNegative(MenuSettingsToggleState)
            .Show();
    }

    void FinalExit()
    {
        SoundEffectManager.PlayButtonClick2();
        LoadingScreenManager.Instance.LoadScene("MainMenu-Sequence1");
    }

    // Method to set the volume based on the slider value
    public void SetVolume(float volume)
    {
        // Convert linear slider value (0 to 1) to decibels (-80 to 0 dB)
        float volumeInDecibels = Mathf.Log10(volume) * 20;
        mainAudioMixer.SetFloat(VolumeParameter, volumeInDecibels);

        // Save the volume level in PlayerPrefs
        PlayerPrefs.SetFloat("VolumeLevel", volume);
        PlayerPrefs.Save();
    }
}
