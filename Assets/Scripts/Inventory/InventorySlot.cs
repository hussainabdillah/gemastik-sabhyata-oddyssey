using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Stuff to change")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Variables from the item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;

        if (thisItem != null)
        {
            itemImage.sprite = thisItem.itemImage; // Corrected the sprite assignment
            itemNumberText.text = thisItem.numberHeld.ToString(); // Removed unnecessary string concatenation
        }
        else
        {
            itemImage.sprite = null; // Reset the sprite if the item is null
            itemNumberText.text = "";
        }
    }

    public void ClickedOn()
    {
        if (thisItem != null)
        {
            thisManager.SetupDescriptionAndButton(thisItem.itemDescription, thisItem.usable, thisItem);
        }
    }
}
