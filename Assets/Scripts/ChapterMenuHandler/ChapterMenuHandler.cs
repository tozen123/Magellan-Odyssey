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

    [Header("Chapter 1 Level Buttons")]
    public Button[] ButtonChapter1Levels;

    [SerializeField] private int ADP;
    private void Start()
    {
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

        UpdateButtonInteractability();
    }
 
    private void UpdateButtonInteractability()
    {
        int current_adp = ADP;

        // Set interactability based on the value of current_adp
        if (current_adp < 0)
        {
            ButtonChapter1.GetComponent<ButtonStateHandler>().SetToUnLockState();
            ButtonChapter2.GetComponent<ButtonStateHandler>().SetToLockState();
            ButtonChapter3.GetComponent<ButtonStateHandler>().SetToLockState();
        }
        else if (current_adp >= 0 && current_adp < 1000)
        {
            ButtonChapter1.GetComponent<ButtonStateHandler>().SetToUnLockState();
            ButtonChapter2.GetComponent<ButtonStateHandler>().SetToLockState();
            ButtonChapter3.GetComponent<ButtonStateHandler>().SetToLockState();
        }
        else if (current_adp >= 1000 && current_adp < 2000)
        {
            ButtonChapter1.GetComponent<ButtonStateHandler>().SetToUnLockState();
            ButtonChapter2.GetComponent<ButtonStateHandler>().SetToUnLockState();
            ButtonChapter3.GetComponent<ButtonStateHandler>().SetToLockState();
        }
        else if (current_adp >= 2000)
        {
            ButtonChapter1.GetComponent<ButtonStateHandler>().SetToUnLockState();
            ButtonChapter2.GetComponent<ButtonStateHandler>().SetToUnLockState();
            ButtonChapter3.GetComponent<ButtonStateHandler>().SetToUnLockState();
        }
        else
        {
            ButtonChapter1.GetComponent<ButtonStateHandler>().SetToLockState();
            ButtonChapter2.GetComponent<ButtonStateHandler>().SetToLockState();
            ButtonChapter3.GetComponent<ButtonStateHandler>().SetToLockState();
            Debug.LogWarning("Unexpected value for adventure_points: " + current_adp);
        }

        for (int i = 0; i < ButtonChapter1Levels.Length; i++)
        {
            string levelKey = "Chapter1Level" + (i + 1);
            string levelState = PlayerPrefs.GetString(levelKey);

            if (levelState.Equals("IN_PROGRESS")  )
            {
                ButtonChapter1Levels[i].GetComponent<ButtonStateHandler>().SetToUnLockState();
            }
            else if (levelState.Equals("COMPLETED"))
            {
                ButtonChapter1Levels[i].GetComponent<ButtonStateHandler>().SetToCheckState();
            }
            else if (levelState.Equals("LOCKED"))
            {
                ButtonChapter1Levels[i].GetComponent<ButtonStateHandler>().SetToLockState();
            }
            else
            {
                ButtonChapter1Levels[i].GetComponent<ButtonStateHandler>().SetToLockState();
                Debug.LogWarning("Unexpected value for " + levelKey + ": " + levelState);
            }
        }

    }

    private void Update()
    {
        UpdateButtonInteractability();
    }

    public void SetStateChapter1Map(bool state)
    {
        SoundEffectManager.PlayButtonClick2();
        if (state == true)
        {
            ChapterScrollViewHandler.SetActive(false);

        }
        else
        {
            ChapterScrollViewHandler.SetActive(true);

        }

        Chapter1Map.SetActive(state);
    }


    public void LoadChapterLevel(string sceneName)
    {
        SoundEffectManager.PlayButtonClick2();
        LoadingScreenManager.Instance.LoadScene(sceneName);
    }
}
