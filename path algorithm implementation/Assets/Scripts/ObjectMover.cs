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

    private int terrainNumber = 0;

    private void Awake()
    {
        modelTransform = model.transform;
        animator = model.GetComponent<Animator>();
    }

    private void Start()
    {
        Vector3 position = gameObject.transform.position;

        terrainNumber = GetClosestCurrentTerrain(position);

        float terrainY = Terrain.activeTerrains[terrainNumber].SampleHeight(gameObject.transform.position);
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

            float terrainY = Terrain.activeTerrains[terrainNumber].SampleHeight(currentPath[i].worldPosition);

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

    //THE FOLLOWING CODE IS TAKEN FROM https://stackoverflow.com/questions/52345522/unity-get-the-actual-current-terrain
    private int GetClosestCurrentTerrain(Vector3 playerPos)
    {
        Terrain[] _terrains = Terrain.activeTerrains;
        //Get the closest one to the player
        var center = new Vector3(_terrains[0].transform.position.x + _terrains[0].terrainData.size.x / 2, playerPos.y, _terrains[0].transform.position.z + _terrains[0].terrainData.size.z / 2);
        float lowDist = (center - playerPos).sqrMagnitude;
        var terrainIndex = 0;

        for (int i = 0; i < _terrains.Length; i++)
        {
            center = new Vector3(_terrains[i].transform.position.x + _terrains[i].terrainData.size.x / 2, playerPos.y, _terrains[i].transform.position.z + _terrains[i].terrainData.size.z / 2);

            //Find the distance and check if it is lower than the last one then store it
            var dist = (center - playerPos).sqrMagnitude;
            if (dist < lowDist)
            {
                lowDist = dist;
                terrainIndex = i;
            }
        }
        return terrainIndex;
    }
    //BORROWED CODE ENDS HERE


}
