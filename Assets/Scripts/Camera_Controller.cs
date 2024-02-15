using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private Transform target;
    private Vector3 offset;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed;
            Quaternion rotation = Quaternion.Euler(0, horizontalInput, 0);
            offset = rotation * offset;
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
