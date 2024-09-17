using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogMessagePrompt : MonoBehaviour
{
    [Header("Main")]
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

    [Header("Picture")]
    [SerializeField] private Image imageHolder = null; // Image holder in the UI

    void Awake()
    {
        Instance = this;

        canvasGroup = canvas.GetComponent<CanvasGroup>();

        closeUIButton.onClick.RemoveAllListeners();
        closeUIButton.onClick.AddListener(() => {
            Hide();
            SpecificMethod();
        });

        // Hide image by default
        if(imageHolder != null)
        {
            imageHolder.gameObject.SetActive(false);

        }
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

    public DialogMessagePrompt SetImage(Sprite image)
    {
        if (image != null)
        {
            dialog.HasImage = true;
            dialog.Image = image;
        }
        else
        {
            dialog.HasImage = false; // No image by default
        }
        return Instance;
    }

    public DialogMessagePrompt OnClose(UnityAction action)
    {
        dialog.OnClose = action;
        return Instance;
    }

    public void Show()
    {
        SoundEffectManager.PlayButtonCardPopup();

        dialogsQueue.Enqueue(dialog);
        dialog = new DialogPrompt();

        if (!IsActive)
            ShowNextDialog();
    }

    void ShowNextDialog()
    {
        SoundEffectManager.PlayButtonCardPopup();

        tempDialog = dialogsQueue.Dequeue();

        titleUIText.text = tempDialog.Title;
        messageUIText.text = tempDialog.Message;

        if (imageHolder != null)
        {
            if (tempDialog.HasImage && tempDialog.Image != null)
            {

                imageHolder.sprite = tempDialog.Image;
                imageHolder.gameObject.SetActive(true); // Show the image holder
            }
            else
            {
                imageHolder.gameObject.SetActive(false); // Hide the image holder if no image
            }
        }
        

        canvas.SetActive(true);
        IsActive = true;
        StartCoroutine(FadeIn(tempDialog.FadeInDuration));
    }

    public void Hide()
    {
        SoundEffectManager.PlayButtonClick2();

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

