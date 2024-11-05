using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChapterResultController : MonoBehaviour
{
    [SerializeField] private string TargetNextChapterLevel;

    [SerializeField] private string ChapterQuizScoreNamePrefs;

    [SerializeField] private TextMeshProUGUI ADP_EARNED;
    [SerializeField] private TextMeshProUGUI CHAPTER_QUIZ_POINT;


    private void Start()
    {
        CHAPTER_QUIZ_POINT.text = PlayerPrefs.GetInt(ChapterQuizScoreNamePrefs, -1).ToString() ;
        ADP_EARNED.text = PlayerPrefs.GetInt("adventure_points", -1).ToString();
    }
    public void GoToNextChapter()
    {
        LoadingScreenManager.Instance.LoadScene(TargetNextChapterLevel);
    }

    public void GotoHome()
    {
        LoadingScreenManager.Instance.LoadScene("MainMenu-Sequence1");

    }
}
