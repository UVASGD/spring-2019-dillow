using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Fx_Object_Ranged : Fx_Object { 

    public List<SoundRange> sounds;
    public Vector2 range;
    public float amplify = 1.2f;
    public float blend_ratio = 1f;

    // Start is called before the first frame update
    void Start() {
        float max_audio_len = 0, max_part_len = 0;
        if(sounds.Count > 0) {
            sounds.Sort(delegate (SoundRange c1, SoundRange c2) { return c1.threshold.CompareTo(c2.threshold); });
            SoundRange i = new SoundRange(null, 0);

            // Choose a sound to play within the range of the list of sounds
            for (int s = 0; s<sounds.Count;s++) if(vol <= sounds[s].threshold && vol > (s < 1 ? 0 : sounds[s - 1].threshold)) i = sounds[s];

            if (i.sound != null) {
                AudioClip sound = i.sound;
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.outputAudioMixerGroup = mixerGroup;
                source.volume = vol * amplify;
                source.minDistance = range.x;
                source.maxDistance = range.y;
                source.spatialBlend = blend_ratio;
                source.pitch += Random.Range(-pitch_range, pitch_range);
                source.playOnAwake = true;
                source.loop = false;
                source.PlayOneShot(sound);
                max_audio_len = sound.length;
            }
        }


        foreach (ParticleSystem part in GetComponentsInChildren<ParticleSystem>()) {
            if (part.main.duration > max_part_len) max_part_len = part.main.duration;
        }
        Destroy(gameObject, Mathf.Max(max_audio_len, max_part_len));
    }
}

[System.Serializable]
public struct SoundRange {
    public AudioClip sound;
    public float threshold;
    public SoundRange(AudioClip clip, float _threshold) {
        sound = clip;
        threshold = _threshold;
    }
}