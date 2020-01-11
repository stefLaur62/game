using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCamera : MonoBehaviour
{
    public Transform car;
    public float distance;
    public float height;
    public float rotationDamping;
    public float heightDamping;
    public float zoomRatio;
    public float defaultFOV;

    public float rotation_vector;

    void FixedUpdate()
    {
        Vector3 local_velocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);
        if (local_velocity.z < -0.5)
        {
            rotation_vector = car.eulerAngles.y + 180;
        }
        else
        {
            rotation_vector = car.eulerAngles.y;
        }
        float acceleration = car.GetComponent<Rigidbody>().velocity.magnitude;
        Camera.main.fieldOfView = defaultFOV + acceleration + zoomRatio + Time.deltaTime*50;
    }
    private void LateUpdate()
    {
        float wantedAngle = rotation_vector;
        float wantedHeight = car.position.y + height;
        float myAngle = transform.eulerAngles.y;
        float myHeight = transform.position.y;

        myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime*50);
        myHeight = Mathf.LerpAngle(myHeight, wantedHeight, heightDamping * Time.deltaTime*50);

        Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);

        transform.position = car.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        Vector3 temp = transform.position;
        temp.y = myHeight;
        transform.position = temp;

        transform.LookAt(car);
    }
}
