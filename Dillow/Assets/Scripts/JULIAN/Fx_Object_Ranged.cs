using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Fx_Object_Ranged : Fx_Object { 

    public List<SoundRange> sounds;
    void OnEnable() {
        sounds = new List<SoundRange>();
    }

    // Start is called before the first frame update
    void Start() {
        float max_audio_len = 0, max_part_len = 0;
        SoundRange upper = new SoundRange(null, 0);
        SoundRange lower = new SoundRange(null, 1);
        if(sounds.Count > 0) {
            // Choose a sound to play within the range of the list of sounds
            // Think of it like a grouping, with the range a sound can play by
            // default being from 0 to its threshold, the minimum overriden by
            // sounds of lower thresholds

            // calculated this way to save time on sorting (over the top)
            foreach(SoundRange sound in sounds) {
                print(string.Format("{0} >= {1} || {0} > {2} ) && {3} <= {0}", sound.threshold, lower.threshold, upper.threshold, vol));
                if ((sound.threshold >= lower.threshold || sound.threshold > upper.threshold)
                    && vol <= sound.threshold)upper = sound;
                else lower = sound.threshold < lower.threshold ? sound : lower;
            }

            if (upper.sound != null) {
                AudioClip sound = Resources.Load<AudioClip>(upper.sound);
                gameObject.name = sound.name;
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.outputAudioMixerGroup = mixerGroup;
                source.volume = vol;
                source.pitch += Random.Range(-pitch_range, pitch_range);
                source.playOnAwake = true;
                source.clip = sound;
                source.loop = false;
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
    public string sound;
    public float threshold;
    public SoundRange(string clip, float _threshold) {
        sound = clip;
        threshold = _threshold;
    }
}
