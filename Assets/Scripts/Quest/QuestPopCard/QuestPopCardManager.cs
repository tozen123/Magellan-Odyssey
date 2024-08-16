using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class QuestPopCardManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI titleUIText;
    [SerializeField] private TextMeshProUGUI messageUIText;
    [SerializeField] private Button closeUIButton;
    [SerializeField] private float fadeInDuration = 0.5f; 

    private CanvasGroup canvasGroup;

    public static QuestPopCardManager Instance;

    [HideInInspector] public bool IsActive = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        canvasGroup = canvas.GetComponent<CanvasGroup>();

        closeUIButton.onClick.RemoveAllListeners();
        closeUIButton.onClick.AddListener(() => {
            Hide();
        });
    }

    public QuestPopCardManager SetTitle(string title)
    {
        titleUIText.text = title;
        return this;
    }

    public QuestPopCardManager SetMessage(string message)
    {
        messageUIText.text = message;
        return this;
    }

    public QuestPopCardManager SetFadeInDuration(float duration)
    {
        fadeInDuration = duration;
        return this;
    }

    public QuestPopCardManager OnClose(UnityAction action)
    {
        closeUIButton.onClick.AddListener(action);
        return this;
    }

    public void Show()
    {
        canvas.SetActive(true);
        IsActive = true;
        StartCoroutine(FadeIn(fadeInDuration));
    }

    public void Hide()
    {
        canvas.SetActive(false);
        IsActive = false;

        StopAllCoroutines();
    }

    private IEnumerator FadeIn(float duration)
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
}
