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
            .SetMessage("Magsisimula ka na ngayon sa isang kapana-panabik na paglalakbay upang matutunan ang tungkol sa Ekspedisyon ni Ferdinand Magellan.")
            .Show();

        DialogMessagePrompt.Instance
            .SetTitle("System Message")
            .SetMessage("Ngunit bago ang lahat, gagabayan ka muna namin sa larong ito.")
            .Show();

        DialogMessagePrompt.Instance
            .SetTitle("System Message")
            .SetMessage("Paalala, ang ilang mga pindutan ay hindi magagamit hangga't hindi natatapos ang tutorial.")
            .OnClose(() => ShowTutorialDialog())
            .Show();
    }


    private void ShowTutorialDialog()
    {
        canvasAvatarPanel.SetActive(false);
        ShowPointer1.SetActive(true);
        TutorialDialogPrompt.Instance
            .SetMessage("Naglagay ako ng arrow na tumuturo sa avatar profile. I-tap ito upang buksan ang iyong profile tab.")
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
                    .SetMessage("Sa tab na ito, makikita mo ang impormasyon ng iyong profile at maari mo ring palitan ang iyong pangalan kung nais mo. Makikita rin dito ang ACP at ADP!")
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
            .SetMessage("Ngayon, maaari mong isara ang tab na ito at magpatuloy sa susunod na bahagi ng tutorial.")
            .OnPrevious(() => Pointer2End()) // Enable Back Button
            .Show();
    }

    public void Pointer3End()
    {
        canvasSettingsPanel.SetActive(false);

        ShowPointer3.SetActive(true);
        TutorialDialogPrompt.Instance
            .SetImage(null)
            .SetMessage("Pumunta tayo sa mga settings. Naglagay ako ng arrow na tumuturo sa pindutan para buksan ang settings tab.")
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
                .SetMessage("Ito ang settings. Maaari mong ayusin ang audio ng laro at baguhin ang kalidad ng graphics.")
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
                .SetMessage("Ngayon, maaari mo nang i-tap ang Kabanata 1 upang simulan ang iyong paglalakbay!")
                .OnClose(() => EndTutorial())
                .OnPrevious(() => Pointer4Start()) // Enable Back Button
                .Show();
        });
    }

    private void EndTutorial()
    {
        TutorialDialogPrompt.Instance
            .SetImage(null)
            .SetMessage("Ito na ang pagtatapos ng tutorial. Good luck!")
            .OnClose(() => ChapterMenuHandler.undertutorial = false)
            .Show();
    }

}
