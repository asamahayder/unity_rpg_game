using UnityEngine;

public class WaypointLogic : MonoBehaviour
{
    public float distanceToDestroy = 3;
    public LayerMask mask;
    public float collisionCheckRadius = 3f;

    void Update()
    {
        if (Physics.CheckSphere(transform.position, collisionCheckRadius, mask)) Destroy(gameObject);
    }
}
