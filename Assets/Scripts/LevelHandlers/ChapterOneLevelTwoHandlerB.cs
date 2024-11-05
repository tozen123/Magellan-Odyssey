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
          .Show();

        DialogMessagePrompt.Instance
          .SetTitle("System Message")
          .SetMessage("Magellan ignored the accusations from the soldiers; instead, he went to Lisbon to have an audience with the King without prior notice.")
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
                            "Kilalanin si Haring Manoel I ng Portugal \n\n" +
                            "Ang mapagmatigas at walang utang na loob na pagkatao ni Haring Manoel I \n\n" +
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
