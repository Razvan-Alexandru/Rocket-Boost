using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;

    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticeles;
    [SerializeField] ParticleSystem rightSideEngineParticeles;
    [SerializeField] ParticleSystem leftSideEngineParticeles;


    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate(); 
    }

    void ProcessThrust () {
        if (Input.GetKey(KeyCode.Space)) {
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainEngine);
            }

            if (!mainEngineParticeles.isPlaying) {
                mainEngineParticeles.Play();
            }

            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            
        } else {
            audioSource.Stop();
            mainEngineParticeles.Stop();
        }
    }

    void ProcessRotate () {
        rb.freezeRotation = true;

        if (Input.GetKey(KeyCode.A)) {
            if (!rightSideEngineParticeles.isPlaying) {
                rightSideEngineParticeles.Play();
            }

            transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
            
        } else if (Input.GetKey(KeyCode.D)) {
            if (!leftSideEngineParticeles.isPlaying) {
                leftSideEngineParticeles.Play();
            }

            transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);
            
        } else {
            rightSideEngineParticeles.Stop();
            leftSideEngineParticeles.Stop();
        }

        rb.freezeRotation = false;
    }
}
