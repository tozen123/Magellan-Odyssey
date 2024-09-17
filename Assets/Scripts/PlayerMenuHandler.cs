using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuHandler : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button menuButton;
    [SerializeField] private Button resumeMenuButton;
    [SerializeField] private Button exitButton;

    [Header("Reference")]
    [SerializeField] private GameObject menuSettingPanel;

    private bool isMenuToggledOn = false;

    void Start()
    {
        menuButton.onClick.AddListener(MenuSettingsToggleState);
        resumeMenuButton.onClick.AddListener(MenuSettingsToggleState);
        exitButton.onClick.AddListener(GameExit);

    }
    void MenuSettingsToggleState()
    {
        SoundEffectManager.PlayButtonClick2();
        isMenuToggledOn = !isMenuToggledOn;
        if (isMenuToggledOn)
        {
            Time.timeScale = 0;
            menuSettingPanel.SetActive(isMenuToggledOn);
        }
        else
        {
            Time.timeScale = 1;
            menuSettingPanel.SetActive(isMenuToggledOn);

        }
    }

    void GameExit()
    {
        SoundEffectManager.PlayButtonClick2();

        Time.timeScale = 1;
        DialogMessagePromptAction.Instance
            .SetTitle("Exit Confirmation")
            .SetMessage("Are you sure you want to exit? all progress will be deleted in this level")
            .OnPositive(FinalExit)
            .OnNegative(MenuSettingsToggleState)
            .Show();
    }

    void FinalExit()
    {
        SoundEffectManager.PlayButtonClick2();

        LoadingScreenManager.Instance.LoadScene("MainMenu-Sequence1");
    }



}
