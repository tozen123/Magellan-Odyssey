using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogMessagePromptAction : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] TextMeshProUGUI titleUIText;
    [SerializeField] TextMeshProUGUI messageUIText;
    [SerializeField] Button positiveUIButton;
    [SerializeField] Button negativeUIButton;

    Queue<DialogPrompt> dialogsQueue = new Queue<DialogPrompt>();
    DialogPrompt dialog = new DialogPrompt();
    DialogPrompt tempDialog;

    public static DialogMessagePromptAction Instance;

    [HideInInspector] public bool IsActive = false;
    CanvasGroup canvasGroup;

    void Awake()
    {
        Instance = this;

        canvasGroup = canvas.GetComponent<CanvasGroup>();

     

        positiveUIButton.onClick.RemoveAllListeners();
        positiveUIButton.onClick.AddListener(() => {
            OnPositiveClick();
        });

        negativeUIButton.onClick.RemoveAllListeners();
        negativeUIButton.onClick.AddListener(() => {
            OnNegativeClick();
        });
    }

    void Start()
    {
        SpecificMethod();
    }

    public DialogMessagePromptAction SetTitle(string title)
    {
        dialog.Title = title;
        return Instance;
    }

    public DialogMessagePromptAction SetMessage(string message)
    {
        dialog.Message = message;
        return Instance;
    }

    public DialogMessagePromptAction SetFadeInDuration(float duration)
    {
        dialog.FadeInDuration = duration;
        return Instance;
    }

    public DialogMessagePromptAction OnPositive(UnityAction action)
    {
        dialog.OnPositive = action;
        return Instance;
    }

    public DialogMessagePromptAction OnNegative(UnityAction action)
    {
        dialog.OnNegative = action;
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

    void OnPositiveClick()
    {
        if (tempDialog.OnPositive != null)
            tempDialog.OnPositive.Invoke();
        Hide();
    }

    void OnNegativeClick()
    {
        if (tempDialog.OnNegative != null)
            tempDialog.OnNegative.Invoke();
        Hide();
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
