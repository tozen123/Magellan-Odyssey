using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterMenuHandler : MonoBehaviour
{
    [Header("Chapters")]
    public GameObject Chapter1Map;
    public GameObject ChapterScrollViewHandler;

    [Header("Chapters Buttons")]
    public Button ButtonChapter1;
    public Button ButtonChapter2;
    public Button ButtonChapter3;
    public Button ButtonChapter4;
    public Button ButtonChapter5;
    public Button ButtonChapter6;

    [SerializeField] private int ADP;

    public bool undertutorial;
    private void Start()
    {

        //UnlockMode();

        Debug.Log("--------------------------------------------------------------");
        Debug.Log("Chapter1: " + PlayerPrefs.GetString("Chapter1"));
        Debug.Log("Chapter1Level1: " + PlayerPrefs.GetString("Chapter1Level1"));
        Debug.Log("Chapter1Level2: " + PlayerPrefs.GetString("Chapter1Level2"));
        Debug.Log("Chapter1Level3: " + PlayerPrefs.GetString("Chapter1Level3"));
        Debug.Log("Chapter1Level4: " + PlayerPrefs.GetString("Chapter1Level4"));
        Debug.Log("Chapter1Level5: " + PlayerPrefs.GetString("Chapter1Level5"));
        Debug.Log("--------------------------------------------------------------");

        if (!PlayerPrefs.HasKey("adventure_points"))
        {
            PlayerPrefs.SetInt("adventure_points", 0);
        }
        else
        {
            ADP = PlayerPrefs.GetInt("adventure_points");
        }

        UpdateAllButtonsInteractability();
    }
    void UnlockMode()
    {
        PlayerPrefs.SetInt("Kabanata1BookOfTrivia_IsLock", 0);
        PlayerPrefs.SetInt("Kabanata2BookOfTrivia_IsLock", 0);
        PlayerPrefs.SetInt("Kabanata3BookOfTrivia_IsLock", 0);
        PlayerPrefs.SetInt("Kabanata4BookOfTrivia_IsLock", 0);
        PlayerPrefs.SetInt("Kabanata5BookOfTrivia_IsLock", 0);
        PlayerPrefs.SetInt("Kabanata6BookOfTrivia_IsLock", 0);

        PlayerPrefs.SetInt("adventure_points", 9999);
        PlayerPrefs.SetInt("Chapter1TotalQuizScore", 9999);

        PlayerPrefs.SetString("Chapter1Level1", "IN_PROGRESS");
        PlayerPrefs.SetString("Chapter1Level2", "IN_PROGRESS");
        PlayerPrefs.SetString("Chapter1Level3", "IN_PROGRESS");
        PlayerPrefs.SetString("Chapter1Level4", "IN_PROGRESS");
        PlayerPrefs.SetString("Chapter1Level5", "IN_PROGRESS");
        PlayerPrefs.SetString("Chapter1Level6", "IN_PROGRESS");

        PlayerPrefs.Save();

    }
    private void UpdateAllButtonsInteractability()
    {
        // Update all buttons based on their chapter level progress
        UpdateButtonState(ButtonChapter1, "Chapter1Level1");
        UpdateButtonState(ButtonChapter2, "Chapter1Level2");
        UpdateButtonState(ButtonChapter3, "Chapter1Level3");
        UpdateButtonState(ButtonChapter4, "Chapter1Level4");
        UpdateButtonState(ButtonChapter5, "Chapter1Level5");
        UpdateButtonState(ButtonChapter6, "Chapter1Level6");
    }

    private void UpdateButtonState(Button button, string chapterLevelKey)
    {
        string state = PlayerPrefs.GetString(chapterLevelKey); 

        switch (state)
        {
            case "IN_PROGRESS":
                button.GetComponent<ButtonStateHandler>().SetToUnLockState();
                break;
            case "COMPLETED":
                button.GetComponent<ButtonStateHandler>().SetToCheckState();
                break;
            case "LOCKED":
            default:
                button.GetComponent<ButtonStateHandler>().SetToLockState();
                break;
        }
    }

    private void Update()
    {
        if (!undertutorial)
        {
            UpdateAllButtonsInteractability();

        }else
        {
            ButtonChapter1.interactable = false;
        }
    }



    public void LoadChapterLevel(string sceneName)
    {
         
        SoundEffectManager.PlayButtonClick2();
        LoadingScreenManager.Instance.LoadScene(sceneName);
    }
}
