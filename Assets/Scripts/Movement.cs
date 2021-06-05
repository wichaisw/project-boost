using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;
    [SerializeField] public AudioClip rocketSound;
    Quaternion rotationConstraints;


    // CACHE
    Rigidbody rb;
    public AudioSource audioSource;
    // public AudioSource[] sounds;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        // rocketSound = sounds[0];
        // gameOverSound = sounds[1];
        // stageClearSound = sounds[2];        
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
            // if(!rocketSound.isPlaying)
            // {
            //     rocketSound.Play();
            //     rocketSound.volume = 1f;
            // }
            if(!audioSource.isPlaying) {
                audioSource.PlayOneShot(rocketSound);
            }
        } else {
            audioSource.Stop();
            // rocketSound.volume = 0f;
            // rocketSound.Stop();
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
        rb.freezeRotation = true; // disable third-party physics
        transform.Rotate(Vector3.forward  * Time.deltaTime * rotateThisFrame);
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ; 
    }

}
