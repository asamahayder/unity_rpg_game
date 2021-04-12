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
            print(Vector3.Distance(currentPath[i].worldPosition, gameObject.transform.position));
            if (Vector3.Distance(currentPath[i].worldPosition, gameObject.transform.position) <= radius)
            {
                
                if (i != currentPath.Count - 1)
                {
                    i++;
                }

                

            }
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, currentPath[i].worldPosition, Time.deltaTime * speed);

        }


    }

    public void updatePath(List<Node> newPath)
    {
        currentPath = newPath;
        i = 0;
    }

    
}
