using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    public void TriggerDialogue()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            DialogueManager.GetInstance().SetSpeakerName(PlayerPrefs.GetString("CharacterName"));
        }
    }

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
        }
        else
        {
            visualCue.SetActive(false); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("SelectedCharacter"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("SelectedCharacter"))
        {
            playerInRange = false;
        }
    }
}
