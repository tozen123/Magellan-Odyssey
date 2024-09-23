using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            animator.SetTrigger("PickUp");

            items.Add(item);

            item.transform.parent = inventoryBag.transform;
            item.transform.position = inventoryBag.transform.position;
            item.gameObject.SetActive(false);

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
}
