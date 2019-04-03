using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;

public class Fx_Object : MonoBehaviour
{
    protected float pitch_range = 0.2f, amp_range = 0.02f;
    public float vol = -1f;

    public AudioMixerGroup mixerGroup;
    // Start is called before the first frame update
    void Start()
    {

        LookAtConstraint lac =GetComponent<LookAtConstraint>();
        if (lac) {
            ConstraintSource src = new ConstraintSource();
            src.sourceTransform = GameObject.FindGameObjectWithTag("Player").transform;
            src.weight = 1;
            lac.AddSource(src);
            lac.constraintActive = true;
        }

        float max_audio_len = 0, max_part_len = 0;
        foreach (AudioSource aud in GetComponents<AudioSource>())
        {
            if (vol != -1) {
                aud.volume = vol;
            }
            aud.outputAudioMixerGroup = mixerGroup;
            aud.pitch += Random.Range(-pitch_range, pitch_range);
            aud.volume += Random.Range(-amp_range, 0);
            if (mixerGroup) aud.outputAudioMixerGroup = mixerGroup;
            if (aud.clip.length > max_audio_len) max_audio_len = aud.clip.length;
        }
        foreach (ParticleSystem part in GetComponentsInChildren<ParticleSystem>())
        {
            if (part.main.duration > max_part_len) max_part_len = part.main.duration;
        }
        Destroy(gameObject, Mathf.Max(max_audio_len, max_part_len));
    }
}
