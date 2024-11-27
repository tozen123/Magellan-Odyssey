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
               .SetMessage("Ilang araw pagkatapos dumating sa Seville, pormal na pinaltan ni Magellan ang kanyang pangalan, mula Fernao de Magalhaes (Portuges) naging Fernando de Magallanes (Espanyol), bilang katapatan sa Espanya.")
               .Show();

        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Pagkalipas ng isang taon, pinakasalan ni Magellan si Beatriz Barbosa. Si Beatriz ay anak ng kanyang host na si Diogo Barbosa, na nag-imbita sa kanya na manatili sa kanilang tirahan. ")
               .Show();


        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Nagkaanak si Beatriz ng isang lalaki na pinangalanan nilang Rodrigo, na anim (6) na buwang gulang noong panahon ng ekspedisyon.")
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
            .SetTitle("Kabanata 5")
            .SetAnnounce("\n" +
                            " - Tuklasin ang lungsod ng Seville, Spain \n\n" +
                            " - Ang pamilya ni Magellan kasama si Beatriz Barbosa \n\n" +
                            " - Ang pagpupulong nina Magellan at Falero sa maharlikang konseho ng Espanya para sa ekspedisyon \n\n" +
                            " - Ang pahintulot ni Haring Charles I ng Espanya para sa Magellan Expedition \n\n" +
                            " - Ang mga tagubilin o alituntunin ni Haring Charles I kay Magellan para sa ekspedisyon  \n\n" +
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

            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Tuklasin ang lungsod ng Seville, Spain"));
            PlayerQuestHandler.CompleteQuest("Tuklasin ang lungsod ng Seville, Spain");
        }
    }

    private void Update()
    {
        UpdateFindChestQuest();


        if (playerQuestHandler.IsQuestCompleted("Pumunta kay Magellan"))
        {
            mag0.SetActive(true);
            ruy0.SetActive(true);
        }
        if (playerQuestHandler.IsQuestCompleted("Special Quest: Hanapin ang mga Kaban ng Kayamanan"))
        {
            ruy1.SetActive(true);
            mag1.SetActive(true);
        }

        if (playerQuestHandler.IsCurrentQuest("Pumunta kay Magellan"))
        {
            CH1L5_ToMagellan1.SetActive(true);
        }
        else
        {
            CH1L5_ToMagellan1.SetActive(false);

        }

        if (playerQuestHandler.IsCurrentQuest("Kilalanin si Ruy Falero"))
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
            if (currentQuest.QuestTitle == "Special Quest: Hanapin ang mga Kaban ng Kayamanan")
            {
                foreach (Quest quest in quests)
                {
                    if (quest.QuestTitle == "Special Quest: Hanapin ang mga Kaban ng Kayamanan")
                    {
                        quest.ChangeWhatToDo("Special Quest: Hanapin ang mga Kaban ng Kayamanan", $"Hanapin ang tatlong kaban ({3 - remainingChest}/3)");
                        playerQuestListManager.PopulateQuestList();
                        playerQuestHandler.DisplayQuest(quest);
                    }
                }

                if (remainingChest == 0 && !isTargetChestCompleted)
                {
                    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Special Quest: Hanapin ang mga Kaban ng Kayamanan"));
                    PlayerQuestHandler.CompleteQuest("Special Quest: Hanapin ang mga Kaban ng Kayamanan");


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
              .SetMessage("Si Haring Charles I ng Espanya at Haring Manoel I ng Portugal ay may kaugnayan sa pamamagitan ng kasal ni Manoel sa tiyahin ni Charles na si Isabel, sunod sa kanyang tiyahin Maria, at sa kapatid niyang si Leonora.")
              .Show();
        }

        if (targetChest.Count == 1)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("Ang kasal ni Haring Manoel I sa pamilya ni Haring Charles I ang inaasahang tatapos sa teritoryal na hidwaan sa pagitan ng Portugal at Espanya ngunit muling umusbong sa pagtuklas ni Magellan ng Moluccas o Spice Islands.")
              .Show();
        }

        if (targetChest.Count == 0)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("Ipinakita ni Magellan ang isang globo sa kanyang presentasyon ng ekspedisyon kay Haring Charles I habang pinapanatiling lihim ang daanan ng Strait sa Amerika.")
              .Show();
        }

        SoundEffectManager.PlayReward();
    }
}
