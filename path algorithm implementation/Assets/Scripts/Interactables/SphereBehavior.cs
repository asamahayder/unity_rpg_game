using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehavior : Interactable
{


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
       
    }

}
