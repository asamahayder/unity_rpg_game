using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{

    
    public GameObject gameObject;
    List<Node> currentPath = new List<Node>();
    int i = 0;
    float radius = 1f;
    public float speed;
    Animator animator;
    Vector3 oldPosition;
    Vector3 newPosition;

    

    private void Awake()
    {
        animator = gameObject.transform.Find("Model").GetComponent<Animator>();
        //animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame

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

            gameObject.transform.Find("Model").rotation = rotation;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        oldPosition = newPosition;


        if (currentPath.Count != 0)
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

            float terrainY = Terrain.activeTerrain.SampleHeight(currentPath[i].worldPosition);

            Vector3 targetPosition = currentPath[i].worldPosition;
            targetPosition.y = terrainY;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, Time.deltaTime * speed);

        }
    }

    public void updatePath(List<Node> newPath)
    {
        currentPath = newPath;
        i = 0;
    }

    
}
