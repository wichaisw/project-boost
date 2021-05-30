using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;

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
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        }
    }
    void ProcessRotation() 
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);

        }
        else if(Input.GetKey(KeyCode.D))
        {   
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotateThisFrame)
    {
    transform.Rotate(Vector3.forward  * Time.deltaTime * rotateThisFrame);
    }

}