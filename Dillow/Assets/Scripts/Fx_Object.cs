using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx_Object : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        float audioLen = (GetComponent<AudioSource>()) ? GetComponent<AudioSource>().clip.length : 0f;
        float partLen = (GetComponent<ParticleSystem>()) ? GetComponent<ParticleSystem>().main.duration : 0f;
        Destroy(gameObject, Mathf.Max(audioLen, partLen));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
