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

        ChapterLevelSummaryAnnounceControl.Instance
    .SetTitle("Chapter 1: Level 6")
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
