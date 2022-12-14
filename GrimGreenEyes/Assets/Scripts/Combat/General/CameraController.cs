using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const int mouseButton = 0;
    private const float speed = 2f;
    private const float maxSize = 4;
    private const float minSize = 2f;
    private new Camera camera;
    private float input;
    private bool moveingCamera = false;
    private Vector3 mouseStartPosition;

    public bool closedBook = true;

    [SerializeField] private float zoomModifierSpeed;
    private float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;
    private Vector2 firstTouchPrevPos, secondTouchPrevPos;
    private void Awake()
    {
        camera = GetComponent<Camera>();
        if (GameObject.Find("GlobalAttributes").GetComponent<GlobalVar>().firstTimeCombat) {
            closedBook = false;
        }
        else
        {
            closedBook = true;
        }
    }
    private void Update()
    {
        if(Input.touchCount > 1)
        {
            return;
        }
        Vector3 direction;
        if (Input.GetMouseButtonDown(mouseButton))
        {
            mouseStartPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            moveingCamera = true;
        }
        if (Input.GetMouseButton(mouseButton) && moveingCamera)
        {
            if (closedBook)
            {
                direction = mouseStartPosition - camera.ScreenToWorldPoint(Input.mousePosition);
                camera.transform.position += direction;
            }
        }
        if(Input.GetMouseButtonUp(mouseButton))
        {
            moveingCamera = false;
        }
        input = Input.GetAxisRaw("Mouse ScrollWheel");
        if (input != 0)
        {
            camera.orthographicSize = ((camera.orthographicSize > minSize && input > 0) || (camera.orthographicSize < maxSize && input < 0)) ? camera.orthographicSize - (speed * input) : camera.orthographicSize;
        }
       
    }
    private void FixedUpdate()
    {
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference)
            {
                camera.orthographicSize = (camera.orthographicSize - zoomModifier > minSize) ? camera.orthographicSize - zoomModifier : camera.orthographicSize;
            }
            if (touchesPrevPosDifference < touchesCurPosDifference)
            {
                camera.orthographicSize = (camera.orthographicSize + zoomModifier < maxSize) ? camera.orthographicSize + zoomModifier : camera.orthographicSize;
            }
        }
    }
}
