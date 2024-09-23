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
    private void Dialogs()
    {
        DialogMessagePrompt.Instance
          .SetTitle("System Message")
          .SetMessage("You accompanied Ferdinand Magellan to the Royal Palace.")
          .OnClose(chapterOneLevelTwoHandlerA.Game)
          .Show();
    }
    private void Start()
    {
        
        foreach (Quest quest in quests)
        {
            playerQuestHandler.AddQuest(quest);
        }

        playerQuestListManager.PopulateQuestList();
        ChapterLevelSummaryAnnounceControl.Instance
           .SetTitle("Chapter 1: Level 3")
           .SetQuests(quests)
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
