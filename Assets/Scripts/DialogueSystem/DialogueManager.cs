using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Interface Reference")]
    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    public List<Button> choiceButtons;
    public GameObject choiceButtonPanelParent;
    public GameObject panelFG;


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
    public float typingSpeed = 0.025f;


    private bool isAwaitingInput = false;


    [SerializeField] private Animator animator;

    [Header("Controller Reference")]
    [SerializeField] private CanvasGroup canvasGroup;

    private int correctAnswersCount = 0;
    private int dialogueWithChoicesCount = 0;

    private bool chapQuiz;


    public TextMeshProUGUI acpCount;

    public AudioSource audioSource;


    public void StartRandomizedDialogue(Dialogue dialogue)
    {
        PlayerSoundEffectManager.PlayConvoPopUp();
        isDialogueActive = true;
        animator.Play("UIPop");
        panelFG.SetActive(true);

        if (isCompleteQuiz)
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

        List<DialogueLine> dialoguesWithChoices = dialogue.dialogueLines
            .Where(line => line.hasChoices)
            .OrderBy(_ => Random.value)
            .ToList();

        List<DialogueLine> dialoguesWithoutChoices = dialogue.dialogueLines
            .Where(line => !line.hasChoices && !line.line.Contains("Napakagaling Mo"))
            .ToList();

        DialogueLine endLine = dialogue.dialogueLines
            .FirstOrDefault(line => line.line.Contains("Napakagaling Mo"));

        foreach (DialogueLine line in dialoguesWithChoices)
        {
            line.choices = line.choices.OrderBy(_ => Random.value).ToList();
        }

        lines.Clear();
        foreach (DialogueLine line in dialoguesWithoutChoices)
        {
            lines.Enqueue(line);
        }
        foreach (DialogueLine shuffledLine in dialoguesWithChoices)
        {
            lines.Enqueue(shuffledLine);
        }

        if (endLine != null)
        {
            lines.Enqueue(endLine);
        }

        feedbackBodyParent.SetActive(false);
        dialogueBodyParent.SetActive(true);

        DisplayNextDialogueLine();
    }





    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();

        animator.Play("UIFullHide");
        panelFG.SetActive(false);

    }
    public void QuizMode(bool isTook, bool _chapQuiz)
    {

        isCompleteQuiz = isTook;
        chapQuiz = _chapQuiz;
    }
    public void StartDialogue(Dialogue dialogue)
    {

        PlayerSoundEffectManager.PlayConvoPopUp();
        isDialogueActive = true;
        animator.Play("UIPop");
        panelFG.SetActive(true);

        lines.Clear();


        if (isCompleteQuiz)
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

            if (dialogueLine.hasChoices || dialogueLine.isConverstationWithDefinedChoices)
            {
                dialogueWithChoicesCount++;
            }
        }

        feedbackBodyParent.SetActive(false);
        dialogueBodyParent.SetActive(true);

        DisplayNextDialogueLine();
    }


    public void DisplayNextDialogueLine()
    {
        PlayerSoundEffectManager.PlayNextDialogue();

        if (currentLine != null && (currentLine.hasChoices || currentLine.isConverstationWithDefinedChoices))
        {
            PlayerSoundEffectManager.StopQuizTheme();
        }

        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterIcon.preserveAspect = true;


        characterName.text = currentLine.character.name;
        if (characterName.text.Equals("Avatar"))
        {

            characterName.text = PlayerPrefs.GetString("userName").ToString();
        }


        StopAllCoroutines();


        if (currentLine.audioClip != null)
        {
            audioSource.Stop(); 
            audioSource.clip = currentLine.audioClip;
            audioSource.Play();
        }


        if (currentLine.hasChoices)
        {
            PlayerSoundEffectManager.PlayQuizTheme(); 
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
            PlayerSoundEffectManager.StopQuizTheme(); 

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
        isAwaitingInput = true; 
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

            correctAnswersCount++;
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
        panelFG.SetActive(false);

        PlayerSoundEffectManager.StopQuizTheme();

        int acp;
        acp = correctAnswersCount * 10;

        int adp = 0;

        int quizlength = 0;
        if (chapQuiz)
        {
            if (SceneManager.GetActiveScene().name == "Chapter1Level1")
            {

                PlayerPrefs.SetInt("Kabanata1BookOfTrivia_IsLock", 1);

                int oldcount = PlayerPrefs.GetInt("Chapter1TotalQuizScore", 0);
                PlayerPrefs.SetInt("Chapter1TotalQuizScore", oldcount + acp);

                PlayerPrefs.SetInt("Chapter1QuizScore", correctAnswersCount);
                PlayerPrefs.Save();

                if (correctAnswersCount == 5)
                {
                    adp = 50;
                }
                quizlength = 5;


            }
            if (SceneManager.GetActiveScene().name == "Chapter1Level2")
            {
                PlayerPrefs.SetInt("Kabanata2BookOfTrivia_IsLock", 1);


                int oldcount = PlayerPrefs.GetInt("Chapter1TotalQuizScore", 0);
                PlayerPrefs.SetInt("Chapter1TotalQuizScore", oldcount + acp);


                PlayerPrefs.SetInt("Chapter2QuizScore", correctAnswersCount);
                PlayerPrefs.Save();

                if (correctAnswersCount == 7)
                {
                    adp = 70;
                }
                quizlength = 7;
            }
            if (SceneManager.GetActiveScene().name == "Chapter1Level3")
            {
                PlayerPrefs.SetInt("Kabanata3BookOfTrivia_IsLock", 1);

                int oldcount = PlayerPrefs.GetInt("Chapter1TotalQuizScore", 0);
                PlayerPrefs.SetInt("Chapter1TotalQuizScore", oldcount + acp);

                PlayerPrefs.SetInt("Chapter3QuizScore", correctAnswersCount);
                PlayerPrefs.Save();

                if (correctAnswersCount == 3)
                {
                    adp = 30;
                }
                quizlength = 3;
            }
            if (SceneManager.GetActiveScene().name == "Chapter1Level4")
            {
                PlayerPrefs.SetInt("Kabanata4BookOfTrivia_IsLock", 1);

                int oldcount = PlayerPrefs.GetInt("Chapter1TotalQuizScore", 0);
                PlayerPrefs.SetInt("Chapter1TotalQuizScore", oldcount + correctAnswersCount);

                PlayerPrefs.SetInt("Chapter4QuizScore", correctAnswersCount);
                PlayerPrefs.Save();

                if (correctAnswersCount == 7)
                {
                    adp = 70;
                }
                quizlength = 7;

            }
            if (SceneManager.GetActiveScene().name == "Chapter1Level5")
            {
                PlayerPrefs.SetInt("Kabanata5BookOfTrivia_IsLock", 1);

                int oldcount = PlayerPrefs.GetInt("Chapter1TotalQuizScore", 0);
                PlayerPrefs.SetInt("Chapter1TotalQuizScore", oldcount + correctAnswersCount);

                PlayerPrefs.SetInt("Chapter5QuizScore", correctAnswersCount);
                PlayerPrefs.Save();

                if (correctAnswersCount == 20)
                {
                    adp = 200;
                }
                quizlength = 20;
            }
            if (SceneManager.GetActiveScene().name == "Chapter1Level6")
            {
                PlayerPrefs.SetInt("Kabanata6BookOfTrivia_IsLock", 1);

                int oldcount = PlayerPrefs.GetInt("Chapter1TotalQuizScore", 0);
                PlayerPrefs.SetInt("Chapter1TotalQuizScore", oldcount + correctAnswersCount);

                PlayerPrefs.SetInt("Chapter6QuizScore", correctAnswersCount);
                PlayerPrefs.Save();

                if (correctAnswersCount == 15)
                {
                    adp = 150;
                }
                quizlength = 15;
            }
            AddACP();

            if (adp > 0)
            {

                DialogMessagePrompt.Instance
                    .SetTitle("System Message")
                    .SetMessage(correctAnswersCount + " / " + quizlength + " Ikaw ay nakakuha ka ng Adventure Points na " + adp + " at Academic Points na " + acp)
                    .OnClose(Close)
                    .Show();
            }
            else
            {
                DialogMessagePrompt.Instance
                    .SetTitle("System Message")
                    .SetMessage(correctAnswersCount + " / " + quizlength + " Ikaw ay nakakuha ka ng Academic Points na " + acp)
                    .OnClose(Close)
                    .Show();
            }

            return;
        }

        correctAnswersCount = 0;

        isDialogueActive = false;
        isCompleteQuiz = false;

        if (!string.IsNullOrEmpty(currentLine.targetSceneName))
        {
            LoadingScreenManager.Instance.LoadScene(currentLine.targetSceneName);
        }

        Debug.Log("EndDialogue");

        animator.Play("UIHide");
    }

    public void Close()
    {
        correctAnswersCount = 0;

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

    public void AddACP()
    {
        int count = PlayerPrefs.GetInt("Chapter1TotalQuizScore", 0);
        acpCount.text = count.ToString();

    }

}
