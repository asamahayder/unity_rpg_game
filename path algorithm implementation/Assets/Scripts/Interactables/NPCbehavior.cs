using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCbehavior : Actor
{

    public string message;
    [SerializeField] private Texture2D cursorImage;
    protected override void onInteract()
    {
        base.onInteract();
        print(message);

    }

    protected override void onMouseOver()
    {
        base.onMouseOver();
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (isMouseOver) Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }

}
