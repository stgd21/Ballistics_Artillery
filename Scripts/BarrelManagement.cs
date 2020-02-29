using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelManagement : MonoBehaviour
{
    public AudioSource stopAudio;
    public AudioSource movingAudio;
    public Projectile projectile;
    bool playedOnce = false;
    bool allowFiring = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingAudio.isPlaying == false && stopAudio.isPlaying == false && playedOnce == false)
        {
            stopAudio.Play();
            playedOnce = true;
            Invoke("AllowFiring", 1f);
        }

        if (allowFiring == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                projectile.enabled = true;
            }
        }
    }

    void AllowFiring()
    {
        allowFiring = true;
    }
}
