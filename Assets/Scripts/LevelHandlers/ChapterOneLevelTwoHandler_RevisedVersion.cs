using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterOneLevelTwoHandler_RevisedVersion : MonoBehaviour
{
    [Header("Handlers")]
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private PlayerQuestHandler playerQuestHandler;
    [SerializeField] private PlayerQuestListManager playerQuestListManager;

    [Header("Quests")]
    [SerializeField] private List<Quest> quests;

    [SerializeField] private Sprite targetImage;
    [SerializeField] private List<GameObject> targetDummies; // Change to a List<GameObject>

    private void Awake()
    {
        if (!playerCanvas)
        {
            playerCanvas = GameObject.FindWithTag("PlayerCanvas").gameObject;
        }

        playerCanvas.SetActive(false);

        if (!playerQuestHandler)
        {
            playerQuestHandler = GameObject.FindWithTag("Player").GetComponent<PlayerQuestHandler>();
        }

        if (!playerQuestListManager)
        {
            playerQuestListManager = GameObject.FindWithTag("Player").GetComponent<PlayerQuestListManager>();
        }
    }

    private void Start()
    {
        foreach (Quest quest in quests)
        {
            playerQuestHandler.AddQuest(quest);
        }

        playerQuestListManager.PopulateQuestList();

        ChapterLevelSummaryAnnounceControl.Instance
            .SetTitle("Chapter 1: Level 1")
            .SetQuests(quests)
            .SetFadeInDuration(0.5f)
            .OnContinue(() =>
            {
                OpenPlayerCanvas();
            })
            .Show();
    }

    void OpenPlayerCanvas()
    {
        playerCanvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Test")
                .SetImage(targetImage)
                .Show();
        }
    }

    private void Update()
    {
        int remainingDummies = targetDummies.Count;
        foreach (Quest quest in quests)
        {
            if (quest.QuestTitle == "Destroy the Dummies")
            {
                quest.ChangeWhatToDo("Destroy the Dummies", $"Use your weapon to destroy the 5 dummies ({5 - remainingDummies}/5)");

                playerQuestListManager.PopulateQuestList();
                playerQuestHandler.DisplayQuest(quest);
            }
        }
        foreach (Quest quest in quests)
        {
            if (quest.QuestTitle == "Destroy the Dummies")
            {
                quest.ChangeWhatToDo("Destroy the Dummies", $"Use your weapon to destroy the 5 dummies ({5 - remainingDummies}/5)");
            }
        }

        if (remainingDummies == 0)
        {
            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Destroy the Dummies"));
            PlayerQuestHandler.CompleteQuest("Destroy the Dummies");
        }


    }

    public void OnDummyDestroyed(GameObject dummy)
    {
        if (targetDummies.Contains(dummy))
        {
            targetDummies.Remove(dummy); 
        }
    }
}
