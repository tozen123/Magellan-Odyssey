using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public Image foregroundHealthBar; 
    public Image backgroundHealthBar; 

    public float backgroundHealthBarLerpSpeed = 2f; 
    public float backgroundHealthBarDelay = 0.5f; 

    void Start()
    {
        currentHealth = maxHealth;
        UpdateForegroundHealthBar();
        UpdateBackgroundHealthBar();
    }
    private bool defeatDialogShown = false;
    void Update()
    {
        if (backgroundHealthBar.fillAmount > foregroundHealthBar.fillAmount)
        {
            backgroundHealthBar.fillAmount = Mathf.Lerp(backgroundHealthBar.fillAmount, foregroundHealthBar.fillAmount, Time.deltaTime * backgroundHealthBarLerpSpeed);
        }
        if (currentHealth <= 0 && !defeatDialogShown)
        {
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("You have been defeated. The level will restart.")
                .OnClose(RestartScene) 
                .Show();

            defeatDialogShown = true;
        }
    }

    void UpdateForegroundHealthBar()
    {
        foregroundHealthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    void UpdateBackgroundHealthBar()
    {
        backgroundHealthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateForegroundHealthBar();

        

        StartCoroutine(UpdateBackgroundHealthBarDelayed());
    }

    IEnumerator UpdateBackgroundHealthBarDelayed()
    {
        yield return new WaitForSeconds(backgroundHealthBarDelay);

        while (backgroundHealthBar.fillAmount > foregroundHealthBar.fillAmount)
        {
            backgroundHealthBar.fillAmount = Mathf.Lerp(backgroundHealthBar.fillAmount, foregroundHealthBar.fillAmount, Time.deltaTime * backgroundHealthBarLerpSpeed);
            yield return null;
        }

        backgroundHealthBar.fillAmount = foregroundHealthBar.fillAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemyAttackBox")
        {
            TakeDamage(10);

        }
    }
    void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // Get the current scene
        SceneManager.LoadScene(currentScene.name); // Reload the current scene
    }
}
