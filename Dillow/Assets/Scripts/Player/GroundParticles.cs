using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JumpDetector), typeof(ParticleSystem))]
public class GroundParticles : MonoBehaviour
{

    JumpDetector jump_detector;
    ParticleSystem particles;
    ParticleSystem.EmissionModule em;

    Rigidbody rb;
    AudioSource aud;

    float rate = 1,
          pitch = 3f,
          amp_range = 0.02f,
          amp_damp = 0.1f,
          sound_threshold = 0.3f,
          max_speed;

    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        jump_detector = GetComponent<JumpDetector>();
        particles = GetComponent<ParticleSystem>();

        em = particles.emission;

        jump_detector.GroundExitEvent += delegate { em.rateOverDistance = 0; grounded = false; };
        jump_detector.CanJumpEvent += delegate { em.rateOverDistance = rate; grounded = true; };

        /*
        BallBody b = transform.parent.GetComponentInChildren<BallBody>();
        aud = GetComponent<AudioSource>();
        while (!b.ready)
        {
            yield return null;
        }
        rb = b.rb;
        */
    }

    /*
    private void Update()
    {
        if (aud && rb)
        {
            print("we're trying: " + rb.angularVelocity);
            if (rb.angularVelocity.magnitude > sound_threshold && grounded) {
                aud.volume = rb.angularVelocity.magnitude / rb.maxAngularVelocity;
                aud.volume -= amp_damp;
                if (!aud.isPlaying)
                    Play();
            }
            else
            {
                aud.volume = 0;
            }
        }
    }

    private void Play()
    {
        aud.pitch = pitch;
        aud.volume += Random.Range(-amp_range, 0);
        aud.Play();
    }
    */
    //int terrainLayer = TerrainSurface.GetMainTexture(transform.position);
    //Use this in an OnCollisionStay or something to determine which particle effect to use
}
