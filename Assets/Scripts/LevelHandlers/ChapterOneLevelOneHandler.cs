using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterOneLevelOneHandler : MonoBehaviour
{
    [Header("Handlers")]
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private PlayerQuestHandler playerQuestHandler;
    [SerializeField] private PlayerQuestListManager playerQuestListManager;

    [Header("Quests")]
    [SerializeField] private List<Quest> quests;

    public Sprite legendMinimap;

    public GameObject CH1L1_ToMagellan;
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
    
    }
    void Dialogs()
    {


        //DialogMessagePrompt.Instance
        //       .SetTitle("System Message")
        //       .SetMessage("Magellan's childhood was mysterious; historians assumed his life was no different from that of ordinary children at his age.")
        //       .Show();

        //DialogMessagePrompt.Instance
        //       .SetTitle("System Message")
        //       .SetMessage("However, Magellan lost his parents when he was around 10-12 years old. Fortunately, his cousins brought him to Lisbon, the capital of the expanding Portuguese empire.")
        //       .OnClose(OpenPlayerCanvas)
        //       .Show();

      

       
    }
    private void Start()
    {
    
        foreach (Quest quest in quests)
        {
            playerQuestHandler.AddQuest(quest);
        }

        playerQuestListManager.PopulateQuestList();

        //ChapterLevelSummaryAnnounceControl.Instance
        //    .SetTitle("Chapter 1: Level 1")
        //    .SetQuests(quests)
        //    .SetFadeInDuration(0.5f)
        //    .OnContinue(() =>
        //    {
        //        OpenPlayerCanvas();
        //        Dialogs();
        //    })
        //    .Show();

        ChapterLevelSummaryAnnounceControl.Instance
            .SetTitle("Chapter 1")
            .SetAnnounce("\n" +
            "Tuklasin ang bayan ni Magellan - Lisbon, Portugal \n\n" +
            "Kilalanin si Ferdinand Magellan, ang Kapitan ng Ekspedisyon \n\n" +
            "Ang isyu sa pagtaas ng sweldo sa pagitcan nina Magellan at King Manoel I ng Portugal \n\n" +
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
        if (other.gameObject.tag == "Player")
        {
        

            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Explore the City of Lisbon"));

            PlayerQuestHandler.CompleteQuest("Explore the City of Lisbon");
            CH1L1_ToMagellan.SetActive(true);


            DialogMessagePrompt.Instance
                    .SetTitle("System Message")
                    .SetMessage("This is the game minimap legend")
                    .SetImage(legendMinimap)
                    .Show();

        }
    }
}
