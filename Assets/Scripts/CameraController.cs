using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target; // Target to move towards
    public Vector3 offset; // Offset form target
    public float defaultFov = 30; // Default field of view
    public float rotateSpeed = 5f; // Speed of rotation
    public float zoomSpeed = 0.5f; // Speed of zooming

    void Start() {
        ReturnToDefalutView();
    }

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider != null) {
                    target = hit.transform; // Set the target to the clicked object
                }
            }
        }

        // Return to default view
        if (Input.GetKeyDown("q") && target != null) {
            target = null;
            ReturnToDefalutView();
        }

        // Move camera towards target
        if (target != null) {
            if (Input.GetMouseButton(2)) { // Middle mouse button for rotation
                float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
                float vertical = -Input.GetAxis("Mouse Y") * rotateSpeed;

                transform.RotateAround(target.position, Vector3.up, horizontal);
                transform.RotateAround(target.position, transform.right, vertical);        
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f) { // Zoom in view on scroll up
                Camera.main.fieldOfView -= zoomSpeed;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) { // Zoom out view on scroll down
                Camera.main.fieldOfView += zoomSpeed;
            }
            
            offset = transform.position - target.position;
            transform.position = target.position + offset;
            transform.LookAt(target);
        }

    }

    void ReturnToDefalutView() {
        target = GameObject.Find("Sun").transform;
    }
}