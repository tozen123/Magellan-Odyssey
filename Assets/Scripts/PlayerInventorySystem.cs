using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInventorySystem : MonoBehaviour
{
    public List<PickableObject> items = new List<PickableObject>();

    public PickableObject item;
    [Header("Action Button")]
    public Button ButtonPickUp;

    [Header("Animations")]
    public Animator animator;

    public Transform inventoryBag;

    [Header("ActionUpdater")]
    public TextActionUpdateSystem textActionUpdateSystem;

    [Header("UI Controller")]
    public PlayerInventoryUIController inventoryUIController;




    public ChapterOneLevelTwoHandler_RevisedVersion questHandler;
    public ChapterOneLevelSixHandler questHandlerL6;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PickableObject>())
        {
            item = other.gameObject.GetComponent<PickableObject>();

            if (inventoryUIController.CanAddItem())
            {
                ButtonPickUp.interactable = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ResetControllers();
    }

    public void PickUpItem()
    {
        if (item != null && inventoryUIController.CanAddItem())
        {
            PlayerSoundEffectManager.PlayPickUp();
            animator.SetTrigger("PickUp");

            items.Add(item);
            item.transform.parent = inventoryBag.transform;
            item.transform.position = inventoryBag.transform.position;
            item.gameObject.SetActive(false);

            if(SceneManager.GetActiveScene().name == "Chapter1Level2")
            {
                Debug.Log("yes");

                if (item.itemName == "Crate")
                {
                    questHandler.OnCrateCollected(item.gameObject); 
                }
            }
            if (SceneManager.GetActiveScene().name == "Chapter1Level6")
            {
                Debug.Log("yes");

                if (item.itemName == "Provision Crate")
                {
                    questHandlerL6.OnCrateCollected(item.gameObject);
                }
            }


            if (textActionUpdateSystem != null)
            {
                textActionUpdateSystem.CreateActionUpdateText("You picked up " + item.itemName);
            }

            inventoryUIController.UpdateInventoryUI(); // Update the inventory UI

            ResetControllers();
        }
        else
        {
            // Handle full inventory case if needed
            Debug.Log("Inventory is full. Cannot pick up " + item.itemName);
        }
    }

    private void ResetControllers()
    {
        item = null;
        ButtonPickUp.interactable = false;
    }
    public void RemoveItem(PickableObject itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
            itemToRemove.transform.parent = null; // Unparent the item from the inventory bag

            //// Optional: if you want to make the item visible again in the game world
            //itemToRemove.transform.position = transform.position + transform.forward; // Drop it in front of the player
            //itemToRemove.gameObject.SetActive(true);

            if (textActionUpdateSystem != null)
            {
                textActionUpdateSystem.CreateActionUpdateText("You removed " + itemToRemove.itemName);
            }

            inventoryUIController.UpdateInventoryUI(); // Update the inventory UI
            Debug.Log("Item removed: " + itemToRemove.itemName);
        }
        else
        {
            Debug.Log("Item not found in inventory: " + itemToRemove.itemName);
        }
    }
}
