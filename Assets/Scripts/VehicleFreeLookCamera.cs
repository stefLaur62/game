using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleFreeLookCamera : MonoBehaviour
{
    public Vector2 rotationRange = new Vector2(70, 361);
    public float rotationSpeed = 10;
    //private VehicleMainCamera vehicleMainCamera;
    private Transform targetTransform;
    private Vector3 targetAngles;
    private Quaternion capturedRotation;
    private bool isInFreeLook;
    private float inputH;
    private float inputV;


    void Start()
    {
        SetInitialReferences();
/*        vehicleMainCamera.EventAssignCameraTarget += AssignTarget;
*/    }
    void OnDisable()
    {
/*        vehicleMainCamera.EventAssignCameraTarget -= AssignTarget;
*/    }
    void Update()
    {
        FreeLookRotation();
    }
    void FixedUpdate()
    {
        FollowTargerRotation();
    }
    void SetInitialReferences()
    {
//        vehicleMainCamera = GetComponent<VehicleMainCamera>();
    }
    void AssignTarget(Transform targ)
    {
        targetTransform = targ;
    }
    void FreeLookRotation()
    {
        if (Input.GetMouseButton(0) && Time.timeScale > 0)
        {
            if (targetTransform == null)
                return;
            isInFreeLook = true;
            transform.rotation = capturedRotation;

            inputH = Input.GetAxis("Mouse X");
            inputV = Input.GetAxis("Mouse Y");
            if (targetAngles.y > 180)
                targetAngles.y -= 360;
            if (targetAngles.x > 180)
                targetAngles.x -= 360;
            if (targetAngles.y < -180)
                targetAngles.y += 360;
            if (targetAngles.x < -180)
                targetAngles.x += 360;

            targetAngles.y += inputH * rotationSpeed;
            targetAngles.x += inputV * rotationSpeed;

            targetAngles.y = Mathf.Clamp(targetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f);
            targetAngles.x = Mathf.Clamp(targetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f);
        } 
        else
        {
            isInFreeLook = false;
        }
    }

    void FollowTargerRotation()
    {
        if (targetTransform != null && !isInFreeLook) {
            capturedRotation = Quaternion.LookRotation(targetTransform.forward, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, capturedRotation, Time.deltaTime * 5);
            targetAngles = capturedRotation.eulerAngles;
        }
    }
}
