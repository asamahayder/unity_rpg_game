using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform lookAt;
    public Vector3 offset;
    private Camera cam;
    
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);
        if(transform.position != pos)
        {
            transform.position = pos;
        }
        
    }
}
