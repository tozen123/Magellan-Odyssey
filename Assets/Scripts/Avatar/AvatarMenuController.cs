using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarMenuController : MonoBehaviour
{
    [Header("Inputs")]
    public TMP_InputField InputName;
    public TextMeshProUGUI ADPCount;

    [Header("Controls")]
    
    public Button ButtonControlClose;
    public Button ButtonControlLogout;

    [Header("Main")]
    public GameObject Canvas;
    bool isEditMode;
    void Start()
    {
        InputName.text = PlayerPrefs.GetString("userName");
        ADPCount.text = PlayerPrefs.GetInt("adventure_points").ToString();
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
        SoundEffectManager.PlayButtonClick2();
        Canvas.SetActive(state);
    }

    public void Logout()
    {
        SoundEffectManager.PlayButtonClick2();
        DialogMessagePromptAction.Instance
            .SetTitle("Confirmation")
            .SetMessage("Do you really want to clear your avatar, this action is not reversible and all of the progress will be deleted permanently?")
            .SetFadeInDuration(0.5f)
            .OnPositive(ConfirmLogout)
            .Show();

       

    }

    void ConfirmLogout()
    {
        SoundEffectManager.PlayButtonClick2();
        PlayerPrefs.DeleteAll();
        LoadingScreenManager.Instance.LoadScene("Setup-Sequence0");
    }


}
