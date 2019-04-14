using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ConfrontationEffects : MonoBehaviour
{
    ParticleSystem particles;

    int intensity = 1;
    float runTime = 0f;
    float runLength = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rumble(int _intensity = 1) {
        Debug.Log("RUMBLE RUMBLE");
        intensity = _intensity;
        StartCoroutine("DoRumble");
    }

    IEnumerator DoRumble() {
        particles.Play();

        Debug.Log("AAAAAAAAAG");

        while (runTime < runLength) {
            runTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("DONEA");

        runTime = 0f;
        particles.Stop();
    }
}
