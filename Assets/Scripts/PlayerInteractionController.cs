using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    [Header("References")]
    [SerializeField] private CanvasGroup playerMainCanvasControllerGroup;


   


    private void Awake()
    {
        ButtonSetState(ButtonInteract, false);
        ButtonSetState(ButtonPickUp, false);


        isDialogueStarted = false;

        playerMainCanvasControllerGroup.alpha = 1;
        playerMainCanvasControllerGroup.enabled = false;
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

                    if(_character.name == "Ferdinand Magellan")
                    {
                        // -------------------------- CHAPTER 1 LEVEL 1 ---------------------------------

                        if (SceneManager.GetActiveScene().name == "Chapter1Level1")
                        {
                            PlayerPrefs.SetString("Chapter1Level1", "COMPLETED");
                            PlayerPrefs.SetString("Chapter1Level2", "IN_PROGRESS");
                            PlayerPrefs.Save();

                            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Meet Ferdinand Magellan"));

                            PlayerQuestHandler.CompleteQuest("Meet Ferdinand Magellan");
                        }

                        // -------------------------- CHAPTER 1 LEVEL 2 ---------------------------------


                        if (SceneManager.GetActiveScene().name== "Chapter1Level2")
                        {
                            //Revised
                            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Go to the Center of the Training Field"));

                            PlayerQuestHandler.CompleteQuest("Go to the Center of the Training Field");
                        }

                        if (SceneManager.GetActiveScene().name == "Chapter1Level2")
                        {
                            //Revised
                            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("To Battlefield"));

                            PlayerQuestHandler.CompleteQuest("To Battlefield");


                        }

                        if (SceneManager.GetActiveScene().name == "Chapter1Level2")
                        {
                            //Revised
                            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Talk to Magellan About the Issues"));

                            PlayerQuestHandler.CompleteQuest("Talk to Magellan About the Issues");


                        }

                        // -------------------------- CHAPTER 1 LEVEL 5 ---------------------------------
                        if (SceneManager.GetActiveScene().name == "Chapter1Level5")
                        {  
                            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Go to Magellan"));

                            PlayerQuestHandler.CompleteQuest("Go to Magellan");
                        }

                        if (SceneManager.GetActiveScene().name == "Chapter1Level5")
                        {
                            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Go to Magellan at the fountain"));

                            PlayerQuestHandler.CompleteQuest("Go to Magellan at the fountain");
                        }
                        


                    }
                    if (_character.name == "Ruy")
                    {
                        // -------------------------- CHAPTER 1 LEVEL 5 ---------------------------------

                        if (SceneManager.GetActiveScene().name == "Chapter1Level5")
                        {
                            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Meet Ruy Falero"));

                            PlayerQuestHandler.CompleteQuest("Meet Ruy Falero");
                        }
                    }
                    if (other.gameObject.GetComponent<DialogueTrigger>())
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
        else
        {
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
