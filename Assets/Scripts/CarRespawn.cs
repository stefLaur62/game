using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRespawn : MonoBehaviour
{
    public Transform carTransform;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            carTransform.position = originalPosition;
            carTransform.rotation = originalRotation;
        }
    }
}
