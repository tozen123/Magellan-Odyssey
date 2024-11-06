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
    [SerializeField] public bool isDialogueStarted;


    public Animator playerAnimator;


    [Header("References")]
    [SerializeField] private CanvasGroup playerMainCanvasControllerGroup;

    PlayerQuestHandler playerQuestHandler;


    [SerializeField] public string interactingWith;


    private Vector3 interactOriginalScale;
    private Vector3 pickUpOriginalScale;
    private bool isPulsing = false;
    private float pulseSpeed = 1f;
    private float scaleAmount = 0.2f;

    private void Start()
    {
        playerQuestHandler = GetComponent<PlayerQuestHandler>();

        interactOriginalScale = ButtonInteract.transform.localScale;
        pickUpOriginalScale = ButtonPickUp.transform.localScale;
    }

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
                    interactingWith = _character.name;

                    if (_character.name == "Ferdinand Magellan")
                    {
                        // -------------------------- CHAPTER 1 LEVEL 1 ---------------------------------

                        //if (SceneManager.GetActiveScene().name == "Chapter1Level1")
                        //{
                        //    PlayerPrefs.SetString("Chapter1Level1", "COMPLETED");
                        //    PlayerPrefs.SetString("Chapter1Level2", "IN_PROGRESS");
                        //    PlayerPrefs.Save();

                        //    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Meet Ferdinand Magellan"));

                        //    PlayerQuestHandler.CompleteQuest("Meet Ferdinand Magellan");
                        //}

                        // -------------------------- CHAPTER 1 LEVEL 2 ---------------------------------


                        //if (SceneManager.GetActiveScene().name== "Chapter1Level2")
                        //{
                        //    //Revised
                        //    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Go to the Center of the Training Field"));

                        //    PlayerQuestHandler.CompleteQuest("Go to the Center of the Training Field");
                        //}

                        //if (SceneManager.GetActiveScene().name == "Chapter1Level2")
                        //{
                        //    //Revised
                        //    PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("To Battlefield"));

                        //    PlayerQuestHandler.CompleteQuest("To Battlefield");


                        //}



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

                        // -------------------------- CHAPTER 1 LEVEL 6 ---------------------------------
                        if (SceneManager.GetActiveScene().name == "Chapter1Level6")
                        {
                            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Go to Magellan"));

                            PlayerQuestHandler.CompleteQuest("Go to Magellan");
                        }

                        // -------------------------- CHAPTER 1 LEVEL 6 ---------------------------------
                        if (SceneManager.GetActiveScene().name == "Chapter1Level6")
                        {
                            PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Report to Magellan"));

                            PlayerQuestHandler.CompleteQuest("Report to Magellan");
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

                    if (_character.name == "Ship Master Martinez")
                    {
                        // -------------------------- CHAPTER 1 LEVEL 6 ---------------------------------
                        if (SceneManager.GetActiveScene().name == "Chapter1Level6")
                        {
                            if (!playerQuestHandler.IsCurrentQuest("Special Quiz: Talk to the Ship Master"))
                            {
                                DialogMessagePrompt.Instance
                                    .SetTitle("System Message")
                                    .SetMessage("You cannot interact with the ship master at the moment. you first need to complete other quest first.")
                                    .Show();
                                return;
                            }
                            
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (other.gameObject.GetComponent<Character>())
            { 
                PulsateButton(ButtonInteract, interactOriginalScale);

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
        interactingWith = "";

        if (other.gameObject.tag == "NPC")
        {
            interactingWith = "";
            GameObject floaterUI = other.transform.GetChild(0).gameObject;
            if (floaterUI != null)
            {
                floaterUI.SetActive(false);
            }


            ButtonInteract.transform.localScale = interactOriginalScale;

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
            if(interactingWith == "Ferdinand Magellan")
            {
                if (SceneManager.GetActiveScene().name == "Chapter1Level1")
                {
                    if (playerQuestHandler.IsCurrentQuest("Meet Ferdinand Magellan"))
                    {
                 

                        PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Meet Ferdinand Magellan"));

                        PlayerQuestHandler.CompleteQuest("Meet Ferdinand Magellan");

                        PlayerPrefs.SetInt("Kabanata1BookOfTrivia_IsLock", 0); 
                    }
                    else
                    {
                        DialogMessagePrompt.Instance
                               .SetTitle("System Message")
                               .SetMessage("You must complete the other quest before interacting with this character.")
                               .Show();
                        return;
                    }

                    

                }



                if (SceneManager.GetActiveScene().name == "Chapter1Level2")
                {
                    if (playerQuestHandler.IsCurrentQuest("Go to the Center of the Training Field"))
                    {
                        PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Go to the Center of the Training Field"));

                        PlayerQuestHandler.CompleteQuest("Go to the Center of the Training Field");
                    }

                    else if (playerQuestHandler.IsCurrentQuest("Talk to Magellan About the Issues"))
                    {
                        PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Talk to Magellan About the Issues"));

                        PlayerQuestHandler.CompleteQuest("Talk to Magellan About the Issues");
                    }

                    else
                    {
                        DialogMessagePrompt.Instance
                               .SetTitle("System Message")
                               .SetMessage("You must complete the other quest before interacting with this character.")
                               .Show();
                        return;
                    }


                  

                }
                

            }


            if(interactingWith == "Henry (Old Man)")
            {
                if (SceneManager.GetActiveScene().name == "Chapter1Level1")
                {
                    if (playerQuestHandler.IsCurrentQuest("Kilalanin si Ferdinand Magellan"))
                    {


                        PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Kilalanin si Ferdinand Magellan"));

                        PlayerQuestHandler.CompleteQuest("Kilalanin si Ferdinand Magellan");
                    }
                    else
                    {
                        DialogMessagePrompt.Instance
                               .SetTitle("System Message")
                               .SetMessage("You must complete the other quest before interacting with this character.")
                               .Show();
                        return;
                    }
                }
                    
            }
            if (interactingWith == "Antonio Pigafetta")
            {
                if (SceneManager.GetActiveScene().name == "Chapter1Level1")
                {
                    if (playerQuestHandler.IsCurrentQuest("Quiz Master Chapter 1"))
                    {
                        PlayerPrefs.SetString("Chapter1Level1", "COMPLETED");
                        PlayerPrefs.SetString("Chapter1Level2", "IN_PROGRESS");
                        PlayerPrefs.Save();

                        PlayerPointingSystem.Instance.AddPoints(PlayerQuestHandler.GetQuestADPPoints("Quiz Master Chapter 1"));

                        PlayerQuestHandler.CompleteQuest("Quiz Master Chapter 1");
                    }
                    else
                    {
                        DialogMessagePrompt.Instance
                               .SetTitle("System Message")
                               .SetMessage("You must complete the other quest before interacting with this character.")
                               .Show();
                        return;
                    }
                }
            }





                dialogueTrigger.TriggerDialogue();

            isDialogueStarted = true;

            ButtonSetState(ButtonInteract, false);
            movementJoystick.enabled = false;
        }
    }

  
    public void PulsateButton(Button button, Vector3 originalScale)
    {

        Transform targetTransform = button.transform;

        float scale = 1 + Mathf.PingPong(Time.time * pulseSpeed, scaleAmount);
        targetTransform.localScale = originalScale * scale;
    }

   
}
