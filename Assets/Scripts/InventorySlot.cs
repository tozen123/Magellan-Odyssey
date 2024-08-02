using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI name;

    public void AddItem(PickableObject item)
    {
        icon.sprite = item.itemIcon;
        icon.enabled = true;

        name.text = item.itemName;
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        

        name.text = "";
    }
}

