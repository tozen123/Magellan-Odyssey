using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterOneLevelTwoHandlerB : MonoBehaviour
{
    [Header("Handlers")]
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private PlayerQuestHandler playerQuestHandler;
    [SerializeField] private PlayerQuestListManager playerQuestListManager;

    [Header("Quests")]
    [SerializeField] private List<Quest> quests;


    [Header("Modified")]
    [SerializeField] private ChapterOneLevelTwoHandlerA chapterOneLevelTwoHandlerA;


    public SoldierGuardChaser guardChaser;
    public SoldierGuardChaser guardChaser1;
    public SoldierGuardChaser guardChaser2;
    public SoldierGuardChaser guardChaser3;
    public SoldierGuardChaser guardChaser4;
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
    

    private void Update()
    {
       
        if (playerQuestHandler.IsCurrentQuest("Mini-Game: Lutasin ang Parirala"))
        {
            guardChaser.startChasing = true;
            guardChaser1.startChasing = true;
            guardChaser2.startChasing = true;
            guardChaser3.startChasing = true;
            guardChaser4.startChasing = true;
        }

        if (playerQuestHandler.IsQuestCompleted("Mini-Game: Lutasin ang Parirala"))
        {
            guardChaser.startChasing = false;
            guardChaser1.startChasing = false;
            guardChaser2.startChasing = false;
            guardChaser3.startChasing = false;
            guardChaser4.startChasing = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (playerQuestHandler.IsCurrentQuest("Umalis sa Palasyo"))
        {
            if (other.gameObject.tag == "Player")
            {

                PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Umalis sa Palasyo"));

                PlayerQuestHandler.CompleteQuest("Umalis sa Palasyo");

                DialogMessagePrompt.Instance
                  .SetTitle("System Message")
                  .SetMessage("Pagkatapos mong subukang lumabas ng palasyo, hinabol ka ng mga guwardiya. Gawin mo ang iyong makakaya upang matakasan sila at pumunta sa labasan.")
                  .Show();

                DialogMessagePrompt.Instance
                 .SetTitle("System Message")
                 .SetMessage("Ang exit ay kung saan kayo nag simula ni magellan")
                 .Show();
            }
        }
        
            
    }
    private void Dialogs()
    {
        DialogMessagePrompt.Instance
          .SetTitle("System Message")
          .SetMessage("Sinamahan mo si Ferdinand Magellan sa Palasyo .")
          .Show();

        DialogMessagePrompt.Instance
          .SetTitle("System Message")
          .SetMessage("Hindi pinansin ni Magellan ang mga paratang mula sa mga sundalo; sa halip, pumunta siya sa Lisbon upang makipagkita sa Hari nang walang paunang abiso.")
          .OnClose(chapterOneLevelTwoHandlerA.Game)
          .Show();
    }
    private void Start()
    {
        PlayerPrefs.SetString("Chapter1Level1", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level2", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level3", "IN_PROGRESS");
        PlayerPrefs.Save();

        foreach (Quest quest in quests)
        {
            playerQuestHandler.AddQuest(quest);
        }

        playerQuestListManager.PopulateQuestList();
        //ChapterLevelSummaryAnnounceControl.Instance
        //   .SetTitle("Chapter 1: Level 3")
        //   .SetQuests(quests)
        //   .SetFadeInDuration(0.5f)
        //   .OnContinue(() =>
        //   {
        //       OpenPlayerCanvas();
        //       Dialogs();
        //   })
        //   .Show();


        ChapterLevelSummaryAnnounceControl.Instance
           .SetTitle("Chapter 3")
           .SetAnnounce("\n" +
                            "- Kilalanin si Haring Manoel I ng Portugal \n\n" +
                            "- Ang mapagmatigas at walang utang na loob na pagkatao ni Haring Manoel I \n\n" +
                            "\n")
           .SetFadeInDuration(0.5f)
           .OnContinue(() =>
           {
               OpenPlayerCanvas();
               Dialogs();
           })
           .Show();

        //OpenPlayerCanvas();
    }

    void OpenPlayerCanvas()
    {
        playerCanvas.SetActive(true);
    }
}
