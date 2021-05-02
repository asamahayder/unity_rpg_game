using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Interactable
{

    protected GameObject playerCharacter;
    protected PathFinding pathfinder;
    ObjectMover objectMover;
    protected bool isInteracting = false; //this means that the actor is currently talking or fighting with someone.
    protected Animator animator;

    protected override void onInteract()
    {
        pathfinder.stopRoam();
        objectMover.clearPath(); //this stops the actor from following his current path.
        //objectMover.updatePath(null);
        isInteracting = true;
      
    }

    /// <summary>
    /// This function dictates when an interaction is over. 
    /// </summary>
    protected virtual void onEndInteraction()
    {
        isInteracting = false;
        StartCoroutine(waitBeforeStartRoam());
    }

    private IEnumerator waitBeforeStartRoam()
    {
        yield return new WaitForSeconds(3);
        pathfinder.startRoam();
    }

    protected override void onMouseOver()
    {
       
    }

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<PathFinding>();
        objectMover = GetComponent<ObjectMover>();
        playerCharacter = GameObject.Find("Character");
    }

    protected override void Update()
    {
        base.Update();
        if (isInteracting)
        {
            objectMover.turnTowardsTarget(playerCharacter.transform, 10f);
        }
    }


   
}
