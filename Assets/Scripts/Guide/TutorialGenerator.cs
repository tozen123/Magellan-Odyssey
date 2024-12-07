using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGenerator : MonoBehaviour
{
    public GameObject ShowPointer1;
    public GameObject ShowPointer2;
    public GameObject ShowPointer3;
    public GameObject ShowPointer4;

    public Button AvatarProfileButton;
    public Button DeleteButton;
    public Button CloseButton;
    public Button SettingsCloseButton;

    public Button KabanataButton;
    public Button SettingsButton;

    public Sprite ACP;
    public Sprite ADP;

    public ChapterMenuHandler ChapterMenuHandler;

    public GameObject canvasAvatarPanel;
    public GameObject canvasSettingsPanel;
    private bool isAvatarProfileClicked = false;

    private void Awake()
    {
        ChapterMenuHandler.undertutorial = false;
    }

    void Start()
    {
        //PlayerPrefs.SetInt("Kabanata1BookOfTrivia_IsLock", 0);
        //PlayerPrefs.SetInt("academic_points", 385);
        //PlayerPrefs.SetInt("adventure_points", 1285);

        if (PlayerPrefs.GetString("HasSeenTutorial") == "")
        {
            Debug.Log("Starting Tutorial...");
            DeleteButton.interactable = false;
            KabanataButton.interactable = false;
            SettingsButton.interactable = false;

            PlayerPrefs.SetString("HasSeenTutorial", "Yes");
            PlayerPrefs.Save();

            ShowTutorial();
        }
        else
        {

            Debug.Log("Skipping Tutorial...");
            ChapterMenuHandler.undertutorial = false;
            DeleteButton.interactable = true;
            KabanataButton.interactable = true;
            SettingsButton.interactable = true;
        }

        CloseButton.onClick.AddListener(() =>
        {
            Pointer3End();
        });
    }

    private void ShowTutorial()
    {
        DialogMessagePrompt.Instance
            .SetTitle("System Message")
            .SetMessage("Maligayang pagdating sa larong Ang Paghahanda sa Paglalakbay ni Magellan")
            .Show();

        DialogMessagePrompt.Instance
            .SetTitle("System Message")
            .SetMessage("You will now embark on an engaging adventure to learn about Ferdinand Magellan's Expedition")
            .Show();

        DialogMessagePrompt.Instance
            .SetTitle("System Message")
            .SetMessage("But first, let's guide you through this game.")
            .Show();

        DialogMessagePrompt.Instance
            .SetTitle("System Message")
            .SetMessage("Take note, some buttons are disabled until you finish the tutorial.")
            .OnClose(() => ShowTutorialDialog())
            .Show();
    }

    private void ShowTutorialDialog()
    {
        canvasAvatarPanel.SetActive(false);
        ShowPointer1.SetActive(true);
        TutorialDialogPrompt.Instance
            .SetMessage("I put an arrow pointing to the avatar profile. Tap that to open your profile tab.")
            .SetImage(null)
            .OnClose(() => Pointer1())
            .OnPrevious(null)
            .Show();
    }

    public void Pointer1()
    {
        Debug.Log("Pointer1 activated");

        ShowPointer1.SetActive(false);

        if (!isAvatarProfileClicked)
        {
            isAvatarProfileClicked = true;

            AvatarProfileButton.onClick.RemoveAllListeners();
            AvatarProfileButton.onClick.AddListener(() =>
            {
                TutorialDialogPrompt.Instance
                    .SetImage(null)
                    .SetMessage("In this tab, you can view your profile information and also change your name if you wanted. ACP and ADP can also be viewed here!")
                    .OnClose(() => Pointer2Start())
                    .OnPrevious(() => ShowTutorialDialog()) // Enable Back Button
                    .Show();
            });
        }
    }

    public void Pointer2Start()
    {
        TutorialDialogPrompt.Instance
            .SetImage(ACP)
            .SetMessage("ACP. Ang bilang ng Academic Points ay may kaakibat na Academic Rank para matukoy ang antas ng kaalaman. Para magkaroon ng Academic Points, sagutan ang mga pagsusulit pagkatapos ng bawat kabanata. " +
            "\n - Academic Rank S: 1,000 ACP+" +
            "\n - Academic Rank A: 500 ACP+" +
            "\n - Academic Rank B: 200 ACP+" +
            "\n")
            .OnClose(() => Pointer2End())
            .OnPrevious(() => Pointer1()) // Enable Back Button
            .Show();
    }

    public void Pointer2End()
    {
        
        TutorialDialogPrompt.Instance
            .SetImage(ADP)
            .SetMessage("ADP. Ang Adventure Points ay ang pera na magagamit sa pagbukas ng mga kabanata. Para magkaroon ng Adventure Points, kailangan mong gawin ang mga sumusunod:" +
            "\n - Tapusin ang mga kabanata" +
            "\n - Gawin ang mga Quest (kabilang na ang Mini-Games sa Quest)" +
            "\n - Magkaroon ng Perpektong Puntos sa mga Pagsusulit" +
            "\n")
            .OnClose(() => Pointer3Start())
            .OnPrevious(() => Pointer2Start()) // Enable Back Button
            .Show();
    }

    public void Pointer3Start()
    {
        canvasAvatarPanel.SetActive(true);
        ShowPointer3.SetActive(false);

        TutorialDialogPrompt.Instance
            .SetImage(null)
            .SetMessage("Now you can close this tab and proceed to the next part of the tutorial.")
            .OnPrevious(() => Pointer2End()) // Enable Back Button
            .Show();

        
    }

    public void Pointer3End()
    {
        canvasSettingsPanel.SetActive(false);

        ShowPointer3.SetActive(true);
        TutorialDialogPrompt.Instance
            .SetImage(null)
            .SetMessage("Let's go to the settings. I have put an arrow pointing to the button for opening the settings tab.")
            .OnClose(() => Pointer4Start())
            .OnPrevious(() => Pointer3Start()) // Enable Back Button
            .Show();
    }

    public void Pointer4Start()
    {
        ShowPointer3.SetActive(false);

        SettingsButton.interactable = true;

        SettingsButton.onClick.AddListener(() =>
        {
            TutorialDialogPrompt.Instance
                .SetImage(null)
                .SetMessage("This is the settings. You can adjust the audio of the game and change the graphics quality.")
                .OnClose(() => Pointer4End())
                .OnPrevious(() => Pointer3End()) // Enable Back Button
                .Show();
        });
    }

    public void Pointer4End()
    {
        SettingsCloseButton.onClick.AddListener(() =>
        {
            TutorialDialogPrompt.Instance
                .SetImage(null)
                .SetMessage("Now you can tap on Kabanata 1 to start your adventure!")
                .OnClose(() => EndTutorial())
                .OnPrevious(() => Pointer4Start()) // Enable Back Button
                .Show();
        });
    }

    private void EndTutorial()
    {
        TutorialDialogPrompt.Instance
            .SetImage(null)
            .SetMessage("This is the end of the tutorial. Good luck!")
            .OnClose(() => ChapterMenuHandler.undertutorial = false)
            .Show();
    }
}
