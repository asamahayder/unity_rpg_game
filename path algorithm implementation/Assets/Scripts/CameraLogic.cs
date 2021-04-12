using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{

    //private float zoomSpeed = 2f;
    private float zoomAmount = 0f;
    public Transform target;
    public float startX;
    public float startY;
    public float startZ;

    private Vector3 previousPosition;
    [SerializeField] private Camera cam;
    [SerializeField] private float distanceToTarget = 10;

    public bool lookAtPlayer = false;
    public bool rotateAroundPlayer = false;
    public float rotationSpeed = 5.0f;
    private Vector3 _cameraOffset;
    public float smoothFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        //centering player in view creating the isometric feel


        //zooming
        /*if (Input.mouseScrollDelta[1] == -1 && transform.localPosition.x != 10f)
        {
            transform.localPosition = transform.localPosition + new Vector3(1, 1, -1) * zoomSpeed;
        }
        else if (Input.mouseScrollDelta[1] == 1 && transform.localPosition.x != 1f)
        {
            transform.localPosition = transform.localPosition + new Vector3(-1, -1, 1) * zoomSpeed;
        }*/


        if (Input.mouseScrollDelta[1] == -1 )
        {
            zoomAmount += 0.1f;
            transform.localPosition = new Vector3(startX, startY, startZ) * (1+zoomAmount);
        }
        else if (Input.mouseScrollDelta[1] == 1)
        {
            zoomAmount -= 0.1f;
            transform.localPosition = new Vector3(startX, startY, startZ) * (1 + zoomAmount);
        }

        zoomAmount = Mathf.Clamp(zoomAmount, 0f, 1f);
        print(zoomAmount);


        //Rotation
        /*if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);

        }
        if (Input.GetMouseButton(1))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally


            cam.transform.position = target.position;

           
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

            cam.transform.Translate(new Vector3(0, 10, -distanceToTarget));
            //transform.LookAt(target);

            previousPosition = newPosition;


        }*/

        if (Input.GetMouseButtonDown(1)) rotateAroundPlayer = true;
        if (Input.GetMouseButtonUp(1)) rotateAroundPlayer = false;
        

    }

    private void LateUpdate()
    {
        if (rotateAroundPlayer)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            _cameraOffset = camTurnAngle * _cameraOffset;

        }

        Vector3 newPos = target.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        transform.LookAt(target);

    }
}
