using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float speed = 2f;
    private const float maxSize = 4;
    private const float minSize = 2f;
    private new Camera camera;
    private float input;
    private bool moveingCamera = false;
    private Vector3 mouseStartPosition;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }
    private void FixedUpdate()
    {
        Vector3 direction;
        if (Input.GetMouseButtonDown(2))
        {
            mouseStartPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            moveingCamera = true;
        }
        if (Input.GetMouseButton(2) && moveingCamera)
        {
            direction = mouseStartPosition - camera.ScreenToWorldPoint(Input.mousePosition);
            camera.transform.position += direction;
        }
        if(Input.GetMouseButtonUp(2))
        {
            moveingCamera = false;
        }
        input = Input.GetAxisRaw("Mouse ScrollWheel");
        if (input != 0)
        {
            camera.orthographicSize = ((camera.orthographicSize > minSize && input > 0) || (camera.orthographicSize < maxSize && input < 0)) ? camera.orthographicSize - (speed * input) : camera.orthographicSize;
        }
        

    }
}
