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
    [SerializeField] private List<GameObject> targetCrates; // List of crates to be collected

    [Header("Magellans")]
    [SerializeField] private GameObject magellan1Camp;
    [SerializeField] private GameObject magellan2Gate;
    [SerializeField] private GameObject magellan3Battlefield;
    [SerializeField] private GameObject magellan4Soldier;

    [SerializeField] private GameObject borderAntiMissionCancel;

    private bool isQuestCompleted = false;
    private bool isBanditQuestCompleted = false;
    private bool isCrateQuestCompleted = false;



    private void Awake()
    {
        magellan2Gate.SetActive(false);
        magellan3Battlefield.SetActive(false);
        magellan4Soldier.SetActive(false);

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

    bool alreadyGoneToDummy = false;
    private void OnTriggerExit(Collider other)
    {
        if (!alreadyGoneToDummy)
        {
            if (other.gameObject.tag == "Player")
            {
                DialogMessagePrompt.Instance
                    .SetTitle("System Message")
                    .SetMessage("Test")
                    .SetImage(targetImage)
                    .Show();
                alreadyGoneToDummy = true;
            }
        }
    }

    private void Update()
    {
        UpdateCrateQuest();

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

        if (playerQuestHandler.IsQuestCompleted("To Battlefield"))
        {
            magellan3Battlefield.SetActive(true);
        }

        if (playerQuestHandler.IsQuestCompleted("Deliver the crates to Soldier"))
        {
            magellan4Soldier.SetActive(true);
        }
        if (playerQuestHandler.IsQuestCompleted("Talk to Magellan About the Issues"))
        {
            PlayerPrefs.SetString("Chapter1Level1", "COMPLETED");
            PlayerPrefs.SetString("Chapter1Level2", "COMPLETED");
            PlayerPrefs.SetString("Chapter1Level3", "IN_PROGRESS");
            PlayerPrefs.Save();
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

                    magellan2Gate.SetActive(true);

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

    // Updated Crate Quest Update Method
    private void UpdateCrateQuest()
    {
        int remainingCrates = targetCrates.Count; // Calculate remaining crates to be collected

        if (playerQuestHandler.Level1Quests.Count > 0)
        {
            Quest currentQuest = playerQuestHandler.Level1Quests[playerQuestHandler.currentQuestIndex];
            if (currentQuest.QuestTitle == "Battle Spoils")
            {
                Debug.Log("Battle Spoils MATCHED");
                foreach (Quest quest in quests)
                {
                    if (quest.QuestTitle == "Battle Spoils")
                    {
                        Debug.Log("Data");

                        quest.ChangeWhatToDo("Collect 3 Crates", $"Collect 3 crates ({3 - remainingCrates}/3)");
                        playerQuestListManager.PopulateQuestList();
                        playerQuestHandler.DisplayQuest(quest);
                    }
                }

                if (remainingCrates == 0 && !isCrateQuestCompleted)
                {
                    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Battle Spoils"));
                    PlayerQuestHandler.CompleteQuest("Battle Spoils");

                    isCrateQuestCompleted = true;
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

    // Updated method to handle crate collection
    public void OnCrateCollected(GameObject crate)
    {
        if (targetCrates.Contains(crate))
        {
            targetCrates.Remove(crate); // Remove the collected crate from the list
        }
    }
}
