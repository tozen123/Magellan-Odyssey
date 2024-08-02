using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterMenuHandler : MonoBehaviour
{
    public GameObject Chapter1Map;
    public GameObject ChapterScrollViewHandler;
    public void SetStateChapter1Map(bool state)
    {
        if (state == true)
        {
            ChapterScrollViewHandler.SetActive(false);

        }
        else
        {
            ChapterScrollViewHandler.SetActive(true);

        }

        Chapter1Map.SetActive(state);
    }
}
