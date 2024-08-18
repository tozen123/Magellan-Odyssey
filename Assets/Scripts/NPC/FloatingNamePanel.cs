using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FloatingNamePanel : MonoBehaviour
{
    public Character character;

    [Header("UI")]
    public GameObject ParentCanvas;
    public TextMeshProUGUI UIText;



    private void Start()
    {

        if (name == "")
        {
            UIText.text = "Default_NPC";
        }
        UIText.text = character.name;

        ParentCanvas.SetActive(false);
    }
   

}
