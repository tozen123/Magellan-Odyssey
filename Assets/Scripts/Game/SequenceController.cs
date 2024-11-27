using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SequenceController : MonoBehaviour
{
    [Header("Creation Inputs")]
    public TMP_InputField inputName;



    [Header("Avatar Canvas")]
    public GameObject creationCanvas;
    public GameObject loginCanvas;

    private CanvasGroup creationCanvasGroup;
    private CanvasGroup loginCanvasGroup;

    [Header("Button")]
    public Button LoginButton;
    void Start()
    {
        Application.targetFrameRate = 60;
        LoadGraphicsQuality();

        creationCanvasGroup = creationCanvas.GetComponent<CanvasGroup>();
        loginCanvasGroup = loginCanvas.GetComponent<CanvasGroup>();

        creationCanvas.SetActive(false);
        loginCanvas.SetActive(false);

        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Maligayang pagdating sa larong Ang Paghahanda sa Paglalakbay ni Magellan")
               .Show();

        if (PlayerPrefs.HasKey("userName") )
        {
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Maligayang pagdating, Player. " + PlayerPrefs.GetString("userName"))
                .OnClose(AutomaticLogin)
                .Show();
        }
        else
        {
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Kamusta, Player. bago ka mag simula mag laro kailangan mo muna gumawa ng avatar")
                .OnClose(ShowCreationAvatarCanvas)
                .Show();
        }
    }
    private void Update()
    {
        // Check if the input name is empty
        if (inputName.text == "")
        {
            LoginButton.interactable = false;

        }
        else
        {
            LoginButton.interactable = true;

        }
    }
    private void LoadGraphicsQuality()
    {
        if (PlayerPrefs.HasKey("GraphicsQuality"))
        {
            int savedQuality = PlayerPrefs.GetInt("GraphicsQuality");
            QualitySettings.SetQualityLevel(savedQuality);
        }
        else
        {
            QualitySettings.SetQualityLevel(1);
            PlayerPrefs.SetInt("GraphicsQuality", 1);
            PlayerPrefs.Save();
        }
    }

    public void Register()
    {
      
       

        // Save the user name and initialize game progress
        PlayerPrefs.SetString("userName", inputName.text.ToString());

        PlayerPrefs.SetString("Chapter1", "IN_PROGRESS");
        PlayerPrefs.SetString("Chapter1Level1", "IN_PROGRESS");
        PlayerPrefs.SetString("Chapter1Level2", "LOCKED");
        PlayerPrefs.SetString("Chapter1Level3", "LOCKED");
        PlayerPrefs.SetString("Chapter1Level4", "LOCKED");
        PlayerPrefs.SetString("Chapter1Level5", "LOCKED");
        PlayerPrefs.SetString("Chapter1Level6", "LOCKED");
        PlayerPrefs.SetString("Chapter1Level7", "LOCKED");

        PlayerPrefs.Save();

        // Display success message
        DialogMessagePrompt.Instance
            .SetTitle("System Message")
            .SetMessage("Matagumpay na nalikha ang iyong Avatar.")
            .OnClose(Finish)
            .Show();


    }
   

    public void AutomaticLogin()
    {
        SoundEffectManager.PlayReward();

        LoadingScreenManager.Instance.LoadScene("MainMenu-Sequence1");
    }
    void ShowLogin()
    {
        loginCanvas.SetActive(true);
        StartCoroutine(FadeCanvasGroup(loginCanvasGroup, 0, 1, 1f));

    }
    void ShowCreationAvatarCanvas()
    {
        SoundEffectManager.PlayButtonCardPopup();

        creationCanvas.SetActive(true);
        StartCoroutine(FadeCanvasGroup(creationCanvasGroup, 0, 1, 1f));
    }

    public void Finish()
    {

        SoundEffectManager.PlayReward();

        LoadingScreenManager.Instance.LoadScene("MainMenu-Sequence1");

    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float elapsed = Time.time - startTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        canvasGroup.interactable = (endAlpha == 1);
        canvasGroup.blocksRaycasts = (endAlpha == 1);
    }

    private IEnumerator SwitchCanvas(CanvasGroup fromCanvas, CanvasGroup toCanvas)
    {
        yield return StartCoroutine(FadeCanvasGroup(fromCanvas, 1, 0, 0.3f));
        fromCanvas.gameObject.SetActive(false);
        toCanvas.gameObject.SetActive(true);
        yield return StartCoroutine(FadeCanvasGroup(toCanvas, 0, 1, 0.3f));
    }

}
