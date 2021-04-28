using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehavior : Interactable
{

    [SerializeField] private Texture2D cursorImage;

    protected override void onInteract()
    {
        print("Hello i am a sphere. Whatsup my G");
    }

    protected override void onMouseOver()
    {
        
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (isMouseOver) Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }

}
