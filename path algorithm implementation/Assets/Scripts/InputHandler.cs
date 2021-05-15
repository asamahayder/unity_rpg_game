using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    PathFinding pathfinder;
    Outline outline;
    ObjectMover objectMover;

    public Texture2D noActionCursor;


    private bool movingTowardsTarget = false;

    public float minimumDistanceToTarget = 5;

    private GameObject targetObject;
    private GameObject interactableObject;

    private DialogueManager dialogueManager;

    private Animator characterAnimator;

    GraphicRaycaster raycaster;


    // Start is called before the first frame update
    void Start()
    {
        pathfinder = GetComponent<PathFinding>();
        outline = GetComponent<Outline>();
        objectMover = GetComponent<ObjectMover>();
        outline.OutlineMode = Outline.Mode.SilhouetteOnly;
        outline.OutlineColor = Color.gray;
        raycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();

        dialogueManager = GameObject.Find("DialogueManager").gameObject.GetComponent<DialogueManager>();
        characterAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        interactableObject = GetInterActableObject(); //we get every frame because we need to check if mouse is over an interactable object


        if(interactableObject != null && interactableObject.TryGetComponent(out IInteractable thing)) //the TryGetComponent is used to check if the interactable has implemented the nessesary functions for interactions.
        {
            thing.onMouseOver();
        }
        else if(interactableObject != null){
            //on ground
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            //on nothing
            Cursor.SetCursor(noActionCursor, Vector2.zero, CursorMode.ForceSoftware);
        }

        if (Input.GetMouseButtonDown(0))
        {
            characterAnimator.SetBool("isWoodCutting", false);
            if (interactableObject != null && interactableObject.name.Equals("Terrain")) //Mouse on ground
            {
                
                dialogueManager.endDialogue();
                pathfinder.findPath(GetMouseClickWorldPosition(), false);
                movingTowardsTarget = false;
                targetObject = null;
            }
            else if(interactableObject != null) //Mouse on interactable item
            {
                targetObject = interactableObject;
                pathfinder.findPath(targetObject.transform.position, true);
                movingTowardsTarget = true;
            }
           
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

       
        if (targetObject != null)
        {
            if (movingTowardsTarget && Vector3.Distance(transform.position, targetObject.transform.position) <= minimumDistanceToTarget && targetObject.TryGetComponent(out IInteractable thing2))
            {
                movingTowardsTarget = false;
                thing2.onInteract();
            }

            //we have reached the target
            if(targetObject != null && !movingTowardsTarget && objectMover != null)
            {
                objectMover.turnTowardsTarget(targetObject.transform, 10f);
            }

            //Calculate path every frame in case the target is an NPC that is moving
            if (movingTowardsTarget && Vector3.Distance(transform.position, targetObject.transform.position) > minimumDistanceToTarget)
            {
                pathfinder.findPath(targetObject.transform.position, true);
            }
        }
        
    }

    private Vector3 GetMouseClickWorldPosition()
    {
        LayerMask groundMask = LayerMask.GetMask("Ground");

        Vector3 clickPosition = -1000 * Vector3.one;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, groundMask))
        {
            clickPosition = hit.point;
            
        }

        return clickPosition;

    }


    private GameObject getUIObject()
    {
        PointerEventData mouseData = new PointerEventData(EventSystem.current);
        List<RaycastResult> hits = new List<RaycastResult>();
        
        mouseData.position = Input.mousePosition;
        raycaster.Raycast(mouseData, hits);

        if (hits.Count > 0)
        {
            return hits[0].gameObject;
        }
        else
        {
            return null;
        }

    }

    private GameObject GetInterActableObject()
    {
        GameObject gameObject = null;
        //First we check if we are on a ui object:
        gameObject = getUIObject();

        if (gameObject != null)
        {
            if (gameObject.name == "QuestList" || gameObject.name == "Scroll") //we want to be able to click through the quest list.
            {
                gameObject = null;
            }
        }
        

        //if no hits, we then check for objects in the world:
        if (gameObject == null)
        {
            LayerMask interactableMask = LayerMask.GetMask("Interactable");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, interactableMask))
            {
                gameObject = hit.collider.gameObject;
            }
        }

        //if no hits, we then check for ground:
        if (gameObject == null)
        {
            LayerMask interactableMask = LayerMask.GetMask("Ground");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, interactableMask))
            {
                gameObject = hit.collider.gameObject;

            }
        }

        return gameObject;
    }

}
