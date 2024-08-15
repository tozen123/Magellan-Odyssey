[System.Serializable]
public class Quest
{
    public string QuestTitle { get; private set; }
    public string QuestDescription { get; private set; }
    public string QuestWhatToDo { get; private set; }
    public int QuestADPPoints { get; private set; }
    public bool IsCompleted { get; private set; }

    public Quest(string title, string description, string whatToDo, int adpPoints)
    {
        QuestTitle = title;
        QuestDescription = description;
        QuestWhatToDo = whatToDo;
        QuestADPPoints = adpPoints;
        IsCompleted = false;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}
