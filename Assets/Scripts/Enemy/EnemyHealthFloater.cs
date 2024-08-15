using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthFloater : MonoBehaviour
{
    [Header("Bars")]
    [SerializeField] private Image parentBar;
    [SerializeField] private Image foregroundBar;
    [SerializeField] private Image foregroundWhiteBar;

    [Header("References")]
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Canvas thisCanvas;

    [Header("Settings")]
    [SerializeField] private float whiteBarDelay = 0.5f;
    [SerializeField] private float whiteBarLerpSpeed = 0.5f;
    [SerializeField] private float scaleUpAmount = 1.1f;
    [SerializeField] private float scaleDuration = 0.2f;

    private Vector3 originalScale;
    private float lastHealth;

    private void Start()
    {
        originalScale = parentBar.rectTransform.localScale;
        lastHealth = enemyController.currentHealth;
    }

    private void Update()
    {

        float healthPercent = enemyController.currentHealth / enemyController.maxHealth;

        if (healthPercent < 1)
        {
            thisCanvas.enabled = true;
        }
        else
        {
            thisCanvas.enabled = false;

        }

        foregroundBar.fillAmount = healthPercent;

        if (enemyController.currentHealth < lastHealth)
        {
            StartCoroutine(ScaleHealthBar());
        }

        if (foregroundWhiteBar.fillAmount > healthPercent)
        {
            StartCoroutine(LerpWhiteBar(healthPercent));
        }

        lastHealth = enemyController.currentHealth;
    }

    private IEnumerator LerpWhiteBar(float targetFillAmount)
    {
        yield return new WaitForSeconds(whiteBarDelay);

        while (foregroundWhiteBar.fillAmount > targetFillAmount)
        {
            foregroundWhiteBar.fillAmount = Mathf.Lerp(foregroundWhiteBar.fillAmount, targetFillAmount, Time.deltaTime * whiteBarLerpSpeed);
            yield return null;
        }

        foregroundWhiteBar.fillAmount = targetFillAmount;
    }

    private IEnumerator ScaleHealthBar()
    {
        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            parentBar.rectTransform.localScale = Vector3.Lerp(originalScale, originalScale * scaleUpAmount, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        parentBar.rectTransform.localScale = originalScale * scaleUpAmount;

        elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            parentBar.rectTransform.localScale = Vector3.Lerp(originalScale * scaleUpAmount, originalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        parentBar.rectTransform.localScale = originalScale;
    }
}
