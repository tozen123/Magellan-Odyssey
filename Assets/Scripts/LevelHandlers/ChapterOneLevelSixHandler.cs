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
               .SetMessage("Ang Magellan Expedition ay inorganisa upang samantalahin ang yaman ng Moluccas o Spice Islands.\r\n")
               .Show();


        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Para sa mga Espanyol, ang pagtuklas sa Moluccas o Spice Islands ay nangangahulugan ng walang limitasyong pagkalakal ng mga pampalasa na mahalaga sa pagpreserba ng karne.\r\n")
               .Show();


        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Ang mga pampalasa ay ang mga pangunahing sangkap sa pagtatayo ng imperyo ng Espanya.")
               .Show();





    }
    private bool showProv = false;

    private void Update()
    {
        UpdateCrateQuest();

        if(playerQuestHandler.IsQuestCompleted("Tipunin ang mga Probisyon sa Daungan"))
        {
            mag2.SetActive(true);
            mag1.SetActive(false);
        }
        
        if(playerQuestHandler.IsQuestCompleted("Special Quiz: Talk to the Ship Master"))
        {
            PlayerPrefs.SetString("Chapter1Level6", "COMPLETED");
            PlayerPrefs.Save();
        }
        

        if (playerQuestHandler.IsQuestCompleted("Mag-ulat kay Magellan"))
        {
            foreach(Transform child in playerInv.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }


        if (playerQuestHandler.IsQuestCompleted("Pumunta kay Magellan") && !showProv)
        {
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Hanapin ang ganitong uri ng kahon, ito ay espesyal para sa mga probisyon. Ito ay nakakalat sa paligid ng pantalan.")
                .SetImage(provCrateImage)
                .Show();
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Inakala ni Magellan na tatagal ang ekspedisyon ng dalawang taon, kaya ang mga barko ay siniguradong may probisyon para sa dalawang taon.")
                .Show();
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Ang bawat tao ay makakatanggap ng isang litrong rasyon araw-araw. Ang lahat ng probisyon ay pantay na ipapamahagi bawat dalawang araw simula sa kanilang pag-alis.")
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
        PlayerPrefs.SetString("Chapter1Level4", "COMPLETED");
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
            .SetTitle("Kabanata 6")
            .SetAnnounce("\n" +
                            "Ang paghahanda at mga probisyon para sa Magellan Expedition \n\n" +
                            "Ang limang barko ng Magellan Expedition \n\n" +
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
            if (currentQuest.QuestTitle == "Tipunin ang mga Probisyon sa Daungan")
            {
                Debug.Log("Gather MATCHED");

                foreach (Quest quest in quests) 
                {
                    if (quest.QuestTitle == "Tipunin ang mga Probisyon sa Daungan")
                    {
                        Debug.Log("Data");

                        quest.ChangeWhatToDo("Tipunin ang mga Probisyon sa Daungan", $"Tipunin ang mga Probisyon sa Daungan ({6 - remainingCrates}/6)");
                        playerQuestListManager.PopulateQuestList();
                        playerQuestHandler.DisplayQuest(quest);
                    }
                }

                if (remainingCrates == 0 && !isCrateQuestCompleted)
                {
                    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Tipunin ang mga Probisyon sa Daungan"));
                    PlayerQuestHandler.CompleteQuest("Tipunin ang mga Probisyon sa Daungan");

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
     
            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Tuklasin ang Daungan ng Seville"));
            PlayerQuestHandler.CompleteQuest("Tuklasin ang Daungan ng Seville");
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
              .SetMessage("Ang timbangan at mga dispenser ay gagamitin para sa pagrarasyon.")
              .Show();
        }

        if (targetCrates.Count == 1)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("Inatasan si Magellan na pangasiwaan ang pagrarasyon at pana-panahong suriin ang mga nakaimbak na probisyon. \n Si Magellan ay may awtoridad na bawasan ang rasyon kung kinakailangan.\r\n")
              .Show();
        }
        if (targetCrates.Count == 2)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("Ang pangunahing sangkap ng ika-16 na siglong diyeta sa dagat ay ang biskwit na maaaring tumagal sa mahabang paglalakbay. \n Kasama sa napreserbang pagkain ng ekspedisyon ang bagoong, tuyong isda, tuyong baboy, at keso na kaya pakainin ang maraming tao para sa mahabang paglalakbay.\r\n")
              .Show();
        }
        if (targetCrates.Count == 3)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("Ang mga tripulante ay nasiyahan magdiyeta ng atsarang gulay o salad.\n Ang ekspedisyon ay binigyan ng malaking bariles ng alak na nagkakahalaga ng humigit-kumulang 590,000 maravedis.\r\n")
              .Show();
        }
        if (targetCrates.Count == 4)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("Ang mga kargamento ng ekspedisyon ay binubuo rin ng mga armas, kagamitan, kalakalan, pamigay, medical kits, at mga record book para sa dokumentasyon at opisyal na mga ulat.\n")
              .Show();
        }
        if (targetCrates.Count == 5)
        {
            DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("Ang ekspedisyon ay nagdala rin ng pitong (7) baka at tatlong (3) baboy dahil ang karne ay mahalaga sa kultura ng Espanyol sa pagluluto.\n")
              .Show();
        }
    }
}
