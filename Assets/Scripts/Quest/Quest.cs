using UnityEngine;

[System.Serializable]
public class Quest
{
    public string QuestTitle;

    [TextArea(3, 10)]
    public string QuestDescription;    
    public string QuestWhatToDo;        
    public int QuestADPPoints;          
    public bool IsCompleted { get; private set; }  


    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}
