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
    [SerializeField] private List<GameObject> targetDummies;
    [SerializeField] private List<GameObject> targetBandit;



    [SerializeField] private GameObject borderAntiMissionCancel;

    private bool isQuestCompleted = false;
    private bool isBanditQuestCompleted = false; // Added a flag for the bandit quest

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
            .SetTitle("Chapter 1: Level 2")
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
        UpdateDummyQuest();
        UpdateBanditQuest();

        if (playerQuestHandler.Level1Quests.Count > 0)
        {
            Quest currentQuest = playerQuestHandler.Level1Quests[playerQuestHandler.currentQuestIndex];
            if (currentQuest.QuestTitle == "Destroy the Dummies")
            {
                borderAntiMissionCancel.SetActive(true);
            }
            else
            {
                borderAntiMissionCancel.SetActive(false);
            }
        }
    }

    private void UpdateDummyQuest()
    {
        int remainingDummies = targetDummies.Count;

        if (playerQuestHandler.Level1Quests.Count > 0)
        {
            Quest currentQuest = playerQuestHandler.Level1Quests[playerQuestHandler.currentQuestIndex];
            if (currentQuest.QuestTitle == "Destroy the Dummies")
            {
                foreach (Quest quest in quests)
                {
                    if (quest.QuestTitle == "Destroy the Dummies")
                    {
                        quest.ChangeWhatToDo("Destroy the Dummies", $"Use your weapon to destroy the 5 dummies ({5 - remainingDummies}/5)");
                        playerQuestListManager.PopulateQuestList();
                        playerQuestHandler.DisplayQuest(quest);
                    }
                }

                if (remainingDummies == 0 && !isQuestCompleted)
                {
                    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Destroy the Dummies"));
                    PlayerQuestHandler.CompleteQuest("Destroy the Dummies");

                    isQuestCompleted = true;
                }
            }
        }
    }

    private void UpdateBanditQuest()
    {
        int remainingBandits = targetBandit.Count;

        if (playerQuestHandler.Level1Quests.Count > 0)
        {
            Quest currentQuest = playerQuestHandler.Level1Quests[playerQuestHandler.currentQuestIndex];
            if (currentQuest.QuestTitle == "Fight 5 Arabs")
            {
                foreach (Quest quest in quests)
                {
                    if (quest.QuestTitle == "Fight 5 Arabs")
                    {
                        quest.ChangeWhatToDo("Fight 5 Arabs", $"Defeat the 5 bandits ({5 - remainingBandits}/5)");
                        playerQuestListManager.PopulateQuestList();
                        playerQuestHandler.DisplayQuest(quest);
                    }
                }

                if (remainingBandits == 0 && !isBanditQuestCompleted)
                {
                    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Fight 5 Arabs"));
                    PlayerQuestHandler.CompleteQuest("Fight 5 Arabs");

                    isBanditQuestCompleted = true;
                }
            }
        }
    }

    public void OnBanditDestroyed(GameObject bandit)
    {
        if (targetBandit.Contains(bandit))
        {
            targetBandit.Remove(bandit);
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
