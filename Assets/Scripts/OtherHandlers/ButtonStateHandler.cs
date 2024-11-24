using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStateHandler : MonoBehaviour
{
    [SerializeField] private Image ChildImage; 
    [SerializeField] private Sprite lockImage; 
    [SerializeField] private Sprite doneImage;
    [SerializeField] private Sprite checkImage; 

    private Button thisButton; 
 
    void Start()
    {
        doneImage = transform.GetChild(0).GetComponent<Image>().sprite;

        // Get the Button component attached to this game object
        thisButton = this.gameObject.GetComponent<Button>();
        // Check for null references
        if (ChildImage == null)
        {
            Debug.LogWarning("ChildImage (Image component) is not assigned in the Inspector on " + gameObject.name);
        }
        if (thisButton == null)
        {
            Debug.LogWarning("Button component is missing on " + gameObject.name);
        }
    }

    // Method to set the button to a locked state
    public void SetToLockState()
    {
        if (ChildImage != null && lockImage != null)
        {
            ChildImage.sprite = lockImage;

            if (thisButton != null)
            {
                thisButton.interactable = false; // Disable interaction
            }
        }
        else
        {
            Debug.LogWarning("Cannot set to lock state. Either ChildImage or lockImage is null on " + gameObject.name);
        }
    }

    // Method to set the button to an unlocked state
    public void SetToUnLockState()
    {
        if (ChildImage != null)
        {
            ChildImage.sprite = doneImage; // Remove the lock image
        }
        else
        {
            Debug.LogWarning("ChildImage (Image component) is not assigned in the Inspector on " + gameObject.name);
        }

        if (thisButton != null)
        {
            thisButton.interactable = true; // Enable interaction
        }
    }

    // Method to set the button to a completed state
    public void SetToCheckState()
    {

        if (ChildImage != null && doneImage != null)
        {
            ChildImage.sprite = checkImage; // Set the completed image
        }
        else
        {
            Debug.LogWarning("Cannot set to check state. Either ChildImage or doneImage is null on " + gameObject.name);
        }

        if (thisButton != null)
        {
            thisButton.interactable = false; // Disable interaction for completed state
        }
    }
}
