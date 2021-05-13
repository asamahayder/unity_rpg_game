using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCbehavior : Actor
{

    DialogueTrigger dialogueTrigger;


    protected override void Start()
    {
        base.Start();
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    protected override void Update()
    {
        base.Update();
        print(pathfinder.numberOfRoutinesRunning);
    }

    protected override void onInteract()
    {
        base.onInteract();
        dialogueTrigger.triggerDialogue();
    }

    protected override void onMouseOver()
    {
        base.onMouseOver();
        
    }

    protected override void onEndInteraction()
    {
        base.onEndInteraction();
    }

    public void endInteraction() //used as a workaround to the fact that onEndInteraction is protected.
    {
        onEndInteraction();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (isMouseOver) Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }

}
