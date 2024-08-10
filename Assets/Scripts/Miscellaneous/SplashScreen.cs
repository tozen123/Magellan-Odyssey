using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    public CanvasGroup splashCanvasGroup; // Reference to the CanvasGroup component
    public string nextScene; // Name of the scene to load after the splash screen
    public float fadeDuration = 2f; // Duration of the fade effect

    private void Start()
    {
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // Fade in
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));
        // Wait for a few seconds (optional)
        yield return new WaitForSeconds(2f);
        // Fade out
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));
        // Load the next scene
        SceneManager.LoadScene(nextScene);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            splashCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }

        splashCanvasGroup.alpha = endAlpha;
    }
}
