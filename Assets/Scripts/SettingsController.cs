using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;  // Required for AudioMixer

public class SettingsController : MonoBehaviour
{
    [Header("Chapters")]
    [SerializeField] private GameObject MainCanvas;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer mainAudioMixer;  // Reference to the Main Audio Mixer

    private const string VolumeParameter = "MasterVolume";  // Name of the exposed volume parameter in AudioMixer

    void Start()
    {
        MainCanvas.SetActive(false);

        // Load and apply saved graphics quality
        if (PlayerPrefs.HasKey("GraphicsQuality"))
        {
            int savedQuality = PlayerPrefs.GetInt("GraphicsQuality");
            label.text = "Graphics Quality: " + GetQualityLevelString(savedQuality);
        }

        // Load saved volume level and set it to the slider and audio mixer
        if (PlayerPrefs.HasKey("VolumeLevel"))
        {
            float savedVolume = PlayerPrefs.GetFloat("VolumeLevel");
            volumeSlider.value = savedVolume;
            SetVolume(savedVolume);  // Apply the volume to the AudioMixer
        }
        else
        {
            volumeSlider.value = 1.5f;  // Default volume to max if no saved value
            SetVolume(1.5f);
        }

        // Add listener to the volume slider to adjust volume in real-time
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private bool isToggle = false;

    public void Toggle()
    {
        SoundEffectManager.PlayButtonClick2();
        isToggle = !isToggle;
        MainCanvas.SetActive(isToggle);
    }

    public void SetGraphicsQuality(int index)
    {
        SoundEffectManager.PlayButtonClick2();

        QualitySettings.SetQualityLevel(index);
        SaveGraphicsQuality(index);

        int qualityLevel = QualitySettings.GetQualityLevel();
        string qualityLevelString = GetQualityLevelString(qualityLevel);
        label.text = "Graphics Quality: " + GetQualityLevelString(qualityLevel);
        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("You have successfully changed the Game Graphics Quality to " + qualityLevelString)
               .Show();
    }

    private string GetQualityLevelString(int level)
    {
        switch (level)
        {
            case 0:
                return "Low";
            case 1:
                return "Medium";
            case 2:
                return "High";
            default:
                return "Unknown";
        }
    }

    public void Devs()
    {
        DialogMessagePrompt.Instance
                 .SetTitle("System Message")
                 .SetMessage("Game Developed By: \n\n" +
                 "Christian Serwelas - Lead Game Developer\n" +
                 "Jan Christian King - 3D Artist \n" +
                 "Hariette Espina - Game and Art Designer")
                 .Show();
    }

    private void SaveGraphicsQuality(int qualityLevel)
    {
        PlayerPrefs.SetInt("GraphicsQuality", qualityLevel);
        PlayerPrefs.Save();
    }

    // Method to set volume level and save it
    public void SetVolume(float volume)
    {
        // Convert linear slider value (0 to 1) to decibels (-80 to 0 dB)
        float volumeInDecibels = Mathf.Log10(volume) * 20;
        mainAudioMixer.SetFloat(VolumeParameter, volumeInDecibels);

        // Save the volume value to PlayerPrefs
        PlayerPrefs.SetFloat("VolumeLevel", volume);
        PlayerPrefs.Save();
    }
}
