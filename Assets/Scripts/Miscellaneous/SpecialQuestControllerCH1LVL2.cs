using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialQuestControllerCH1LVL2 : MonoBehaviour
{
    public PlayerInventorySystem playerInventorySystem;
    public PlayerInventoryUIController playerInventoryUIController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Ipamahagi ang mga Pabuya sa mga Sundalo"));
            PlayerQuestHandler.CompleteQuest("Ipamahagi ang mga Pabuya sa mga Sundalo");

            // Remove all items that have the name "Crate"
            RemoveAllCratesFromInventory();
        }
    }

    // Method to remove all items named "Crate" from the player's inventory
    private void RemoveAllCratesFromInventory()
    {
        List<PickableObject> itemsToRemove = new List<PickableObject>();

        // Find all items named "Crate" in the inventory
        foreach (PickableObject item in playerInventorySystem.items)
        {
            if (item.itemName == "Crate")
            {
                itemsToRemove.Add(item);
            }
        }

        // Remove each "Crate" item from the inventory
        foreach (PickableObject crate in itemsToRemove)
        {
            playerInventorySystem.RemoveItem(crate);
        }

        // Update the inventory UI
        playerInventoryUIController.UpdateInventoryUI();
    }
}
