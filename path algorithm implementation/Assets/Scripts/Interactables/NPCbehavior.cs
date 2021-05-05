using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCbehavior : Actor
{

    public string message;
    [SerializeField] private Texture2D cursorImage;

    DialogueTrigger dialogueTrigger;


    protected override void Start()
    {
        base.Start();
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    protected override void onInteract()
    {
        base.onInteract();
        print(message);

        dialogueTrigger.triggerDialogue();

        onEndInteraction();
    }

    protected override void onMouseOver()
    {
        base.onMouseOver();
        
    }

    protected override void onEndInteraction()
    {
        base.onEndInteraction();

    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (isMouseOver) Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }

}
