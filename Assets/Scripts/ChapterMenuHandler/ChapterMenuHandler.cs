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


    [SerializeField] private int ADP;
    private void Start()
    {

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
