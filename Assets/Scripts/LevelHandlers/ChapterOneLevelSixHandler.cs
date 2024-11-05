using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterOneLevelSixHandler : MonoBehaviour
{
    [Header("Handlers")]
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private PlayerQuestHandler playerQuestHandler;
    [SerializeField] private PlayerQuestListManager playerQuestListManager;

    [Header("Quests")]
    [SerializeField] private List<Quest> quests;

    public GameObject playerInv;
    public Sprite provCrateImage;

    public GameObject mag1;
    public GameObject mag2;
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
               .SetMessage("The Magellan expedition was organized to exploit the wealth offered by the abundant resources of the Moluccas or Spice Islands.")
               .Show();


        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("For the Spaniards, the discovery of the Moluccas or Spice Islands meant unlimited access to spices crucial in preserving meat.")
               .Show();


        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("The spices were the major ingredients of Spain's empire-building projects.")
               .Show();





    }
    private bool showProv = false;

    private void Update()
    {
        UpdateCrateQuest();

        if(playerQuestHandler.IsQuestCompleted("Gather the Provisions in the Port"))
        {
            mag2.SetActive(true);
            mag1.SetActive(false);
        }
        
        if(playerQuestHandler.IsQuestCompleted("Special Quiz: Talk to the Ship Master"))
        {
            PlayerPrefs.SetString("Chapter1Level6", "COMPLETED");
            PlayerPrefs.Save();
        }
        

        if (playerQuestHandler.IsQuestCompleted("Report to Magellan"))
        {
            foreach(Transform child in playerInv.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        
        if (playerQuestHandler.IsQuestCompleted("Go to Magellan") && !showProv)
        {

            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Look for this type of crate, it is specialized for provisions. This is scattered around the port.")
                .SetImage(provCrateImage)
                .Show();
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Magellan predicted that the expedition would last for two years, so the ships were provisioned for two years.")
                .Show();
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Each man was to receive a daily ration of one liter. All provisions were to be equally distributed every two days beginning from their departure.")
                .Show();
            showProv = true;
        }
    }
    private void Start()
    {

        //IN_PROGRESS
        PlayerPrefs.SetString("Chapter1Level1", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level2", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level3", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level5", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level6", "IN_PROGRESS");
        PlayerPrefs.Save();
        foreach (Quest quest in quests)
        {
            playerQuestHandler.AddQuest(quest);
        }

        playerQuestListManager.PopulateQuestList();

        //ChapterLevelSummaryAnnounceControl.Instance
        //    .SetTitle("Chapter 1: Level 6")
        //    .SetQuests(quests)
        //    .SetFadeInDuration(0.5f)
        //    .OnContinue(() =>
        //    {
        //        OpenPlayerCanvas();
        //        Dialogs();
        //    })
        //    .Show();


        ChapterLevelSummaryAnnounceControl.Instance
            .SetTitle("Chapter 6")
            .SetAnnounce("\n" +
                            "The preparation and provisions of the Magellan Expedition \n\n" +
                            "The five ships of the Magellan Expedition \n\n" +
                            "\n")
            .SetFadeInDuration(0.5f)
            .OnContinue(() =>
            {
                OpenPlayerCanvas();
                Dialogs();
            })
            .Show();
    }
    [SerializeField] private List<GameObject> targetCrates; // List of crates to be collected

    private bool isCrateQuestCompleted = false;

    private void UpdateCrateQuest()
    {
        int remainingCrates = targetCrates.Count; // Calculate remaining crates to be collected

        if (playerQuestHandler.Level1Quests.Count > 0)
        {
            Quest currentQuest = playerQuestHandler.Level1Quests[playerQuestHandler.currentQuestIndex];
            if (currentQuest.QuestTitle == "Gather the Provisions in the Port")
            {
                Debug.Log("Gather MATCHED");

                foreach (Quest quest in quests) 
                {
                    if (quest.QuestTitle == "Gather the Provisions in the Port")
                    {
                        Debug.Log("Data");

                        quest.ChangeWhatToDo("Gather the Provisions in the Port", $"Gather the Provisions in the Port({6 - remainingCrates}/6)");
                        playerQuestListManager.PopulateQuestList();
                        playerQuestHandler.DisplayQuest(quest);
                    }
                }

                if (remainingCrates == 0 && !isCrateQuestCompleted)
                {
                    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Gather the Provisions in the Port"));
                    PlayerQuestHandler.CompleteQuest("Gather the Provisions in the Port");

                    isCrateQuestCompleted = true;
                }
            }
        }
    }
    void OpenPlayerCanvas()
    {
        playerCanvas.SetActive(true);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
     
            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Explore the Port in Seville"));
            PlayerQuestHandler.CompleteQuest("Explore the Port in Seville");
        }
    }

    public void OnCrateCollected(GameObject crate)
    {
        if (targetCrates.Contains(crate))
        {
            targetCrates.Remove(crate); // Remove the collected crate from the list
        }

        if (targetCrates.Count == 0)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("Magellan was tasked to supervise the rationing and periodically check the stored provisions.")
              .Show();
        }

        if (targetCrates.Count == 1)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage(" Magellan had the authority to reduce the ration as deemed necessary. \n The basic ingredient of the 16th-century nautical diet was a biscuit that could endure long voyages.")
              .Show();
        }
        if (targetCrates.Count == 2)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("The expedition's preserved food included hefty anchovies, dried fish, dried pork, and cheese to feed many people, even in small quantities for long voyages. \n The crew enjoyed a heavy vegetable diet, either in pickled or salad form.")
              .Show();
        }
        if (targetCrates.Count == 3)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("The expedition was provisioned with substantial barrels of wine costing around 590,000 maravedis.")
              .Show();
        }
        if (targetCrates.Count == 4)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("The expedition’s cargo also consisted of weapons, equipment, trade, giveaways, medical kits, and record books for documentation and official reports.")
              .Show();
        }
        if (targetCrates.Count == 5)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("The expedition also carried 7 cows and 3 pigs since meat was vital in Spanish culinary culture.")
              .Show();
        }
    }
}
