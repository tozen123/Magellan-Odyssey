using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;



public class DialogMessagePrompt : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] TextMeshProUGUI titleUIText;
    [SerializeField] TextMeshProUGUI messageUIText;
    [SerializeField] Button closeUIButton;

    Queue<DialogPrompt> dialogsQueue = new Queue<DialogPrompt>();
    DialogPrompt dialog = new DialogPrompt();
    DialogPrompt tempDialog;

    public static DialogMessagePrompt Instance;

    [HideInInspector] public bool IsActive = false;
    CanvasGroup canvasGroup;

    void Awake()
    {
        Instance = this;

        canvasGroup = canvas.GetComponent<CanvasGroup>();

        closeUIButton.onClick.RemoveAllListeners();
        closeUIButton.onClick.AddListener(() => {
            Hide();
            SpecificMethod();
        });
    }

    void Start()
    {
        SpecificMethod();
    }

    public DialogMessagePrompt SetTitle(string title)
    {
        dialog.Title = title;
        return Instance;
    }

    public DialogMessagePrompt SetMessage(string message)
    {
        dialog.Message = message;
        return Instance;
    }

    public DialogMessagePrompt SetFadeInDuration(float duration)
    {
        dialog.FadeInDuration = duration;
        return Instance;
    }

    public DialogMessagePrompt OnClose(UnityAction action)
    {
        dialog.OnClose = action;
        return Instance;
    }

    public void Show()
    {
        dialogsQueue.Enqueue(dialog);
        dialog = new DialogPrompt();

        if (!IsActive)
            ShowNextDialog();
    }

    void ShowNextDialog()
    {
        tempDialog = dialogsQueue.Dequeue();

        titleUIText.text = tempDialog.Title;
        messageUIText.text = tempDialog.Message;

        canvas.SetActive(true);
        IsActive = true;
        StartCoroutine(FadeIn(tempDialog.FadeInDuration));
    }

    public void Hide()
    {
        canvas.SetActive(false);
        IsActive = false;

        if (tempDialog.OnClose != null)
            tempDialog.OnClose.Invoke();

        StopAllCoroutines();

        if (dialogsQueue.Count != 0)
            ShowNextDialog();
    }

    IEnumerator FadeIn(float duration)
    {
        float startTime = Time.time;
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / duration);
            canvasGroup.alpha = alpha;

            yield return null;
        }
    }

    void SpecificMethod()
    {
    }
}
