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
        float terrainY = Terrain.activeTerrain.SampleHeight(gameObject.transform.position);
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
            Quaternion rotation = Quaternion.LookRotation(deltaVec);
            rotation.x = 0;
            rotation.z = 0;

            modelTransform.rotation = rotation;
            //gameObject.transform.Find("Model").rotation = rotation;
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

            float terrainY = Terrain.activeTerrain.SampleHeight(currentPath[i].worldPosition);

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

    

    
}
