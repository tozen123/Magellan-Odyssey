using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]

public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
    public List<DialogueChoice> choices = new List<DialogueChoice>();
    public bool hasChoices = false;
    public bool isConverstationWithDefinedChoices = false;

    public string targetSceneName;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool isThisQuiz = false;
    public bool isTook = false;
    public bool chapterQuiz = false;

    public void TriggerDialogue()
    {

        DialogueManager.Instance.QuizMode(isTook, chapterQuiz);


        if (isThisQuiz)
        {
            DialogueManager.Instance.StartRandomizedDialogue(dialogue); 
        }
        else
        {
            DialogueManager.Instance.StartDialogue(dialogue); 
        }

        if (isThisQuiz)
        {
            
            isTook = true;
        }

        if (chapterQuiz)
        {
            chapterQuiz = true;
        }
    }

    public int GetRemainingLines()
    {
        return DialogueManager.Instance.GetRemainingLinesCount();
    }
}
