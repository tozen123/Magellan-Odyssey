using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch1L3Handler : MonoBehaviour
{
    [Header("Handlers")]
    [SerializeField] private PlayerQuestHandler playerQuestHandler;
    [SerializeField] private PlayerQuestListManager playerQuestListManager;

    public GameObject semiwall;
    private void Awake()
    {
        



        if (!playerQuestHandler)
        {
            playerQuestHandler = GameObject.FindWithTag("Player").GetComponent<PlayerQuestHandler>();

        }


        if (!playerQuestListManager)
        {
            playerQuestListManager = GameObject.FindWithTag("Player").GetComponent<PlayerQuestListManager>();


        }


    }
    public void OnTriggerEnter(Collider other)
    {
        if (playerQuestHandler.IsCurrentQuest("Mini-Game: Lutasin ang Parirala"))
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Mini-Game: Lutasin ang Parirala"));

                PlayerQuestHandler.CompleteQuest("Mini-Game: Lutasin ang Parirala");

                semiwall.SetActive(true);

            }
        }
    }
}
