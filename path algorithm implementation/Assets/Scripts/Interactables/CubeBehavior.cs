using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : Interactable
{
    protected override void onInteract()
    {
        print("Psssst, Beware of the sketchy sphere over there");
    }

    protected override void onMouseOver()
    {
        
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

}
