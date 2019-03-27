using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundParticles : MonoBehaviour
{

    JumpDetector jump_detector;
    ParticleSystem particles;
    ParticleSystem.EmissionModule em;
    Rigidbody rb;
    AudioSource aud;

    float rate = 1,
          pitch_range = 0.4f,
          amp_range = 0.4f,
          sound_threshold = 0.3f,
          max_speed;

    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.parent.GetComponentInChildren<Rigidbody>();
        jump_detector = GetComponent<JumpDetector>();
        particles = GetComponent<ParticleSystem>();
        aud = GetComponent<AudioSource>();

        em = particles.emission;

        jump_detector.GroundExitEvent += delegate { em.rateOverDistance = 0; grounded = false; };
        jump_detector.CanJumpEvent += delegate { em.rateOverDistance = rate; grounded = true; };
    }

    private void Update()
    {
        if (aud)
        {         
            if (rb.angularVelocity.magnitude > sound_threshold) {
                aud.volume = rb.angularVelocity.magnitude / rb.maxAngularVelocity;
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
        aud.pitch += Random.Range(-pitch_range, pitch_range);
        aud.volume += Random.Range(-amp_range, 0);
        aud.Play();
    }

    //int terrainLayer = TerrainSurface.GetMainTexture(transform.position);
    //Use this in an OnCollisionStay or something to determine which particle effect to use
}
