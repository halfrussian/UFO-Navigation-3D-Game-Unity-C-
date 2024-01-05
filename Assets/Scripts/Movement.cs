using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

   
    [SerializeField] float mainRocketThrust  = 100f; 
    [SerializeField] float rotationTrust  = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainParticles;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;


    Rigidbody ufoRigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        ufoRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationTrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
           ApplyRotation(-rotationTrust);
        }
    }


    void StartThrusting()
    {
        ufoRigidBody.AddRelativeForce(Vector3.up * mainRocketThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainParticles.isPlaying)
        {
            mainParticles.Play();
        }
    }

      void StopThrusting()
    {
        audioSource.Stop();
        mainParticles.Stop();
    }



     void ApplyRotation(float rotationThisFrame)
    {
        //freezoing rotation so we can maunally rotate
        ufoRigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        //unfreezoing rotation so we let physics system take over
         ufoRigidBody.freezeRotation = false;
    }
}
