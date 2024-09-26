using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapControlSystem : MonoBehaviour
{
    [Header("Map Reference")]
    [SerializeField] private GameObject mapPanel;

    [Header("Map Holder")]
    [SerializeField] private Image Holder;



    [Header("Map Sprites")]
    [Header("Chapter 1")]
    [SerializeField] private Sprite LisbonMap;
    [SerializeField] private Sprite SevillMap;
    [SerializeField] private Sprite OutpostMap;
    [SerializeField] private Sprite RoyalPalaceMap;

    void SetMap()
    {
        if (SceneManager.GetActiveScene().name == "Chapter1Level1")
        {
            Holder.sprite = LisbonMap;
        }
        if (SceneManager.GetActiveScene().name == "Chapter1Level2")
        {
            Holder.sprite = OutpostMap;
        }
        if (SceneManager.GetActiveScene().name == "Chapter1Level3")
        {
            Holder.sprite = RoyalPalaceMap;
        }
        if (SceneManager.GetActiveScene().name == "Chapter1Level4")
        {
            Holder.sprite = LisbonMap;
        }
        if (SceneManager.GetActiveScene().name == "Chapter1Level5")
        {
            Holder.sprite = SevillMap;
        }
    }

    private bool isToggle = false;
    public void ToggleMap()
    {
        SoundEffectManager.PlayButtonClick2();
        SetMap();

        isToggle = !isToggle;
        if (isToggle)
        {
            mapPanel.SetActive(true);
        }
        else
        {
            mapPanel.SetActive(false);
        }
    }
}
