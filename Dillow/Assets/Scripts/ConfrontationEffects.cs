using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class ConfrontationEffects : MonoBehaviour
{
    public ParticleSystem particles;

    int intensity = 1;
    float runTime = 0f;
    float runLength = 0.5f;
    CinemachineImpulseSource impulse;

    // Start is called before the first frame update
    void Start()
    {
        //particles = GetComponent<ParticleSystem>();
        impulse = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rumble(int _intensity = 1) {
        intensity = _intensity;
        StartCoroutine("DoRumble");
    }

    IEnumerator DoRumble() {
        particles.Play();

        impulse.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = runLength*3f;
        impulse.GenerateImpulse();

        while (runTime < runLength) {
            runTime += Time.deltaTime;
            yield return null;
        }

        runTime = 0f;
        particles.Stop();
    }
}
