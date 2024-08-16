using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarMenuController : MonoBehaviour
{
    [Header("Inputs")]
    public TMP_InputField InputName;
  

    [Header("Controls")]
    
    public Button ButtonControlClose;
    public Button ButtonControlLogout;
    [Header("Main")]
    public GameObject Canvas;
    bool isEditMode;
    void Start()
    {
        
    }

    void Update()
    {
        if (isEditMode)
        {


            ButtonControlClose.gameObject.SetActive(false);
            //ButtonControlLogout.gameObject.SetActive(false);
        } 
        else
        {


            ButtonControlClose.gameObject.SetActive(true);
            //ButtonControlLogout.gameObject.SetActive(true);
        }
    }

    public void SetEditMode(bool state)
    {
        isEditMode = state;
    }

    public void SetCanvasState(bool state)
    {
        Canvas.SetActive(state);
    }

    public void Logout()
    {

        DialogMessagePromptAction.Instance
            .SetTitle("Confirmation")
            .SetMessage("Do you really want to clear your avatar, this action is not reversible and all of the progress will be deleted permanently?")
            .SetFadeInDuration(0.5f)
            .OnPositive(ConfirmLogout)
            .Show();

       

    }

    void ConfirmLogout()
    {
        PlayerPrefs.DeleteAll();
        LoadingScreenManager.Instance.LoadScene("Setup-Sequence0");
    }


}
