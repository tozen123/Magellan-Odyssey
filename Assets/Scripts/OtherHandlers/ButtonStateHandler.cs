using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStateHandler : MonoBehaviour
{
    [SerializeField] private Image ChildImage; // Reference to the image component that displays the lock icon

    private Sprite lockImage; // Sprite for the lock icon
    private Sprite doneImage; // Sprite for the lock icon

    private Button thisButton; // Reference to the Button component

    void Start()
    {
        // Load the lock image sprite
        Sprite loadedSprite1 = Resources.Load<Sprite>("Images/lock_icon");
        Sprite loadedSprite2 = Resources.Load<Sprite>("Images/check_icon");
        if (loadedSprite1 != null)
        {
            lockImage = loadedSprite1;
        }
        else
        {
            Debug.LogWarning("Lock image not found in Resources folder");
        }
         if (loadedSprite2 != null)
        {
            doneImage = loadedSprite2;
        }
        else
        {
            Debug.LogWarning("Lock image not found in Resources folder");
        }

        // Get the Button component attached to this game object
        thisButton = this.gameObject.GetComponent<Button>();

        // Check for null references
        if (ChildImage == null)
        {
            Debug.LogWarning("ChildImage is not assigned in the Inspector on " + gameObject.name);
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
            ChildImage.preserveAspect = true;

       

            if (thisButton != null)
            {
                thisButton.interactable = false;
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
            ChildImage.sprite = null;
            ChildImage.preserveAspect = true;
        }

        
        else
        {
            Debug.LogWarning("Text GameObject is not assigned in the Inspector on " + gameObject.name);
        }

        if (thisButton != null)
        {
            thisButton.interactable = true;
        }
    }

    public void SetToCheckState()
    {
        if (ChildImage != null)
        {
            ChildImage.sprite = doneImage;
            ChildImage.preserveAspect = true;
        }

        
        else
        {
            Debug.LogWarning("Text GameObject is not assigned in the Inspector on " + gameObject.name);
        }

        if (thisButton != null)
        {
            thisButton.interactable = false;
        }
    }
}
