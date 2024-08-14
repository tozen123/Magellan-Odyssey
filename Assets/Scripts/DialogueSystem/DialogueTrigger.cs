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

    public void TriggerDialogue()
    {
        DialogueManager.Instance.QuizMode(isTook);

        if (isThisQuiz == true)
        {
            DialogueManager.Instance.StartDialogue(dialogue);
            isTook = true;
        }
        else
        {
            DialogueManager.Instance.StartDialogue(dialogue);
        }

       
            
       
    }

    public int GetRemainingLines()
    {
        return DialogueManager.Instance.GetRemainingLinesCount();
    }
}
