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

    private bool isAvatarProfileClicked = false;
    private void Awake()
    {
        ChapterMenuHandler.undertutorial = true;

    }
    void Start()
    {
        // Debug: Reset tutorial for testing
        Debug.Log(PlayerPrefs.GetString("HasSeenTutorial", "No"));
        if (PlayerPrefs.HasKey("HasSeenTutorial"))
        {
            if (PlayerPrefs.GetString("HasSeenTutorial", "No") == "No")
            {
                Debug.Log("YEAH: 1");

                DeleteButton.interactable = false;
                KabanataButton.interactable = false;
                SettingsButton.interactable = false;
                PlayerPrefs.SetString("HasSeenTutorial", "Yes");
                PlayerPrefs.Save();

                ShowTutorial();

            }
            else
            {
                Debug.Log("YEAH: 2");
                ChapterMenuHandler.undertutorial = false;
                DeleteButton.interactable = true;
                KabanataButton.interactable = true;
                SettingsButton.interactable = true;

            }

        }
        else
        {
            Debug.Log("YEAH: 3");

            DeleteButton.interactable = false;
            KabanataButton.interactable = false;
            SettingsButton.interactable = false;
            PlayerPrefs.SetString("HasSeenTutorial", "Yes");
            PlayerPrefs.Save();

            ShowTutorial();
        }
        


    }

    private void ShowTutorial()
    {
        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Welcome to Magellan Odyssey Game")
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
              .SetMessage("Take note, some of the button are disabled until you finish the tutorial.")
              .OnClose(() => ShowTutorialDialog())
              .Show();
    }

    private void ShowTutorialDialog()
    {
        ShowPointer1.SetActive(true);
        TutorialDialogPrompt.Instance
            .SetMessage("I put an arrow pointing to the avatar profile. Tap that to open your profile tab.")
            .SetImage(null)
            .OnClose(() => Pointer1())
            .Show();
    }

    public void Pointer1()
    {
        Debug.Log("Pointer1 activated");

        ShowPointer1.SetActive(false);

        if (!isAvatarProfileClicked)
        {
            isAvatarProfileClicked = true;
            AvatarProfileButton.onClick.RemoveAllListeners(); // Ensure no duplicate listeners
            AvatarProfileButton.onClick.AddListener(() =>
            {
                TutorialDialogPrompt.Instance
                    .SetImage(null)
                    .SetMessage("In this tab, you can view your profile information and also change your name if you wanted. ACP and ADP can also be viewed here!")
                    .OnClose(() => Pointer2Start())
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

            .Show();

        
    }

    public void Pointer2End()
    {
        TutorialDialogPrompt.Instance
           .SetImage(ADP)
           .SetMessage("ADP. ang Adventure Points ay ang pera na magagamit sa pagbukas ng mga kabanata. Para magkaroon ng adventure Points, kailangan mong gawin ang mga sumusunod:" +
            "\n - Tapusin ang mga kabanata" +
            "\n - Gawin ang mga Quest (kabilang na ang Mini-Games sa Quest)" +
            "\n - Magkaroon ng Perpektong Puntos sa mga Pagsusulit" +
            "\n")
            .OnClose(() => Pointer3Start())

           .Show();
     


       
    }

    public void Pointer3Start()
    {

        TutorialDialogPrompt.Instance
            .SetImage(null)
            .SetMessage("Now you can close this tab and proceed to the next part of the tutorial")
            
            

            .Show();

        CloseButton.onClick.AddListener(() =>
        {
            Pointer3End();
        });
    }

    public void Pointer3End()
    {
        ShowPointer3.SetActive (true);
        TutorialDialogPrompt.Instance
            .SetImage(null)
            .SetMessage("Lets go the settings, i have put an arrow pointing to the button for opening the settings tab.")

            .OnClose(() => Pointer4Start())

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
            .SetMessage("This is the settings, you can adjust the audio of the game and change the graphics quality.")

            .OnClose(() => Pointer4End())

            .Show();

        });
    }

    public void Pointer4End()
    {

        SettingsCloseButton.onClick.AddListener(() =>
        {
            TutorialDialogPrompt.Instance
            .SetImage(null)
            .SetMessage("Now you can tap on the Kabanata 1 to start you adventure!")


            .Show();

            TutorialDialogPrompt.Instance
            .SetImage(null)
            .SetMessage("This the end of the tutorial here. Goodluck!")

            .OnClose(() => ChapterMenuHandler.undertutorial = false)
            .Show();



        });
       
    }
}
