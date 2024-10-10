using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float staticDuration = 2.0f;

    private bool errorOccurred = false; // To track if the error occurred

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        // Check if the scene exists before attempting to load it
        if (IsSceneValid(sceneName))
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }
        else
        {
            // Display error message if the scene does not exist
            StartCoroutine(ShowErrorMessage("Encountered a Game Error: Scene Not Found"));
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return StartCoroutine(FadeIn());

        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingText.text = "Loading... " + (progress * 100f).ToString("F0") + "%";
            yield return null;
        }

        yield return new WaitForSeconds(staticDuration);
        yield return StartCoroutine(FadeOut());

        loadingScreen.SetActive(false);
    }

    private IEnumerator ShowErrorMessage(string message)
    {
        yield return StartCoroutine(FadeIn());

        loadingScreen.SetActive(true);
        loadingText.text = message;
        errorOccurred = true;

        // Wait for a click input to proceed to MainMenu-Sequence1
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        // Reset error state and load fallback scene
        errorOccurred = false;
        LoadScene("MainMenu-Sequence1");
    }

    private IEnumerator FadeIn()
    {
        float startTime = Time.time;
        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        float startTime = Time.time;
        float alpha = 1f;

        while (alpha > 0f)
        {
            alpha = Mathf.Lerp(1f, 0f, (Time.time - startTime) / fadeDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }

    // Utility method to check if the scene is valid
    private bool IsSceneValid(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string scene = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (scene.Equals(sceneName, System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        // If error has occurred, wait for a click to go to MainMenu-Sequence1
        if (errorOccurred && Input.GetMouseButtonDown(0))
        {
            LoadScene("MainMenu-Sequence1");
        }
    }
}
