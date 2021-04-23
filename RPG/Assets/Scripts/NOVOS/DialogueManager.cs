using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text nameText, dialogueText;
    public GameObject painel;

    public  Queue<string>   senteces;           // frases do dialogo

    // Start is called before the first frame update
    void Start()
    {
        senteces = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue) {
        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;
        painel.SetActive(true);

        senteces.Clear();

        foreach(string sentence in dialogue.sentences) {
            senteces.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(senteces.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = senteces.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = sentence;
    }

    void EndDialogue() {
        painel.SetActive(false);
        Debug.Log("Finish");
        SceneManager.LoadScene("Lab5_Vitoria");
    }

    


}
