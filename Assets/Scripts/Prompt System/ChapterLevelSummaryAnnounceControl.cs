using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ChapterLevelSummaryAnnounceControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI questHeader;
    [SerializeField] private TextMeshProUGUI chapterHeader;
    [SerializeField] private TextMeshProUGUI chapterSummary;
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Transform headerParent;

    public static ChapterLevelSummaryAnnounceControl Instance;

    [Header("Canvas")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private CanvasGroup canvasGroup;

    private ChapterSummary currentSummary;
    private string announcement;

    [HideInInspector] public bool IsActive = false;

    void Awake()
    {
        Instance = this;
        canvasGroup = canvas.GetComponent<CanvasGroup>();

        buttonContinue.onClick.RemoveAllListeners();
        buttonContinue.onClick.AddListener(() => {
            Hide();
            OnSummaryContinue();
        });
    }

    public ChapterLevelSummaryAnnounceControl SetTitle(string title)
    {
        if (currentSummary == null) currentSummary = new ChapterSummary();
        currentSummary.Title = title;
        return Instance;
    }

    //public ChapterLevelSummaryAnnounceControl SetQuests(List<Quest> quests)
    //{
    //    if (currentSummary == null) currentSummary = new ChapterSummary();
    //    currentSummary.Quests = quests;
    //    return Instance;
    //}


    public ChapterLevelSummaryAnnounceControl SetAnnounce(string announce)
    {
        announcement = announce;
        return Instance;
    }

    public ChapterLevelSummaryAnnounceControl SetFadeInDuration(float duration)
    {
        if (currentSummary == null) currentSummary = new ChapterSummary();
        currentSummary.FadeInDuration = duration;
        return Instance;
    }

    public ChapterLevelSummaryAnnounceControl OnContinue(UnityAction action)
    {
        if (currentSummary == null) currentSummary = new ChapterSummary();
        currentSummary.OnContinue = action;
        return Instance;
    }

    public void Show()
    {
        SoundEffectManager.PlayNotice();

        if (currentSummary == null)
        {
            Debug.LogError("No summary to show. Make sure to set the title, quests, etc., before calling Show().");
            return;
        }

        chapterHeader.text = currentSummary.Title;
        chapterSummary.text = announcement.ToString();
        //Populate(currentSummary.Quests);

        canvas.SetActive(true);
        IsActive = true;
        StartCoroutine(FadeIn(currentSummary.FadeInDuration));
    }

    public void Hide()
    {
        SoundEffectManager.PlayButtonClick2();

        canvas.SetActive(false);
        IsActive = false;

        if (currentSummary.OnContinue != null)
            currentSummary.OnContinue.Invoke();

        StopAllCoroutines();

        currentSummary = null;
    }

    private void Populate(List<Quest> _quests)
    {
        int count = 0;
        foreach (Transform child in headerParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Quest quest in _quests)
        {
            count++;
            TextMeshProUGUI newQuestHeader = Instantiate(questHeader, headerParent);
            newQuestHeader.text = count.ToString() + ". " + quest.QuestTitle + ": " + quest.QuestWhatToDo;
        }
    }

    private IEnumerator FadeIn(float duration)
    {
        float startTime = Time.time;
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / duration);
            canvasGroup.alpha = alpha;

            yield return null;
        }
    }

    private void OnSummaryContinue()
    {
        // This method is called when the "Continue" button is tapped
        // Custom actions can be added here if needed
    }

    // Internal class to hold summary data
    private class ChapterSummary
    {
        public string Title;
        public List<Quest> Quests;
        public float FadeInDuration = 0.5f; // Default fade-in duration
        public UnityAction OnContinue;
    }
}
