using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if(Input.GetKey(KeyCode.Space)) 
        {
            // Debug.Log("Pressed SPACE: Thrusting");
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        }
    }
    void ProcessRotation() 
    {
        if(Input.GetKey(KeyCode.A)) 
        {
            Debug.Log("Pressed A: rotate left");
        } else if(Input.GetKey(KeyCode.D))
        {
            Debug.Log("Pressed D: rotate right");
        }
    }
}
