using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Interface Reference")]
    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    public List<Button> choiceButtons;
    public GameObject choiceButtonPanelParent;

    [Header("Parents")]
    [SerializeField] private GameObject dialogoueParent;

    [SerializeField] private GameObject dialogueBodyParent;

    [Header("Feedback References")]
    public GameObject feedbackBodyParent;
    public TextMeshProUGUI feedbackText;

    private Queue<DialogueLine> lines;
    private DialogueLine currentLine;

    [Header("Configurations")]

    public bool isDialogueActive = false;
    public bool isCompleteQuiz = false;
    public float typingSpeed = 0.03f;


    private bool isAwaitingInput = false;


    [SerializeField] private Animator animator;

    [Header("Controller Reference")]
    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();

        animator.Play("UIFullHide");


    }
    public void QuizMode(bool isTook)
    {

        isCompleteQuiz = isTook;
    }
    public void StartDialogue(Dialogue dialogue)
    {
       
        PlayerSoundEffectManager.PlayConvoPopUp();
        isDialogueActive = true;
        animator.Play("UIPop");

        lines.Clear();


        if ( isCompleteQuiz)
        {
            feedbackBodyParent.SetActive(true);
            dialogueBodyParent.SetActive(false);
            feedbackText.text = "You already took the Quiz!";

            foreach (Button button in choiceButtons)
            {
                button.gameObject.SetActive(false);
            }

            isAwaitingInput = true;
            return;
        }

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        feedbackBodyParent.SetActive(false);
        dialogueBodyParent.SetActive(true);

        DisplayNextDialogueLine();
    }


    public void DisplayNextDialogueLine()
    {
        PlayerSoundEffectManager.PlayNextDialogue();


        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterIcon.preserveAspect = true;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        if (currentLine.hasChoices)
        {
            choiceButtonPanelParent.SetActive(true);
            DisplayChoices(currentLine);

            for (int i = 0; i < currentLine.choices.Count; i++)
            {
                choiceButtons[i].interactable = true;
                choiceButtons[i].image.color = Color.white;

                Debug.Log("reset");
            }
            return;
        }
        else if (currentLine.isConverstationWithDefinedChoices)
        {
            PlayerSoundEffectManager.PlayQuizTheme();

            choiceButtonPanelParent.SetActive(true);
            DisplaySimpleChoices(currentLine);

            for (int i = 0; i < currentLine.choices.Count; i++)
            {
                choiceButtons[i].interactable = true;
                choiceButtons[i].image.color = Color.white;

                Debug.Log("reset");
            }
            return;
        }
        else
        {
            choiceButtonPanelParent.SetActive(false);
            foreach (Button button in choiceButtons)
            {
                button.gameObject.SetActive(false);
            }

            StartCoroutine(DialogueTypeSentence(currentLine.line));
            return;
        }
       

    }


    void DisplaySimpleChoices(DialogueLine dialogueLine)
    {

        //readyForInput = false;

        ////dialogueArea.text = dialogueLine.line;
        //StartCoroutine(TypeSentence(dialogueLine.line));

        //while(readyForInput)
        //{
        //    for (int i = 0; i < choiceButtons.Count; i++)
        //    {
        //        int correctIndex = i;

        //        if (i < dialogueLine.choices.Count)
        //        {
        //            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = dialogueLine.choices[i].choiceText;
        //            choiceButtons[i].gameObject.SetActive(true);

        //            choiceButtons[i].onClick.AddListener(() => OnSimpleChoiceSelected());
        //        }
        //        else
        //        {
        //            choiceButtons[i].gameObject.SetActive(false);
        //        }
        //    }
        //    readyForInput = false;
        //}
      
        choiceButtonPanelParent.SetActive(false);
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }

        StartCoroutine(ShowChoicesAfterTyping(dialogueLine));

    }

    void OnSimpleChoiceSelected()
    {
        feedbackBodyParent.SetActive(false);
        choiceButtonPanelParent.SetActive(false);
        isAwaitingInput = true; // Now it's safe to await input
        DisplayNextDialogueLine();
    }

    

    IEnumerator DialogueTypeSentence(string sentence)
    {
        dialogueArea.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isAwaitingInput = true;
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueArea.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
     

    }
    void DisplayChoices(DialogueLine dialogueLine)
    {
        //dialogueArea.text = dialogueLine.line;
        //StartCoroutine(TypeSentence(dialogueLine.line));

        //for (int i = 0; i < choiceButtons.Count; i++)
        //{
        //    choiceButtons[i].onClick.RemoveAllListeners(); // Clear previous listeners
        //    choiceButtons[i].gameObject.SetActive(false);  // Hide all buttons initially

        //    if (i < dialogueLine.choices.Count)
        //    {
        //        choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = dialogueLine.choices[i].choiceText;
        //        choiceButtons[i].gameObject.SetActive(true);

        //        // Adding listener for quiz choices
        //        int correctIndex = i;
        //        choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(dialogueLine.choices[correctIndex]));
        //    }
        //}

       
        choiceButtonPanelParent.SetActive(false);
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }

        StartCoroutine(ShowChoicesAfterTypingForQuiz(dialogueLine));

        
    }

    void OnChoiceSelected(DialogueChoice choice)
    {
        isCompleteQuiz = true;

        Debug.Log("Displaying feedback...");

        feedbackBodyParent.SetActive(true);
        dialogueBodyParent.SetActive(true);

        if (choice.isCorrect)
        {
            PlayerSoundEffectManager.PlayCorrectQuiz();

            feedbackText.text = "Correct";
            Debug.Log("Correct answer selected");
        }
        else
        {
            PlayerSoundEffectManager.PlayWrongQuiz();

            feedbackText.text = "Wrong";
            Debug.Log("Wrong answer selected");
        }

        for (int i = 0; i < currentLine.choices.Count; i++)
        {
            choiceButtons[i].interactable = false;

            if (currentLine.choices[i].isCorrect)
            {
                choiceButtons[i].image.color = Color.green;
            }
            else
            {
                choiceButtons[i].image.color = Color.red;
            }
        }

        Debug.Log("Feedback displayed");

        isAwaitingInput = true;
    }

    void EndDialogue()
    {
        PlayerSoundEffectManager.StopQuizTheme();


        isDialogueActive = false;
        isCompleteQuiz = false;

        if (!string.IsNullOrEmpty(currentLine.targetSceneName))
        {
            LoadingScreenManager.Instance.LoadScene(currentLine.targetSceneName);
        }

        Debug.Log("EndDialogue");

        animator.Play("UIHide");
    }

    void Update()
    {
        if (isAwaitingInput && Input.GetMouseButtonDown(0))
        {
            isAwaitingInput = false;
            feedbackBodyParent.SetActive(false);
            DisplayNextDialogueLine();
        }

        if (!isDialogueActive)
        {
            dialogoueParent.gameObject.GetComponent<Image>().enabled = false;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            dialogoueParent.gameObject.GetComponent<Image>().enabled = true;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public int GetRemainingLinesCount()
    {
        return lines.Count;
    }

    IEnumerator ShowChoicesAfterTyping(DialogueLine dialogueLine)
    {
        yield return StartCoroutine(TypeSentence(dialogueLine.line));

        
        choiceButtonPanelParent.SetActive(true);

        for (int i = 0; i < choiceButtons.Count; i++)
        {
            if (i < dialogueLine.choices.Count)
            {
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = dialogueLine.choices[i].choiceText;
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].onClick.RemoveAllListeners(); 
                choiceButtons[i].onClick.AddListener(() => OnSimpleChoiceSelected());
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }
    IEnumerator ShowChoicesAfterTypingForQuiz(DialogueLine dialogueLine)
    {
        yield return StartCoroutine(TypeSentence(dialogueLine.line));


        choiceButtonPanelParent.SetActive(true);

        for (int i = 0; i < choiceButtons.Count; i++)
        {
            if (i < dialogueLine.choices.Count)
            {
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = dialogueLine.choices[i].choiceText;
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].onClick.RemoveAllListeners();
                int correctIndex = i;
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(dialogueLine.choices[correctIndex]));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }

    }

}
