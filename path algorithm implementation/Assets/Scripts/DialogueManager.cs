using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //This class is inspired by the following tutorial: https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Animator dialogueBoxAnimator;

    private Queue<string> sentences;

    private GameObject currentNPC;


    // Start is called before the first frame update
    void Start()
    {

        dialogueBoxAnimator = GameObject.Find("Canvas").transform.Find("DialogueBox").gameObject.GetComponent<Animator>();

        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startDialogue(Dialogue dialogue, GameObject gameObject)
    {

        currentNPC = gameObject;

        string name = gameObject.name;

        dialogueBoxAnimator.SetBool("isActive", true);

        nameText.text = name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        displayNextSentence();

    }

    public void displayNextSentence()
    {
        if(sentences.Count == 0)
        {
            endDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(typeSentence(sentence));
    }

    IEnumerator typeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.02f);
        }
    }

    public void endDialogue()
    {
        if (!dialogueBoxAnimator.GetBool("isActive"))
        {//skip if already not opened
            return;
        }
            
        print("end dialogue");
        dialogueBoxAnimator.SetBool("isActive", false);

        if(currentNPC != null)
        {
            currentNPC.GetComponent<NPCbehavior>().endInteraction();
        }
        
    }
}
