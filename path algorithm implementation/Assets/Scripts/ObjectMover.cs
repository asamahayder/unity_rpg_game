using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    List<Node> currentPath = new List<Node>();
    int i = 0;
    float radius = 1f;
    public float speed;
    
    Vector3 oldPosition;
    Vector3 newPosition;

    [SerializeField] private GameObject model;
    private Transform modelTransform;
    private Animator animator;

    private bool isAtEndOfPath = true;

    private void Awake()
    {
        modelTransform = model.transform;
        animator = model.GetComponent<Animator>();
    }

    private void Start()
    {
        Vector3 position = gameObject.transform.position;
        float terrainY = Terrain.activeTerrains[Terrain.activeTerrains.Length-1].SampleHeight(gameObject.transform.position);
        position.y = terrainY;
        gameObject.transform.position = position;

        oldPosition = gameObject.transform.position;
    }

   
    void Update()
    {

        //Looking for movement by comparing last frame's position with this frame's position.
        //Using movement to control running animation state and rotation of character.
        newPosition = gameObject.transform.position;

        if(newPosition != oldPosition)
        {
            animator.SetBool("isMoving", true);
            //Handling character rotation towards direction
            Vector3 deltaVec = newPosition - oldPosition;
            rotateTowards(deltaVec);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        oldPosition = newPosition;


        if (currentPath != null && currentPath.Count != 0)
        {

            Vector2 startPos = new Vector2(currentPath[i].worldPosition.x, currentPath[i].worldPosition.z);
            Vector2 endPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

            if (Vector2.Distance(startPos, endPos) <= radius)
            {
                
                if (i != currentPath.Count - 1)
                {
                    //next node on path
                    i++;
                }

            }
            
            if(i == currentPath.Count - 1)
            {
                //end of path
                isAtEndOfPath = true;
            }

            float terrainY = Terrain.activeTerrains[Terrain.activeTerrains.Length-1].SampleHeight(currentPath[i].worldPosition);

            Vector3 targetPosition = currentPath[i].worldPosition;
            targetPosition.y = terrainY;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, Time.deltaTime * speed);

        }
    }

    public void updatePath(List<Node> newPath)
    {
        isAtEndOfPath = false;
        currentPath = newPath;
        i = 0;
    }

    public bool getIsAtEndOfPath()
    {
        return isAtEndOfPath;
    }

    public void clearPath()
    {
        isAtEndOfPath = true;
        currentPath = null;
        i = 0;
    }


    /// <summary>
    /// Instantly rotates the object towards a direction. For a smooth rotation, use turnTowardsTarget instead.
    /// </summary>
    /// <param name="direction"></param>
    public void rotateTowards(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotation.x = 0;
        rotation.z = 0;

        modelTransform.rotation = rotation;
    }


    /// <summary>
    /// Smoothly turns the object towards a target transform using the speed paremeter. For instant rotation, use rotateTowards instead.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="speed"></param>
    public void turnTowardsTarget(Transform target, float speed)
    {
        //the following code is taken from: https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html

        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - modelTransform.position;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(modelTransform.forward, targetDirection, singleStep, 0.0f);


        // Calculate a rotation a step closer to the target and applies rotation to this object
        Quaternion lookTowards = Quaternion.LookRotation(newDirection);
        lookTowards.x = 0;
        lookTowards.z = 0;
        modelTransform.rotation = lookTowards;

    }




}
