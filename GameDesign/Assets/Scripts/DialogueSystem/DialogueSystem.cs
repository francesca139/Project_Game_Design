using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Text nameText; //nome della persona con cui si parla
    public Text dialogueText;

    //   public Animator animator; aggiungiamo un'animazione?

    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue d)
    {
        Debug.Log("Start conversation with " + d.name);

        //    animator.SetBool("isOpen", true);
        //in this way a window will appear a dialogue box
        nameText.text = d.name;
        sentences.Clear();

        foreach (string sentence in d.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        // dialogueText.text = sentence;

    }


    IEnumerator TypeSentence(string sentence) //scrive lettera per lettera
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        //  animator.SetBool("isOpen", false); aggiungiamo un'animazione?
        Debug.Log("End of conversation");
    }


}

