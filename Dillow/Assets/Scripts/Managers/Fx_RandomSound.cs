using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx_RandomSound : Fx_Object
{
    public List<AudioClip> clips = new List<AudioClip>();

    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();

        if (!aud || clips.Count == 0) return;
        aud.clip = clips[Random.Range(0, clips.Count)];

        if (vol != -1)
        {
            aud.volume = vol;
        }
        aud.pitch += Random.Range(-pitch_range, pitch_range);
        aud.volume += Random.Range(-amp_range, 0);

        Destroy(gameObject, aud.clip.length);
    }
}
