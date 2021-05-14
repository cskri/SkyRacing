using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.CrossPlatformInput;
using System.Diagnostics;

public class BoostZone : MonoBehaviour
{
    public GameObject car;
    private CarController carController;
   

    void Start()
    {
        carController = car.GetComponent<CarController>();
    }

    void OnTriggerEnter()
    {
        float increaseSpeed = carController.CurrentSpeed + 1f;
        car.GetComponent<Rigidbody>().velocity = Vector3.back * increaseSpeed;  
        UnityEngine.Debug.Log("Boost Triggered");
    }
    
}
