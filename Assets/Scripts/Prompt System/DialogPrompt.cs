
using UnityEngine;
using UnityEngine.Events;

public class DialogPrompt
{
    public string Title = "Title";
    public string Message = "Message goes here.";
    public string ButtonText = "Close";
    public float FadeInDuration = .3f;
    public UnityAction OnClose = null;
    public UnityAction OnPositive = null;
    public UnityAction OnNegative = null;

    public bool HasImage = false; 
    public Sprite Image = null;   
}