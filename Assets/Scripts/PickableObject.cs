using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public string itemName;      // The name of the item
    public Sprite itemIcon;      // The icon that will be displayed in the inventory
    public bool isClickable;     // Indicates if the item is clickable in the inventory
}
