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

        switch (current_adp)
        {
            case 0:
                ButtonChapter1.interactable = true;
                ButtonChapter2.interactable = false;
                ButtonChapter3.interactable = false;
                break;

            case 1000:
                ButtonChapter1.interactable = true;
                ButtonChapter2.interactable = true;
                ButtonChapter3.interactable = false;
                break;

            case 2000:
                ButtonChapter1.interactable = true;
                ButtonChapter2.interactable = true;
                ButtonChapter3.interactable = true;
                break;

            default:
                ButtonChapter1.interactable = false;
                ButtonChapter2.interactable = false;
                ButtonChapter3.interactable = false;
                Debug.LogWarning("Unexpected value for adventure_points: " + current_adp);
                break;
        }

        for (int i = 0; i < ButtonChapter1Levels.Length; i++)
        {
            string levelKey = "Chapter1Level" + (i + 1);
            string levelState = PlayerPrefs.GetString(levelKey);

            if (levelState.Equals("IN_PROGRESS")  )
            {
                ButtonChapter1Levels[i].interactable = true;
            }
            else if (levelState.Equals("COMPLETED"))
            {
                ButtonChapter1Levels[i].interactable = false;
                ButtonChapter1Levels[i].image.color = Color.black;
            }
            else if (levelState.Equals("LOCKED"))
            {
                ButtonChapter1Levels[i].interactable = false;
            }
            else
            {
                ButtonChapter1Levels[i].interactable = false;
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
        LoadingScreenManager.Instance.LoadScene(sceneName);
    }
}
