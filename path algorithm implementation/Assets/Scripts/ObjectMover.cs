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

    

    private void Awake()
    {
        
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
                    i++;
                }

                

            }
            Vector3 targetPosition = currentPath[i].worldPosition;
            targetPosition.y = Terrain.activeTerrain.SampleHeight(currentPath[i].worldPosition);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, Time.deltaTime * speed);

        }


    }

    public void updatePath(List<Node> newPath)
    {
        currentPath = newPath;
        i = 0;
    }

    
}
