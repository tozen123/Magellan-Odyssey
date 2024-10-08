using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [Header("Chapters")]
    [SerializeField] private GameObject MainCanvas;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI label;
    void Start()
    {
        MainCanvas.SetActive(false);


        if (PlayerPrefs.HasKey("GraphicsQuality"))
        {
            int savedQuality = PlayerPrefs.GetInt("GraphicsQuality");
            label.text = "Graphics Quality: " + GetQualityLevelString(savedQuality);
        }

    }

  

    private bool isToggle = false;
    public void Toggle()
    {
        SoundEffectManager.PlayButtonClick2();
        isToggle = !isToggle;
        if (isToggle)
        {
            MainCanvas.SetActive(true);
        }
        else
        {
            MainCanvas.SetActive(false);
        }
    }

    public void SetGraphicsQuality(int index)
    {
        SoundEffectManager.PlayButtonClick2();

        QualitySettings.SetQualityLevel(index);

        SaveGraphicsQuality(index);


        int qualityLevel = QualitySettings.GetQualityLevel();

        string qualityLevelString = GetQualityLevelString(qualityLevel);

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
}
