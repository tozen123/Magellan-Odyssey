using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterOneLevelFiveHandler : MonoBehaviour
{
    [Header("Handlers")]
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private PlayerQuestHandler playerQuestHandler;
    [SerializeField] private PlayerQuestListManager playerQuestListManager;

    [Header("Quests")]
    [SerializeField] private List<Quest> quests;


    public BoxCollider explorationCollider;


    [Header("Special Quiz")]
    [SerializeField] private List<GameObject> targetChest;
    private bool isTargetChestCompleted = false;



    [Header("Chars0")]
    [SerializeField] private GameObject ruy0;
    [SerializeField] private GameObject mag0;
    [SerializeField] private GameObject mag;

    [Header("Chars1")]
    [SerializeField] private GameObject ruy1;
    [SerializeField] private GameObject mag1;

    [Header("Tracers")]
    public GameObject CH1L5_ToMagellan1;
    public GameObject CH1L5_MeetRuy1;

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
    void Dialogs()
    {


        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Magellan began drafting a new image of the world for his dream expedition with the help of pilots who specialized in Asian navigation.")
               .Show();

        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("In October 1517, Magellan left his country, renounced his nationality, and moved to Seville, Spain.")
               .Show();


        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("A few days after arriving in Seville, Magellan formalized his change of loyalty from Fernao de Magalhaes (Portuguese subject) to Fernando de Magallanes (Spanish subject).")
               .Show();


    }
    private void Start()
    {

        //IN_PROGRESS
        PlayerPrefs.SetString("Chapter1Level1", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level2", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level3", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level4", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level5", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level6", "IN_PROGRESS");
        PlayerPrefs.Save();

        mag0.SetActive(false);
        ruy0.SetActive(false);
        ruy1.SetActive(false);
        mag1.SetActive(false);
        foreach (Quest quest in quests)
        {
            playerQuestHandler.AddQuest(quest);
        }

        playerQuestListManager.PopulateQuestList();

        //ChapterLevelSummaryAnnounceControl.Instance
        //    .SetTitle("Chapter 1: Level 5")
        //    .SetQuests(quests)
        //    .SetFadeInDuration(0.5f)
        //    .OnContinue(() =>
        //    {
        //        OpenPlayerCanvas();
        //        Dialogs();
        //    })
        //    .Show();

        ChapterLevelSummaryAnnounceControl.Instance
            .SetTitle("Chapter 5")
            .SetAnnounce("\n" +
                            "Explore the city of Seville, Spain \n\n" +
                            "Magellan formed a family in Seville with Beatriz Barbosa \n\n" +
                            "Magellan and Falero’s expedition meeting with the royal council of Spain \n\n" +
                            "King Charles I of Spain’s consent and royal approval for the Magellan's Expedition \n\n" +
                            "The instructions or guidelines of King Charles I for the Magellan Expedition \n\n" +
                            "\n")
            .SetFadeInDuration(0.5f)
            .OnContinue(() =>
            {
                OpenPlayerCanvas();
                Dialogs();
            })
            .Show();

    }

    void OpenPlayerCanvas()
    {
        playerCanvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            explorationCollider.enabled = false;

            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Explore the City of Seville"));
            PlayerQuestHandler.CompleteQuest("Explore the City of Seville");
        }
    }

    private void Update()
    {
        UpdateFindChestQuest();


        if (playerQuestHandler.IsQuestCompleted("Go to Magellan"))
        {
            mag0.SetActive(true);
            ruy0.SetActive(true);
        }
        if (playerQuestHandler.IsQuestCompleted("Special Quest: Find The Missing Treasure In The City"))
        {
            ruy1.SetActive(true);
            mag1.SetActive(true);
        }

        if (playerQuestHandler.IsCurrentQuest("Go to Magellan"))
        {
            CH1L5_ToMagellan1.SetActive(true);
        }
        else
        {
            CH1L5_ToMagellan1.SetActive(false);

        }

        if (playerQuestHandler.IsCurrentQuest("Meet Ruy Falero"))
        {
            CH1L5_MeetRuy1.SetActive(true);
        }
        else
        {
            CH1L5_MeetRuy1.SetActive(false);

        }

    }

    void UpdateFindChestQuest()
    {
        int remainingChest = targetChest.Count;

        if (playerQuestHandler.Level1Quests.Count > 0)
        {
            Quest currentQuest = playerQuestHandler.Level1Quests[playerQuestHandler.currentQuestIndex];
            if (currentQuest.QuestTitle == "Special Quest: Find The Missing Treasure In The City")
            {
                foreach (Quest quest in quests)
                {
                    if (quest.QuestTitle == "Special Quest: Find The Missing Treasure In The City")
                    {
                        quest.ChangeWhatToDo("Special Quest: Find The Missing Treasure In The City", $"Find the 3 Chest ({3 - remainingChest}/3)");
                        playerQuestListManager.PopulateQuestList();
                        playerQuestHandler.DisplayQuest(quest);
                    }
                }

                if (remainingChest == 0 && !isTargetChestCompleted)
                {
                    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Special Quest: Find The Missing Treasure In The City"));
                    PlayerQuestHandler.CompleteQuest("Special Quest: Find The Missing Treasure In The City");


                    isTargetChestCompleted = true;
                }
            }
        }
    }

    public void OnChestFind(GameObject chest)
    {
        if (targetChest.Contains(chest))
        {
            targetChest.Remove(chest);
        }

        if (targetChest.Count == 2)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("The treasures contain trivia scrolls that will help with this chapter's short quiz and long quiz. \n During King Charles I’s  reign, Spain rose to imperial greatness \n King Charles I of Spain and King Manoel I of Portugal were related through Manoel's marriage to Charles' Aunt Isabel, then Aunt Maria, and then to Charles' sister Leonora.")
              .Show();
        }

        if (targetChest.Count == 1)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("The treasures contain trivia scrolls that will help with this chapter's short quiz and long quiz. \n King Manoel's marriage to King Charles I’s family was supposed to end the territorial dispute between Portugal and Spain but was revived with Magellan's discovery of the Moluccas or the Spice Islands.")
              .Show();
        }

        if (targetChest.Count == 0)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("The treasures contain trivia scrolls that will help with this chapter's short quiz and long quiz. \n Magellan showed a globe in his expedition presentation with the King while keeping the Strait passage in America a secret.")
              .Show();
        }

        SoundEffectManager.PlayReward();
    }
}
