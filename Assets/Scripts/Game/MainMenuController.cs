using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuController : MonoBehaviour
{

    User user;

    [Header("Headers")]
    public TextMeshProUGUI WelcomeText;

    [SerializeField] private GameObject ButtonChapter;

    void Start()
    {
        ButtonChapter.SetActive(true);

        if (CreateUser())
        {
            WelcomeText.text = "Welcome, " + user.name;
        }
        else
        {
            PlayerPrefs.DeleteAll();

            LoadingScreenManager.Instance.LoadScene("Setup-Sequence0");
        }
        


    }

    bool CreateUser()
    {
        if (PlayerPrefs.HasKey("userName") )
        {
            user = new User
            {
                name = PlayerPrefs.GetString("userName"),
            
            };

            return true;
        } 
        else
        {
            return false;
        }
    }

    void Update()
    {
        
    }
}
