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

    

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        


        if (currentPath.Count != 0)
        {

            Vector2 startPos = new Vector2(currentPath[i].worldPosition.x, currentPath[i].worldPosition.z);
            Vector2 endPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

            if (Vector2.Distance(startPos, endPos) <= radius)
            {
                
                if (i != currentPath.Count - 1)
                {

                    //Handling character rotation towards direction
                    Vector3 deltaVec = currentPath[i].worldPosition - gameObject.transform.position;
                    Quaternion rotation = Quaternion.LookRotation(deltaVec);
                    rotation.x = 0;
                    rotation.z = 0;
                    gameObject.transform.rotation = rotation;

                    //handling changing animation state
                    animator.SetBool("isMoving", true);

                    //next node on path
                    i++;
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }

                

            }

            float terrainY = Terrain.activeTerrain.SampleHeight(currentPath[i].worldPosition);

            //Vector3 directionVector = startPos - endPos;
            //directionVector.y = terrainY;

            Vector3 targetPosition = currentPath[i].worldPosition;
            targetPosition.y = terrainY;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, Time.deltaTime * speed);



            




            /*float rotationX = Mathf.Atan2(Mathf.Sqrt(Mathf.Pow(directionVector.z, 2) + Mathf.Pow(directionVector.y, 2)), directionVector.x);
            print("rotation: " + Mathf.Rad2Deg*rotationX);
            Quaternion rotationVector = gameObject.transform.rotation;
            rotationVector.y = rotationX;
            gameObject.transform.rotation = rotationVector;
            //gameObject.transform.forward = directionVector.normalized;
            //gameObject.transform.rotation = Quaternion.LookRotation(targetPosition.normalized);*/

        }


    }

    public void updatePath(List<Node> newPath)
    {
        currentPath = newPath;
        i = 0;
    }

    
}
