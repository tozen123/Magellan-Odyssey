
using UnityEngine.Events;

public class DialogPrompt
{
    public string Title = "Title";
    public string Message = "Message goes here.";
    public string ButtonText = "Close";
    public float FadeInDuration = .3f;
    public UnityAction OnClose = null;
}