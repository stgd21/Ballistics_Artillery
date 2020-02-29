using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public float myTimeScale = 1.0f;
    public GameObject target;
    public float launchForce = 10f;
    public LookAtTarget cameraControl;
    public AudioSource fireAudio;
    public AudioSource hitAudio;
    public AudioSource windAudio;
    public Canvas EndGameCanvas;

    Rigidbody rb;
    bool playedOnce = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        Time.timeScale = myTimeScale; // allow for slowing time to see what's happening
        rb = GetComponent<Rigidbody>();

        //Disable animation convenience settings on rigidbody
        rb.useGravity = true;
        rb.isKinematic = false;

        FiringSolution fs = new FiringSolution();
        Nullable<Vector3> aimVector = fs.Calculate(transform.position, target.transform.position, launchForce, Physics.gravity);
        if (aimVector.HasValue)
        {
            rb.AddForce(aimVector.Value.normalized * launchForce, ForceMode.VelocityChange);
        }
        fireAudio.Play();
        Invoke("ActivateCamera", 0.2f);
        
    }

    void ActivateCamera()
    {
        cameraControl.enabled = true;
        windAudio.Play();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 50 && hitAudio.isPlaying == false && playedOnce == false)
        {
            hitAudio.Play();
            playedOnce = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision");
        EndGameCanvas.gameObject.SetActive(true);
    }
}