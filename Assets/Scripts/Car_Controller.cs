using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    [SerializeField] private GameObject[] wheels;
    private float wheelDiameter, speed;
    [SerializeField] private float kph;

    void Start()
    {
        wheelDiameter = wheels[0].GetComponent<MeshRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        speed = kph / (3.6f * 50f);
        transform.position += new Vector3(speed, 0f, 0f);
        foreach (GameObject wheel in wheels)
        {
            wheel.transform.rotation *= Quaternion.Euler(360f * speed / (Mathf.PI * wheelDiameter), 0f, 0f);
        }
    }

}
