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
    public TextMeshProUGUI ACPCount;

    [Header("Controls")]
    public Button ButtonControlClose;
    public Button ButtonControlLogout;
    public Button ButtonControlSaveChanges;

    [Header("Main")]
    public GameObject Canvas;

    private string initialName; // To store the initial name loaded from PlayerPrefs
    private bool isEditMode;


    public TextMeshProUGUI academicRank;
    void Start()
    {
        // Load the user's name and adventure points from PlayerPrefs
        initialName = PlayerPrefs.GetString("userName");
        InputName.text = initialName;
        ADPCount.text = PlayerPrefs.GetInt("adventure_points").ToString();
        ACPCount.text = PlayerPrefs.GetInt("academic_points").ToString();

        // Hide the Save Changes button initially
        ButtonControlSaveChanges.gameObject.SetActive(false);

        // Add listener to detect changes in the input field
        InputName.onValueChanged.AddListener(OnNameChanged);
    }

    void Update()
    {
        if(PlayerPrefs.GetInt("academic_points") < 200)
        {
            academicRank.text = "Academic Rank: B (Rookie)";
        } else if (PlayerPrefs.GetInt("academic_points") > 200)
        {
            academicRank.text = "Academic Rank: A (Elite)";

        }
        else if (PlayerPrefs.GetInt("academic_points") > 1000)
        {
            academicRank.text = "Academic Rank: S (Master)";

        }
        // Handle Edit mode UI
        if (isEditMode)
        {
            ButtonControlClose.gameObject.SetActive(false);
        }
        else
        {
            ButtonControlClose.gameObject.SetActive(true);
        }
    }

    // Method to detect when the input name has changed
    void OnNameChanged(string newName)
    {
        // Show Save Changes button only if the name has changed
        if (newName != initialName)
        {
            ButtonControlSaveChanges.gameObject.SetActive(true);
        }
        else
        {
            ButtonControlSaveChanges.gameObject.SetActive(false);
        }
    }

    // Method to handle when the Save Changes button is clicked
    public void SaveNameChanges()
    {
        SoundEffectManager.PlayButtonClick2();

        // Save the new name to PlayerPrefs
        PlayerPrefs.SetString("userName", InputName.text);
        PlayerPrefs.Save(); // Make sure to save changes to disk

        // Update the initial name to the new one
        initialName = InputName.text;

        // Hide the Save Changes button after saving
        ButtonControlSaveChanges.gameObject.SetActive(false);
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
