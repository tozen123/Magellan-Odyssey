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

        foreach (Quest quest in quests)
        {
            playerQuestHandler.AddQuest(quest);
        }

        playerQuestListManager.PopulateQuestList();

        ChapterLevelSummaryAnnounceControl.Instance
    .SetTitle("Chapter 1: Level 5")
    .SetQuests(quests)
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
}
