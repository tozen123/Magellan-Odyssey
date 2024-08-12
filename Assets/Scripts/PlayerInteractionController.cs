using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("Action Button")]
    public Button ButtonInteract;
    public Button ButtonInventory;
    public Button ButtonPickUp;

    [Header("Variables DialogueSystem")]
    [SerializeField] private FixedJoystick movementJoystick;
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private bool isDialogueStarted;


    public Animator playerAnimator;

    private void Awake()
    {
        ButtonSetState(ButtonInteract, false);
        ButtonSetState(ButtonPickUp, false);


        isDialogueStarted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NPC")
        {
            if (other.gameObject.GetComponent<Character>())
            {
                Character _character = other.gameObject.GetComponent<Character>();

                GameObject floaterUI = other.transform.GetChild(0).gameObject;

                if (floaterUI != null)
                {
                    floaterUI.SetActive(true);
                }


                if (_character != null)
                {
                    ButtonSetState(ButtonInteract, true);

                    Debug.Log(_character.name);

                    if (_character.name == "Jan")
                    {
                        dialogueTrigger = other.gameObject.GetComponent<DialogueTrigger>();
                    }

                    if (_character.name == "Hariette")
                    {
                        dialogueTrigger = other.gameObject.GetComponent<DialogueTrigger>();
                    }

                    if (_character.name == "Ttan")
                    {
                        dialogueTrigger = other.gameObject.GetComponent<DialogueTrigger>();
                    }
                }

            }
        }
    }

    private void Update()
    {
        if (dialogueTrigger != null)
        {
            if (isDialogueStarted)
            {
                Debug.Log("Dialogue Lines: " + dialogueTrigger.GetRemainingLines());
                if(dialogueTrigger.GetRemainingLines() == 0)
                {
                    isDialogueStarted = false;
                    ResetControllers();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {

            GameObject floaterUI = other.transform.GetChild(0).gameObject;
            if (floaterUI != null)
            {
                floaterUI.SetActive(false);
            }


            ButtonSetState(ButtonInteract, false);
            ResetControllers();
        }
       

    }

    public void ResetControllers()
    {
        ButtonSetState(ButtonInteract, false);
        dialogueTrigger = null;
        movementJoystick.enabled = true;
    }

    public void ButtonSetState(Button button, bool state)
    {
        button.interactable = state;
    }

    public void Interact()
    {
        if (dialogueTrigger != null)
        {
            dialogueTrigger.TriggerDialogue();

            isDialogueStarted = true;

            ButtonSetState(ButtonInteract, false);
            movementJoystick.enabled = false;
        }
    }

  


   
}
