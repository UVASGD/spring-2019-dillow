using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx_Object : MonoBehaviour
{
    public float pitch_range = 0.4f;
    public float amp_range = 0.4f;
    public float vol = -1f;
    // Start is called before the first frame update
    void Start()
    {
        float max_audio_len = 0, max_part_len = 0;
        foreach (AudioSource aud in GetComponents<AudioSource>())
        {
            if (vol != -1) {
                aud.volume = vol;
            }
            aud.pitch += Random.Range(-pitch_range, pitch_range);
            aud.volume += Random.Range(-amp_range, 0);
            if (aud.clip.length > max_audio_len) max_audio_len = aud.clip.length;
        }
        foreach (ParticleSystem part in GetComponentsInChildren<ParticleSystem>())
        {
            if (part.main.duration > max_part_len) max_part_len = part.main.duration;
        }
        Destroy(gameObject, Mathf.Max(max_audio_len, max_part_len));
    }
}
