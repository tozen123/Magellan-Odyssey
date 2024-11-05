using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class TutorialDialogPrompt : MonoBehaviour
{
    public static TutorialDialogPrompt Instance;

    public TextMeshProUGUI promptMessage;
    public Button tapToAnywhereToContinue;
    public Image optionalImage;
    public GameObject child;

    private Queue<DialogData> dialogQueue = new Queue<DialogData>();
    private bool isShowingDialog = false;
    private UnityAction onCloseAction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        tapToAnywhereToContinue.onClick.AddListener(() =>
        {
            Hide();
        });

        child.SetActive(false);
        if (optionalImage != null)
        {
            optionalImage.gameObject.SetActive(false);
        }
    }

    public TutorialDialogPrompt SetMessage(string message)
    {
        if (dialogQueue.Count > 0)
        {
            dialogQueue.Peek().Message = message;
        }
        else
        {
            dialogQueue.Enqueue(new DialogData { Message = message });
        }
        return this;
    }

    public TutorialDialogPrompt SetImage(Sprite image)
    {
        if (dialogQueue.Count > 0)
        {
            dialogQueue.Peek().Image = image;
        }
        else
        {
            dialogQueue.Enqueue(new DialogData { Image = image });
        }
        return this;
    }

    public TutorialDialogPrompt OnClose(UnityAction action)
    {
        if (dialogQueue.Count > 0)
        {
            dialogQueue.Peek().OnClose = action;
        }
        else
        {
            dialogQueue.Enqueue(new DialogData { OnClose = action });
        }
        return this;
    }

    public void Show()
    {
        if (dialogQueue.Count == 0)
        {
            dialogQueue.Enqueue(new DialogData
            {
                Message = promptMessage.text,
                Image = optionalImage.sprite,
                OnClose = onCloseAction
            });
        }

        if (!isShowingDialog)
        {
            ShowNextDialog();
        }
    }

    private void ShowNextDialog()
    {
        if (dialogQueue.Count > 0)
        {
            isShowingDialog = true;
            DialogData currentDialog = dialogQueue.Dequeue();

            promptMessage.text = currentDialog.Message;

            if (currentDialog.Image != null)
            {
                optionalImage.sprite = currentDialog.Image;
                optionalImage.preserveAspect = true;
                optionalImage.gameObject.SetActive(true);
            }
            else
            {
                optionalImage.gameObject.SetActive(false);
            }

            child.SetActive(true);
            onCloseAction = currentDialog.OnClose;
        }
        else
        {
            isShowingDialog = false;
        }
    }

    public void Hide()
    {
        child.SetActive(false);
        onCloseAction?.Invoke();
        onCloseAction = null;
        if (optionalImage != null)
        {
            optionalImage.gameObject.SetActive(false);
        }

        isShowingDialog = false;
        ShowNextDialog();
    }

    private class DialogData
    {
        public string Message;
        public Sprite Image;
        public UnityAction OnClose;
    }
}
