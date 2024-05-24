using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_CameraController : MonoBehaviour
{
    public Camera cam;
    public float zoomSpeed = 1.0f;
    public float zoomDamping = 5.0f;
    public float rotateSpeed = 1.0f;
    public float rotateDamping = 5.0f;
    public float minXRotation = 10.0f;
    public float maxXRotation = 60.0f;
    public float moveSpeed = 10.0f;
    public float edgeDistance = 10.0f;
    public Vector3 centerPoint = Vector3.zero;
    public float maxDistance = 50.0f;

    private Vector3 currentRotation;
    private float currentZoom;

    void Start()
    {
        //currentRotation = transform.eulerAngles;
        currentZoom = cam.transform.localPosition.z;
        currentRotation = new Vector3(45,0,0);
    }

    void Update()
    {
        // Zoom in/out with middle mouse button
        float zoomAmount = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom += zoomAmount;
        currentZoom = Mathf.Clamp(currentZoom, -20, 0); // Clamp zoom between a certain range
        float zoomLerp = Mathf.Lerp(cam.transform.localPosition.z, currentZoom, Time.deltaTime * zoomDamping);
        cam.transform.localPosition = new Vector3(0, 0, zoomLerp);

        // Rotate left/right and up/down with right mouse button
        if (Input.GetMouseButton(1))
        {
            float rotateHorizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            float rotateVertical = Input.GetAxis("Mouse Y") * rotateSpeed;

            currentRotation.y += rotateHorizontal;
            currentRotation.x -= rotateVertical;
            currentRotation.x = Mathf.Clamp(currentRotation.x, minXRotation, maxXRotation); // Clamp the x rotation

            Quaternion newRotation = Quaternion.Euler(currentRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotateDamping);
        }
        else
        {
            // Move object with mouse at screen edges
            Vector3 moveDirection = Vector3.zero;
            if (Input.mousePosition.x >= Screen.width - edgeDistance)
            {
                moveDirection += Vector3.right;
            }
            else if (Input.mousePosition.x <= edgeDistance)
            {
                moveDirection += Vector3.left;
            }
            if (Input.mousePosition.y >= Screen.height - edgeDistance)
            {
                moveDirection += Vector3.forward;
            }
            else if (Input.mousePosition.y <= edgeDistance)
            {
                moveDirection += Vector3.back;
            }

            // Move object with W, A, S, D and arrow keys
            moveDirection += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Transform move direction to be relative to camera's orientation
            moveDirection = cam.transform.TransformDirection(moveDirection);
            moveDirection.y = 0; // Keep movement strictly horizontal

            // Calculate new position and check if within max distance
            Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
            if (Vector3.Distance(centerPoint, newPosition) <= maxDistance)
            {
                transform.position = newPosition;
            }
        }

        cam.transform.LookAt(this.gameObject.transform);
    }
}
