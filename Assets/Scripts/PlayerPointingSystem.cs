using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPointingSystem : MonoBehaviour
{
    public static PlayerPointingSystem Instance { get; private set; }

    [SerializeField] private int PLAYER_CURRENT_ADP;

    [Header("UI REFERENCES")]
    [SerializeField] private TextMeshProUGUI ADP;
    [SerializeField] private TextMeshProUGUI ACP;
    [SerializeField] private RectTransform ADP_Parent;

    [Header("Animation Settings")]
    [SerializeField] private float scaleDuration = 0.2f;
    [SerializeField] private float scaleFactor = 0.8f;
    [SerializeField] private float incrementSpeed = 50f;


    private int totalGatheredPoints = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
            return;
        }

        if (PlayerPrefs.HasKey("adventure_points"))
        {
            PLAYER_CURRENT_ADP = PlayerPrefs.GetInt("adventure_points");
            ADP.text = PLAYER_CURRENT_ADP.ToString();
        }
    }
    void Update()
    {
        int count = PlayerPrefs.GetInt("Chapter1TotalQuizScore", 0);
        ACP.text = count.ToString();
    }
    public void AddPoints(int points)
    {
        totalGatheredPoints += points;  
        StartCoroutine(AnimateAddPoints(points));
    }

    public void ResetGatheredPoints()
    {
        int oldPoints = PlayerPrefs.GetInt("adventure_points", 0);
        PlayerPrefs.SetInt("adventure_points", oldPoints - totalGatheredPoints);
        totalGatheredPoints = 0;
        PlayerPrefs.Save();
    }

    private IEnumerator AnimateAddPoints(int points)
    {
        int targetPoints = PLAYER_CURRENT_ADP + points;

        // Animate the scaling of ADP_Parent
        StartCoroutine(AnimateScale());

        // Animate the incrementing of points
        while (PLAYER_CURRENT_ADP < targetPoints)
        {
            PLAYER_CURRENT_ADP += Mathf.CeilToInt(Time.deltaTime * incrementSpeed);
            if (PLAYER_CURRENT_ADP > targetPoints)
            {
                PLAYER_CURRENT_ADP = targetPoints;
            }

            ADP.text = PLAYER_CURRENT_ADP.ToString();
            PlayerPrefs.SetInt("adventure_points", PLAYER_CURRENT_ADP);

            yield return null;
        }

        PlayerPrefs.Save();
        Debug.Log("adding adp");
    }

    private IEnumerator AnimateScale()
    {
        Vector3 originalScale = ADP_Parent.localScale;
        Vector3 targetScale = originalScale * scaleFactor;

        // Scale down
        float elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            elapsedTime += Time.deltaTime;
            ADP_Parent.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / scaleDuration);
            yield return null;
        }

        // Scale back up
        elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            elapsedTime += Time.deltaTime;
            ADP_Parent.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / scaleDuration);
            yield return null;
        }

        ADP_Parent.localScale = originalScale; // Ensure it's set to original scale at the end
    }

    public int GetPoints()
    {
        return PLAYER_CURRENT_ADP;
    }
}
