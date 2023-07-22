using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;

    [Header("Signals and Dialog")]
    public Signal raiseItem;
    public Signal context;
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    [Header("Animation")]
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (!isOpen)
            {
                // Open the chest
                OpenChest();
            }
            else
            {
                // Chest is already open
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        // Dialog window on
        dialogBox.SetActive(true);
        // dialog text = contents text
        dialogText.text = contents.itemDescription;
        // add contents to the inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        // Raise the signal to the player to animate
        raiseItem.Raise();
        // raise the context clue
        context.Raise();
        // set the chest to opened
        isOpen = true;
        anim.SetBool("opened", true);
    }

    public void ChestAlreadyOpen()
    {
        // Dialog off
        dialogBox.SetActive(false);
        // raise the signal to the player to stop animating
        raiseItem.Raise();

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SelectedCharacter") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SelectedCharacter") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}