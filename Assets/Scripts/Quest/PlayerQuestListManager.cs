using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerQuestListManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject questListContainer;
    [SerializeField] private GameObject questListItemPrefab;
    [SerializeField] private TextMeshProUGUI questCounterText;

    [Header("Quest Manager")]
    [SerializeField] private PlayerQuestHandler playerQuestHandler;

    [Header("Quest Data")]
    public List<Quest> questList = new List<Quest>();


    [Header("Quest List Interface")]
    [SerializeField] private GameObject questListWindowPanel;
    private bool isToggle = false;


    public void Toggle()
    {
        isToggle = !isToggle;
        if (isToggle)
        {
            PopulateQuestList();
            questListWindowPanel.SetActive(true);
        }
        else
        {
            questListWindowPanel.SetActive(false);
        }
    }
 
    public void PopulateQuestList()
    {
        questList = playerQuestHandler.Level1Quests;

        Debug.Log("Populating quest list with " + questList.Count + " quests.");

        foreach (Transform child in questListContainer.transform)
        {
            Destroy(child.gameObject);
        }

        int incompleteQuestsCount = 0;

        foreach (Quest quest in questList)
        {
            GameObject questItem = Instantiate(questListItemPrefab, questListContainer.transform);

            TextMeshProUGUI titleText = questItem.transform.Find("TitleText")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI statusText = questItem.transform.Find("StatusText")?.GetComponent<TextMeshProUGUI>();

            if (titleText == null || statusText == null)
            {
                Debug.LogError("TitleText or StatusText is missing from the prefab!");
                continue;
            }

            titleText.text = quest.QuestTitle;

            if (quest.IsCompleted)
            {
                statusText.text = "Completed";
                statusText.color = Color.green;
            }
            else
            {
                statusText.text = "In Progress";
                statusText.color = Color.red;
                incompleteQuestsCount++;
            }
        }

        questCounterText.text = $"{incompleteQuestsCount}/{questList.Count} Remaining incomplete Quest(s)";
    }
}
