using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarMenuController : MonoBehaviour
{
    [Header("Inputs")]
    public TMP_InputField InputName;
    public TMP_InputField InputEmail;
    public TMP_InputField InputPassword;

    [Header("Controls")]
    public Button ButtonControlEdit;
    public Button ButtonControlSave;
    public Button ButtonControlCancel;
    
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
            ButtonControlEdit.gameObject.SetActive(false);

            ButtonControlSave.gameObject.SetActive(true);
            ButtonControlCancel.gameObject.SetActive(true);

            ButtonControlClose.gameObject.SetActive(false);
            ButtonControlLogout.gameObject.SetActive(false);
        } 
        else
        {
            ButtonControlEdit.gameObject.SetActive(true);

            ButtonControlSave.gameObject.SetActive(false);
            ButtonControlCancel.gameObject.SetActive(false);

            ButtonControlClose.gameObject.SetActive(true);
            ButtonControlLogout.gameObject.SetActive(true);
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
            .SetMessage("Do you really want to logout?")
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
