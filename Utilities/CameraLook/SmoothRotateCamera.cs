using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmoothRotateCamera : MonoBehaviour
{

    private float mouseX;
    private float mouseY;
    public float rotateSpeedX;
    public float rotateSpeedY;

    public float minDistance;
    public float maxDistance;

    public float minRotateYClampValue;
    public float maxRotateYClampValue;

    public float lerpSpeed;
    public float distanceScaleTargetValue;
    private float distanceScaleCurrentValue;
    private float startDistanceScaleValue;
    private Vector3 startEulerAngles;
    public Transform targetTrans;
    // Start is called before the first frame update
    private void Start()
    {
        startEulerAngles = transform.eulerAngles;
        startDistanceScaleValue = distanceScaleTargetValue;
        ResetCameraState();
    }

    // Update is called once per frame
    void Update()
    {/*
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }*/
        if (Input.GetMouseButton(0))
        {
            mouseX += Input.GetAxis("Mouse X") * rotateSpeedX;
            mouseY -= Input.GetAxis("Mouse Y") * rotateSpeedY;
            mouseY = Mathf.Clamp(mouseY, minRotateYClampValue, maxRotateYClampValue);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            distanceScaleTargetValue -= Input.GetAxis("Mouse ScrollWheel");
            distanceScaleTargetValue = Mathf.Clamp(distanceScaleTargetValue, 0, 1);
        }

    }

    private void LateUpdate()
    {
        Quaternion targetRotation = Quaternion.Euler(mouseY, mouseX, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * lerpSpeed);
        Vector3 targetPosition = transform.rotation * new Vector3(0, 0, -GetCurrentPos()) + targetTrans.position;
        transform.position = targetPosition;
    }
    private float GetCurrentPos()
    {
        distanceScaleCurrentValue = Mathf.Lerp(distanceScaleCurrentValue, distanceScaleTargetValue, Time.deltaTime * lerpSpeed);
        return minDistance + (maxDistance - minDistance) * distanceScaleCurrentValue;
    }

    public void ResetCameraState()
    {
        distanceScaleCurrentValue = distanceScaleTargetValue = startDistanceScaleValue;
        mouseX = startEulerAngles.y;
        mouseY = startEulerAngles.x;
    }

}
