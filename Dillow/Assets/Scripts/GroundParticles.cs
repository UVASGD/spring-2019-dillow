using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundParticles : MonoBehaviour
{

    JumpDetector jump_detector;
    ParticleSystem particles;
    ParticleSystem.EmissionModule em;

    float rate = 1;

    // Start is called before the first frame update
    void Start()
    {
        jump_detector = GetComponent<JumpDetector>();
        particles = GetComponent<ParticleSystem>();

        em = particles.emission;

        jump_detector.GroundExitEvent += delegate { em.rateOverDistance = 0; };
        jump_detector.CanJumpEvent += delegate { em.rateOverDistance = rate; };
    }
}
