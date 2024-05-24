using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public float normalSpeed;
    public float fastSpeed;
    public float movmentSpeed;
    public float movmentTime;
    public float rotationAmount;
    public Vector3 zoomAmount;

    public Vector3 newPos;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public float rotationSpeed = 10f; // Rotation speed for right-click drag

    // Start is called before the first frame update
    void Start()
    {
        newPos = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovmentInput();
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }

        if (Input.GetMouseButton(1)) // Right mouse button is held down
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            newRotation *= Quaternion.Euler(Vector3.up * mouseX * rotationSpeed);
            newRotation *= Quaternion.Euler(Vector3.right * -mouseY * rotationSpeed);
            
            // Lock the Z rotation
            Vector3 eulerAngles = newRotation.eulerAngles;
            newRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0);
        }
    }
    void HandleMovmentInput()
    {

        movmentSpeed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
      
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPos += (transform.forward * movmentSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPos += (transform.forward * -movmentSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPos += (transform.right * movmentSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPos += (transform.right * -movmentSpeed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }


        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * movmentTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movmentTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movmentTime);
    }
}
