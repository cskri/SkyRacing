using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public System.Single speed;
    void Update()
    {
        transform.Rotate(new Vector3(0f,speed,0f) * Time.deltaTime);
    }
}
