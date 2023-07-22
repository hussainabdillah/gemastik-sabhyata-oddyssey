using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Signal contextt;
    public bool playerInRange;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SelectedCharacter") && !other.isTrigger)
        {
            contextt.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SelectedCharacter") && !other.isTrigger)
        {
            contextt.Raise();
            playerInRange = false;
        }
    }
}