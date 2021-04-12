using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour
{

    public LayerMask groundMask;

    // Update is called once per frame
    void Update()
    {
       if (Input.GetMouseButtonDown(0))
        {
            GetMouseClickWorldPosition();
        }
        
    }

    private Vector3 GetMouseClickWorldPosition()
    {
        Vector3 clickPosition = -1000*Vector3.one;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100f, groundMask))
        {
            clickPosition = hit.point;
        }

        Debug.Log(clickPosition);
        return clickPosition;

    }

}
