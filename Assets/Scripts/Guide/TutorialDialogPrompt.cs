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
    public Button nextButton;
    public Button backButton;
    public Image optionalImage;
    public GameObject child;

    private DialogData currentDialog; // Current dialog
    private UnityAction onCloseAction;
    private UnityAction onPreviousAction; // OnPrevious UnityAction

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
        nextButton.onClick.AddListener(() => Hide());
        backButton.onClick.AddListener(() => GoBack());

        child.SetActive(false);
        if (optionalImage != null)
        {
            optionalImage.gameObject.SetActive(false);
        }
    }

    public TutorialDialogPrompt SetMessage(string message)
    {
        if (currentDialog == null) currentDialog = new DialogData();
        currentDialog.Message = message;
        return this;
    }

    public TutorialDialogPrompt SetImage(Sprite image)
    {
        if (currentDialog == null) currentDialog = new DialogData();
        currentDialog.Image = image;
        return this;
    }

    public TutorialDialogPrompt OnClose(UnityAction action)
    {
        if (currentDialog == null) currentDialog = new DialogData();
        currentDialog.OnClose = action;
        return this;
    }

    public TutorialDialogPrompt OnPrevious(UnityAction action)
    {
        if (currentDialog == null) currentDialog = new DialogData();
        currentDialog.OnPrevious = action;

        // Enable the back button if OnPrevious is set
        backButton.interactable = true;
        return this;
    }

    public void Show()
    {
        bool hasPreviousAction = currentDialog?.OnPrevious != null;
        backButton.interactable = hasPreviousAction;

        if (backButton.transform.childCount > 0)
        {
            Image childImage = backButton.transform.GetChild(0).GetComponent<Image>();
            if (childImage != null)
            {
                childImage.color = hasPreviousAction ? Color.white : Color.gray;
            }
        }

        DisplayDialog();
    }

    private void DisplayDialog()
    {
        if (currentDialog != null)
        {
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
            onPreviousAction = currentDialog.OnPrevious;
        }
    }

    public void Hide()
    {
        child.SetActive(false);
        onCloseAction?.Invoke();
        onCloseAction = null;
    }

    private void GoBack()
    {
        if (onPreviousAction != null)
        {
            child.SetActive(false);
            onPreviousAction.Invoke();
        }
    }

    private class DialogData
    {
        public string Message;
        public Sprite Image;
        public UnityAction OnClose;
        public UnityAction OnPrevious; // Optional OnPrevious action
    }
}
