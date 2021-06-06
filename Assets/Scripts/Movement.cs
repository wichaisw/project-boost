using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;
    [SerializeField] public AudioClip rocketSound;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    Quaternion rotationConstraints;


    // CACHE
    Rigidbody rb;
    public AudioSource audioSource;    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(rocketSound);
            }

            if(!mainBoosterParticles.isPlaying)
            {
                mainBoosterParticles.Play();
            }
        } else {
            audioSource.Stop();
            mainBoosterParticles.Stop();
        }
    }
    void ProcessRotation() 
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if(!rightThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Play();
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {   
            ApplyRotation(-rotationThrust);
            if(!leftThrusterParticles.isPlaying)
            {
                leftThrusterParticles.Play();
            }
        } 
        else 
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
        }
    }

    private void ApplyRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true; // disable third-party physics
        transform.Rotate(Vector3.forward  * Time.deltaTime * rotateThisFrame);
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ; 
    }

}
