using System.Collections;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PickableObject>())
        {
            item = other.gameObject.GetComponent<PickableObject>();

            ButtonPickUp.interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ResetControllers();


    }

    public void PickUpItem()
    {
        if(item != null)
        {
            animator.SetTrigger("PickUp");

            item.Pick();
            items.Add(item);


            item.transform.parent = inventoryBag.transform;
            item.transform.position = inventoryBag.transform.position;
            item.gameObject.SetActive(false);

            if(textActionUpdateSystem != null)
            {
                textActionUpdateSystem.CreateActionUpdateText("You picked up " + item.itemName);

            }

            ResetControllers();
        }
    }


    private void ResetControllers()
    {
        item = null;
        ButtonPickUp.interactable = false;

    }
}
