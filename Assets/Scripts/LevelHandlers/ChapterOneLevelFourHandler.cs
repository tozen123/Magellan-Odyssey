using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterOneLevelFourHandler : MonoBehaviour
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
               .SetMessage("Magellan underwent a humiliating investigation in Morocco. After the court cleared him of the accusations, Magellan returned to Lisbon.")
               .Show();

     






    }
    private void Start()
    {

        //IN_PROGRESS
        PlayerPrefs.SetString("Chapter1Level1", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level2", "COMPLETED");
        PlayerPrefs.SetString("Chapter1Level3", "COMPLETED"); 
        PlayerPrefs.SetString("Chapter1Level4", "IN_PROGRESS"); 
        PlayerPrefs.Save();
        foreach (Quest quest in quests)
        {
            playerQuestHandler.AddQuest(quest);
        }

        playerQuestListManager.PopulateQuestList();

        ChapterLevelSummaryAnnounceControl.Instance
    .SetTitle("Chapter 1: Level 4")
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
