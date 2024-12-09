using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnlockMode();
    }

    void UnlockMode()
    {

        //UnlockKabanatas
        PlayerPrefs.SetInt("Kabanata1BookOfTrivia_IsLock", 0);
        PlayerPrefs.SetInt("Kabanata2BookOfTrivia_IsLock", 0);
        PlayerPrefs.SetInt("Kabanata3BookOfTrivia_IsLock", 0);
        PlayerPrefs.SetInt("Kabanata4BookOfTrivia_IsLock", 0);
        PlayerPrefs.SetInt("Kabanata5BookOfTrivia_IsLock", 0);
        PlayerPrefs.SetInt("Kabanata6BookOfTrivia_IsLock", 0);

        //GetPoints

        PlayerPrefs.SetInt("adventure_points", 9999);
        PlayerPrefs.SetInt("Chapter1TotalQuizScore", 9999);

        //ChapterUnlockeds

        PlayerPrefs.SetString("Chapter1Level1", "IN_PROGRESS");
        PlayerPrefs.SetString("Chapter1Level2", "IN_PROGRESS");
        PlayerPrefs.SetString("Chapter1Level3", "IN_PROGRESS");
        PlayerPrefs.SetString("Chapter1Level4", "IN_PROGRESS");
        PlayerPrefs.SetString("Chapter1Level5", "IN_PROGRESS");
        PlayerPrefs.SetString("Chapter1Level6", "IN_PROGRESS");

        //Save

        PlayerPrefs.Save();

    }
}
