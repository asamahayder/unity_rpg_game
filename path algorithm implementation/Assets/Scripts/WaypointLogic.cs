using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointLogic : MonoBehaviour
{

    public GameObject target;
    public float distanceToDestroy = 3;
    public LayerMask mask;
    public float collisionCheckRadius = 3f;

    // Update is called once per frame
    void Update()
    {
        /*float dist = Vector3.Distance(target.transform.position, transform.position);
        float distance = (target.transform.position - gameObject.transform.position).magnitude;
        print(dist);
        if (dist <= distanceToDestroy) Destroy(gameObject);*/

        if (Physics.CheckSphere(transform.position, collisionCheckRadius, mask)) Destroy(gameObject);
        
    }
}
