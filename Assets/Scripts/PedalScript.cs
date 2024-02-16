using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PedalScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool gasPedal;
    private Car_Controller carController;
    void Start()
    {
        carController = GameObject.FindWithTag("Player").GetComponent<Car_Controller>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gasPedal)
        {
            carController.GasPedalDown();
        }
        else
            carController.BreakPedalDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (gasPedal)
        {
            carController.GasPedalUp();
        }
        else
            carController.BreakPedalUp();
    }


}
