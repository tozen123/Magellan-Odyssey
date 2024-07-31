using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetupControllerSequence0 : MonoBehaviour
{
    [Header("Creation Inputs")]
    public TextMeshProUGUI name;
    public TextMeshProUGUI email;
    public TextMeshProUGUI password;
    public TextMeshProUGUI confirm_password;

    public GameObject creationCanvas;
    void Start()
    {
        creationCanvas.SetActive(false);

        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Magellan Odyssey")
               .Show();

        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Hello, Player. Before you start the game, you need to have an avatar.")
               .OnClose(ShowCreationAvatarCanvas)
               .Show();

    }

    void Update()
    {
        
    }

    void ShowCreationAvatarCanvas()
    {
        creationCanvas.SetActive(true);


    }

    public void CreateAvatar()
    {
        string avatar_name = name.text;
        string avatar_email = email.text;
        string avatar_password = password.text;
        string avatar_confirm_password = confirm_password.text;


        Debug.Log(avatar_name);
        Debug.Log(avatar_email);
        Debug.Log(avatar_password);
        Debug.Log(avatar_confirm_password);
    }
}
