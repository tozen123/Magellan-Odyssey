using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextActionUpdateSystem : MonoBehaviour
{
    public TextMeshProUGUI textUpdate;
    public Animator textAnim;
    void Start()
    {
        textUpdate.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateActionUpdateText(string text)
    {
        textUpdate.enabled = true;

        textUpdate.text = text;

        textAnim.SetTrigger("Update");
    }
    public void HideText()
    {
        textUpdate.enabled = false;
    }

}
