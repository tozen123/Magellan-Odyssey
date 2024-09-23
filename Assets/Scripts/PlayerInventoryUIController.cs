using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // If using TextMeshPro

public class PlayerInventoryUIController : MonoBehaviour
{
    [Header("Inventory Slots")]
    public List<Image> inventorySlotIcons = new List<Image>(); // The UI slots for displaying item icons
    public List<TextMeshProUGUI> inventorySlotNames = new List<TextMeshProUGUI>(); // The UI slots for displaying item names (or TextMeshPro)

    public Sprite defaultSlotSprite; // Default empty slot sprite

    [Header("Inventory System Reference")]
    public PlayerInventorySystem playerInventorySystem;


    [Header("Inventory System Reference")]
    [SerializeField] private GameObject inventoryPanel;
    private void Start()
    {
        InitializeInventoryUI();
    }

    // Initializes the inventory UI with empty slots
    private void InitializeInventoryUI()
    {
        for (int i = 0; i < inventorySlotIcons.Count; i++)
        {
            inventorySlotIcons[i].sprite = defaultSlotSprite; // Set default sprite for empty slots
            inventorySlotIcons[i].color = new Color(1, 1, 1, 0.2f); // Semi-transparent to indicate emptiness
            inventorySlotNames[i].text = "Empty"; // Clear the text to show the slot is empty
        }
    }

    // Updates the inventory UI when an item is picked up
    public void UpdateInventoryUI()
    {
        InitializeInventoryUI(); // Reset inventory UI

        for (int i = 0; i < playerInventorySystem.items.Count; i++)
        {
            if (i < inventorySlotIcons.Count)
            {
                inventorySlotIcons[i].sprite = playerInventorySystem.items[i].itemIcon; // Set item icon
                inventorySlotIcons[i].color = new Color(1, 1, 1, 1); // Fully opaque to indicate item presence
                inventorySlotNames[i].text = playerInventorySystem.items[i].itemName; // Set item name
            }
        }
    }

    // Checks if there is space in the inventory for a new item
    public bool CanAddItem()
    {
        return playerInventorySystem.items.Count < inventorySlotIcons.Count; // Check if there's room for more items
    }
    private bool isToggle = false;
    public void ToggleInventory()
    {
        isToggle = !isToggle;
        if (isToggle)
        {
            inventoryPanel.SetActive(true);
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }
}
