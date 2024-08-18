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
        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Magellan's childhood was mysterious; historians assumed his life was no different from that of ordinary children at his age.")
               .Show();

        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("However, Magellan lost his parents when he was around 10-12 years old. Fortunately, his cousins brought him to Lisbon, the capital of the expanding Portuguese empire.")
               .OnClose(OpenPlayerCanvas)
               .Show();

        //playerQuestHandler.AddQuest(new Quest("Explore the City of Lisbon",
        //               "In the 15th century, seafaring offered great benefits and rewards such as knighthood promotion, selective tax exemption, and the grant of small annual pensions. In fact, Magellan's relatives mostly pursued a maritime career.",
        //               "Run around the city",
        //               25));

        //playerQuestHandler.AddQuest(new Quest("Meet Ferdinand Magellan",
        //                 "Magellan, 25 years old, was a seafarer recognized for his courage and heroism during the Portuguese dominion expansion in India against local Moro rulers. " +
        //                 "Magellan was then promoted as captain around 1505-1509.",
        //                 "Interact with Magellan",
        //                 40));

        foreach (Quest quest in quests)
        {
            playerQuestHandler.AddQuest(quest);
        }

        playerQuestListManager.PopulateQuestList();
    }

    void OpenPlayerCanvas()
    {
        playerCanvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerQuestHandler.CompleteQuest("Explore the City of Lisbon");
        }
    }
}
