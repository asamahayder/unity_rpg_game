using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //This class is inspired by the following tutorial: https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys

    public Dialogue dialogue;

    public void triggerDialogue()
    {
        FindObjectOfType<DialogueManager>().startDialogue(dialogue, gameObject);
    }
}
