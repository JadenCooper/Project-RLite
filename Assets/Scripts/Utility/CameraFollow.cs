using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;
    public Vector2 MinMaxZoom = new Vector2(8, 11);
    public float ZoomIncrement = 1f;
    public Camera cam;
    [SerializeField]
    private InputActionReference zoom;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    void FixedUpdate()
    {
        float ScrollValue = zoom.action.ReadValue<Vector2>().y;
        if (ScrollValue != 0)
        {
            Zoom(ScrollValue);
        }
        Vector3 desiredPosition = target.position + target.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
        transform.rotation = smoothedrotation;
    }

    public void Zoom(float ScrollValue)
    {
        if (ScrollValue > 0)
        {
            // Zoom In
            cam.orthographicSize -= ZoomIncrement;
        }
        else
        {
            // Zoom Out
            cam.orthographicSize += ZoomIncrement;
        }
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, MinMaxZoom.x, MinMaxZoom.y);
    }
}
