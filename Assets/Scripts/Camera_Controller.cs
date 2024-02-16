using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    private float rotationSpeed = 5f;
    [SerializeField] private Transform carCamera;
    private Transform target;
    private Vector3 offset;
    private bool carCameraBool;

    void Start()
    {
        carCameraBool = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            carCameraBool = !carCameraBool;
        }
    }

    void LateUpdate()
    {
        if (target != null && !carCameraBool)
        {
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed;
            Quaternion rotation = Quaternion.Euler(0, horizontalInput, 0);
            offset = rotation * offset;
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
        else if (target != null && carCameraBool)
        {
            transform.position = carCamera.position;
            transform.rotation = carCamera.rotation;
        }
        else
        {
            Debug.Log("Camera couldn't find the player.");
        }
    }
}
