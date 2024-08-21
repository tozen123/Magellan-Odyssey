using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerQuestHandler : MonoBehaviour
{
    [Header("Toggling Attributes")]
    [SerializeField] private RectTransform QUEST_HOLDER;
    [SerializeField] private Vector2 QUEST_HOLDER_ORIGINAL_SIZE;
    [SerializeField] private Vector2 QUEST_HOLDER_ORIGINAL_ANCHOR_POSITION;
    [SerializeField] private Vector2 QUEST_HOLDER_OFF_SIZE;
    [SerializeField] private Vector2 QUEST_HOLDER_OFF_ANCHOR_POSITION;
    [SerializeField] private TextMeshProUGUI QUEST_BUTTON_TOGGLE;

    [Header("Quest Details References")]
    [SerializeField] private GameObject QUEST_DETAILS_TEXT;

    [Header("Quest Interface References")]
    [SerializeField] private TextMeshProUGUI QUEST_TITLE;
    [SerializeField] private TextMeshProUGUI QUEST_DESCRIPTION;
    [SerializeField] private TextMeshProUGUI QUEST_TASK;
    [SerializeField] private TextMeshProUGUI QUEST_POINTS;

    [Header("Animation Attributes")]
    [SerializeField] private float animationDuration = 0.5f;

    [Header("Quest List")]
    public List<Quest> Level1Quests = new List<Quest>();

    private bool isExpanded = true;
    private Coroutine animationCoroutine;
    private int currentQuestIndex = 0;

    private static PlayerQuestHandler _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple PlayerQuestHandler instances found!");
        }
        Toggle();
    }

    void Start()
    {
        QUEST_HOLDER_ORIGINAL_SIZE = QUEST_HOLDER.sizeDelta;
        QUEST_HOLDER_ORIGINAL_ANCHOR_POSITION = QUEST_HOLDER.anchoredPosition;

        if (Level1Quests.Count > 0)
        {
            DisplayQuest(Level1Quests[currentQuestIndex]);
        }
    }

    public void Toggle()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        if (isExpanded)
        {
            animationCoroutine = StartCoroutine(AnimateQuestHolder(QUEST_HOLDER_OFF_SIZE, QUEST_HOLDER_OFF_ANCHOR_POSITION, false));
            QUEST_BUTTON_TOGGLE.text = "Open Quest Tab";
        }
        else
        {
            animationCoroutine = StartCoroutine(AnimateQuestHolder(QUEST_HOLDER_ORIGINAL_SIZE, QUEST_HOLDER_ORIGINAL_ANCHOR_POSITION, true));
            QUEST_BUTTON_TOGGLE.text = "Close Quest Tab";

        }

        isExpanded = !isExpanded;
    }

    private IEnumerator AnimateQuestHolder(Vector2 targetSize, Vector2 targetPosition, bool showDetails)
    {
        if (!showDetails)
        {
            QUEST_DETAILS_TEXT.SetActive(showDetails);
        }

        Vector2 initialSize = QUEST_HOLDER.sizeDelta;
        Vector2 initialPosition = QUEST_HOLDER.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);

            QUEST_HOLDER.sizeDelta = Vector2.Lerp(initialSize, targetSize, t);
            QUEST_HOLDER.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, t);

            yield return null;
        }

        QUEST_HOLDER.sizeDelta = targetSize;
        QUEST_HOLDER.anchoredPosition = targetPosition;
        if (showDetails)
        {
            QUEST_DETAILS_TEXT.SetActive(showDetails);
        }
    }

    public void AddQuest(Quest quest)
    {
        Level1Quests.Add(quest);
        if (Level1Quests.Count == 1)
        {
            DisplayQuest(quest);
        }
    }
    public static int GetQuestADPPoints(string title)
    {
        if (_instance == null || _instance.Level1Quests.Count == 0) return 0;

        Quest currentQuest = _instance.Level1Quests[_instance.currentQuestIndex];

        if (currentQuest.QuestTitle.Equals(title, System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("okay");
            return currentQuest.GetQuestADPPoints();


        }
        else
        {
            Debug.Log("not found");
            return 0;
        }
        
    }
    public static void CompleteQuest(string title)
    {
        if (_instance == null || _instance.Level1Quests.Count == 0) return;

        Quest currentQuest = _instance.Level1Quests[_instance.currentQuestIndex];

        if (currentQuest.QuestTitle.Equals(title, System.StringComparison.OrdinalIgnoreCase))
        {
            currentQuest.MarkAsCompleted();
            _instance.currentQuestIndex++;

            QuestPopCardManager.Instance
              .SetTitle("Quest Completed\n" + title)
              .SetMessage(currentQuest.QuestDescription + "\n\n" + "You earned " + currentQuest.QuestADPPoints + " Academic Points")
              .Show();

            if (_instance.currentQuestIndex >= _instance.Level1Quests.Count)
            {
                Debug.Log("All quests completed!");
                _instance.QUEST_DETAILS_TEXT.SetActive(false);
            }
            else
            {
                _instance.DisplayQuest(_instance.Level1Quests[_instance.currentQuestIndex]);
            }
        }
        else
        {
            Debug.LogWarning("The quest title provided does not match the current quest.");
        }
    }

    private void DisplayQuest(Quest quest)
    {
        QUEST_TITLE.text = quest.QuestTitle;
        //QUEST_DESCRIPTION.text = quest.QuestDescription;
        QUEST_TASK.text = "Task: " +  quest.QuestWhatToDo;
        QUEST_POINTS.text = "Adventure Points: " + quest.QuestADPPoints.ToString();
    }
}
