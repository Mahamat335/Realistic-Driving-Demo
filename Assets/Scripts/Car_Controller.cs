using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    [SerializeField] private GameObject[] wheels;
    [SerializeField] private Transform needle;
    private float wheelDiameter, speed, rpm;
    private float[] gearRatios = { 3.587f, 2.022f, 1.384f, 1.0f, 0.861f };
    private float[] maxSpeeds = { 60.98849f, 108.1901f, 158.0608f, 218.753f, 254.1059f };
    private int currentGear = 1, maxGear = 5;
    [SerializeField] private float kph = 0;
    [SerializeField] private TMP_Text speedoMeterText;
    [SerializeField] private TMP_Text gearText;
    private float minNeedleRotation = 0, maxNeedleRotation = -240;
    [SerializeField] private float horsePower;
    [SerializeField] private float maxRPM;
    [SerializeField] private float minRPM;
    [SerializeField] private float rpmForLowerGear;
    [SerializeField] private float rpmForHigherGear;
    [SerializeField] private float newRpmForNewGear;
    private const float gearConstant = 1.435f;
    void Start()
    {
        wheelDiameter = wheels[0].GetComponent<MeshRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        HandleInput();
        CalculateRPM();
        CalculateSpeed();
        Move();
    }

    private void LateUpdate()
    {
        speedoMeterText.text = ((int)kph).ToString();
        gearText.text = currentGear.ToString();
        needle.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(minNeedleRotation, maxNeedleRotation, rpm / maxRPM));
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
    void HandleInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rpm += Time.fixedDeltaTime * 4000f;
        }
        else
        {
            rpm -= Time.fixedDeltaTime * 500;
            if (rpm > rpmForHigherGear)
                rpm -= 1500;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rpm -= Time.fixedDeltaTime * 2000f;
        }

        if (Input.GetKey(KeyCode.R) && kph == 0)
        {

        }

        rpm = Mathf.Clamp(rpm, minRPM, maxRPM);
    }

    void CalculateRPM()
    {
        if (rpm >= rpmForHigherGear && currentGear < maxGear)
        {
            currentGear++;
            rpm = kph * rpmForHigherGear / maxSpeeds[currentGear - 1]; Debug.Log(rpm);
        }
        else if (currentGear > 1 && rpm <= rpmForLowerGear && kph < maxSpeeds[currentGear - 2])
        {
            currentGear--;
            rpm = kph * rpmForHigherGear / maxSpeeds[currentGear - 1];

        }
    }

    void CalculateSpeed()
    {
        float enginePower = rpm / rpmForHigherGear * horsePower;
        float wheelTorque = enginePower / (gearRatios[currentGear - 1] * gearConstant);
        float speedMS = wheelTorque * wheelDiameter;
        kph = speedMS * 3.6f;
        if (kph >= maxSpeeds[currentGear - 1])
            kph = maxSpeeds[currentGear - 1];
    }



}
