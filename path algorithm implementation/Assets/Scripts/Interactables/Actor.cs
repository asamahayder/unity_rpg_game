using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Interactable
{

    PathFinding pathfinder;
    ObjectMover objectMover;

    protected override void onInteract()
    {
        pathfinder.stopRoam();
        objectMover.updatePath(null);
    }

    protected override void onMouseOver()
    {
       
    }

    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<PathFinding>();
        objectMover = GetComponent<ObjectMover>();
    }
}
