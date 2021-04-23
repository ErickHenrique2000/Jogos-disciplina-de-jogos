using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public  Dialogue    dialogue;
    public  bool        interagil = false;

    private void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
       TriggerDialogue();
    }

    public void TriggerDialogue() {
        if(interagil == false) { 
            interagil = true;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}
