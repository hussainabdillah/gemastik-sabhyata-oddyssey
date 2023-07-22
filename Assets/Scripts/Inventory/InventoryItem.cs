using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage; // Corrected the field name
    public int numberHeld;
    public bool usable;
    public bool unique;
    public UnityEvent thisEvent;

    public void Use()
    {
        Debug.Log("Using Item");
        thisEvent?.Invoke(); // Use the null-conditional operator to safely invoke the event
    }
}
