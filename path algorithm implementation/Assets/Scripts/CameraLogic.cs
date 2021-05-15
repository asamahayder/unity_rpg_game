using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public Transform target;

    private bool rotateAroundPlayer = false;
    public float rotationSpeed = 5.0f;
    private Vector3 _cameraOffset;
    public float smoothFactor = 0.5f;

    private float currentZoomLevel;
    public float startingZoom = 2f;
    public float nearZoomLimit = 1f;
    public float farZoomLimit = 2f;
    public float delta = .1f;

    public float startX;
    public float startY;
    public float startZ;


    void Start()
    {
        transform.position = new Vector3(startX, startY, startZ);
        _cameraOffset = transform.position - target.position;
        currentZoomLevel = startingZoom;
    }


    public void ZoomIn()
    {
        if (currentZoomLevel <= nearZoomLimit) return;
        currentZoomLevel = Mathf.Max(currentZoomLevel - delta, nearZoomLimit);
      
    }


    public void ZoomOut()
    {
        if (currentZoomLevel >= farZoomLimit) return;
        currentZoomLevel = Mathf.Min(currentZoomLevel + delta, farZoomLimit);
       
    }


    void Update()
    {

        if (Input.mouseScrollDelta[1] == -1) ZoomOut();//Scroll down
        else if (Input.mouseScrollDelta[1] == 1) ZoomIn(); //Scroll up
        
        if (Input.GetMouseButtonDown(2)) rotateAroundPlayer = true;
        if (Input.GetMouseButtonUp(2)) rotateAroundPlayer = false;
        
    }

    private void LateUpdate()
    {

        if (rotateAroundPlayer)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            _cameraOffset = camTurnAngle * _cameraOffset;

        }

        Vector3 newPos = target.position + _cameraOffset * currentZoomLevel;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        //Handling camera look-at player logic. Also adding some height so camera looks at head instead of feet.
        transform.LookAt(target.transform.Find("Camera Lookat"));
    }


}
