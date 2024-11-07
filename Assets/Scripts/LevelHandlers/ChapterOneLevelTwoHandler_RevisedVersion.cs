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
    [SerializeField] private Sprite banditImage;
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

    [Header("Tracers")]

    public GameObject CH1L2_GotoCenterTrainingField;
    public GameObject CH1L2_GotoEntrance;
    public GameObject CH1L2_ToDeliver;
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

        //ChapterLevelSummaryAnnounceControl.Instance
        //    .SetTitle("Chapter 1: Level 2")
        //    .SetQuests(quests)
        //    .SetFadeInDuration(0.5f)
        //    .OnContinue(() =>
        //    {
        //        OpenPlayerCanvas();
        //    })
        //    .Show();


        ChapterLevelSummaryAnnounceControl.Instance
            .SetTitle("Kabanata 2")
            .SetAnnounce("\n" +
                            "- Ang pagsasanay para sa labanan sa Portugal Outposts \n\n" +
                            "- Ang malubhang pinsala sa tuhod at panghabang buhay na pagka-pilay ni Magellan \n\n" +
                            "- Ang alitan sa pagitan ni Magellan at ng mga sundalo sa pabuya (war spoils) ng digmaan \n\n" +
                            "\n")
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
                    .SetMessage("Look for this object around the outpost. This objects can be destroy and attacked safely.")
                    .SetImage(targetImage)
                    .Show();
                alreadyGoneToDummy = true;
            }
        }
    }
    private bool hasShownBanditDialog = false;
    public GameObject soldier;
    private void Update()
    {
        UpdateCrateQuest();

        UpdateDummyQuest();
        UpdateBanditQuest();

        if (playerQuestHandler.Level1Quests.Count > 0)
        {
            Quest currentQuest = playerQuestHandler.Level1Quests[playerQuestHandler.currentQuestIndex];
            if (currentQuest.QuestTitle == "Sirain ang mga Practice Dummies")
            {
                borderAntiMissionCancel.SetActive(true);
            }
            else
            {
                borderAntiMissionCancel.SetActive(false);
            }
        }

        if (playerQuestHandler.IsCurrentQuest("Ang Digmaan sa Outposts") && !hasShownBanditDialog)
        {
            magellan3Battlefield.SetActive(true);

            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Look for the bandits around the outside of the outpost by following the dirt roads. They are mostly in group so be careful!")
                .SetImage(banditImage)
                .Show();

            hasShownBanditDialog = true; 
        }

        if (playerQuestHandler.IsQuestCompleted("Ipamahagi ang mga Pabuya sa mga Sundalo"))
        {
            magellan4Soldier.SetActive(true);
        }
        if (playerQuestHandler.IsQuestCompleted("Ipagtanggol si Magellan laban sa mga Sundalo"))
        {
            PlayerPrefs.SetString("Chapter1Level1", "COMPLETED");
            PlayerPrefs.SetString("Chapter1Level2", "COMPLETED");
            PlayerPrefs.SetString("Chapter1Level3", "IN_PROGRESS");
            PlayerPrefs.Save();
        }
        
        if(playerQuestHandler.IsCurrentQuest("Pumunta kay Magellan sa Training Field"))
        {
            CH1L2_GotoCenterTrainingField.SetActive(true);
        } 
        else
        {
            CH1L2_GotoCenterTrainingField.SetActive(false);

        }
        if (playerQuestHandler.IsCurrentQuest("Ang Digmaan sa Outposts"))
        {
            if (!showGuide1)
            {
                DialogMessagePrompt.Instance
                    .SetTitle("System Message")
                    .SetMessage("Some of the quest can be completed by approaching only the said character in the information of the quest.")
                    .Show();
                showGuide1 = true;
            }
            
            CH1L2_GotoEntrance.SetActive(true);
        }
        else
        {
            CH1L2_GotoEntrance.SetActive(false);

        }

        if (playerQuestHandler.IsCurrentQuest("Ipamahagi ang mga Pabuya sa mga Sundalo"))
        {
            soldier.SetActive(true);

        }
        else
        {
            soldier.SetActive(false);

        }


    }
    bool showGuide1 = false;
    private void UpdateDummyQuest()
    {
        int remainingDummies = targetDummies.Count;

        if (playerQuestHandler.Level1Quests.Count > 0)
        {
            Quest currentQuest = playerQuestHandler.Level1Quests[playerQuestHandler.currentQuestIndex];
            if (currentQuest.QuestTitle == "Sirain ang mga Practice Dummies")
            {
                foreach (Quest quest in quests)
                {
                    if (quest.QuestTitle == "Sirain ang mga Practice Dummies")
                    {
                        quest.ChangeWhatToDo("Sirain ang mga Practice Dummies", $"Gamitin ang iyong weapon para sirain ang 5 na dummies ({5 - remainingDummies}/5)");
                        playerQuestListManager.PopulateQuestList();
                        playerQuestHandler.DisplayQuest(quest);
                    }
                }

                if (remainingDummies == 0 && !isQuestCompleted)
                {
                    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Sirain ang mga Practice Dummies"));
                    PlayerQuestHandler.CompleteQuest("Sirain ang mga Practice Dummies");

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
            if (currentQuest.QuestTitle == "Kalabanin ang mga Arabo")
            {
                foreach (Quest quest in quests)
                {
                    if (quest.QuestTitle == "Kalabanin ang mga Arabo")
                    {
                        quest.ChangeWhatToDo("Kalabanin ang mga Arabo", $"Kalabanin ang 5 na Arabo ({5 - remainingBandits}/5)");
                        playerQuestListManager.PopulateQuestList();
                        playerQuestHandler.DisplayQuest(quest);
                    }
                }

                if (remainingBandits == 0 && !isBanditQuestCompleted)
                {
                    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Kalabanin ang mga Arabo"));
                    PlayerQuestHandler.CompleteQuest("Kalabanin ang mga Arabo");

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
            if (currentQuest.QuestTitle == "Mini-Game: Kolektahin ang mga Crates")
            {
                Debug.Log("Battle Spoils MATCHED");
                foreach (Quest quest in quests)
                {
                    if (quest.QuestTitle == "Mini-Game: Kolektahin ang mga Crates")
                    {
                        Debug.Log("Data");

                        quest.ChangeWhatToDo("Mini-Game: Kolektahin ang mga Crates", $"Kolektahin ang 3 na Crates ({3 - remainingCrates}/3)");
                        playerQuestListManager.PopulateQuestList();
                        playerQuestHandler.DisplayQuest(quest);
                    }
                }

                if (remainingCrates == 0 && !isCrateQuestCompleted)
                {
                    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Mini-Game: Kolektahin ang mga Crates"));
                    PlayerQuestHandler.CompleteQuest("Mini-Game: Kolektahin ang mga Crates");

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
