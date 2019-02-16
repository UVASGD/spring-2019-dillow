using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx_Object : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Mathf.Max(gameObject.GetComponent<ParticleSystem>().main.duration, 
            gameObject.GetComponent<AudioSource>().clip.length));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
