using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Fx_Object_Ranged : Fx_Object { 

    public List<SoundRange> sounds;
    public Vector2 range;
    public float amplify = 1.2f;

    // Start is called before the first frame update
    void Start() {
        float max_audio_len = 0, max_part_len = 0;
        if(sounds.Count > 0) {
            sounds.Sort(delegate (SoundRange c1, SoundRange c2) { return c1.threshold.CompareTo(c2.threshold); });
            SoundRange i = new SoundRange(null, 0);

            // Choose a sound to play within the range of the list of sounds
            for (int s = 0; s<sounds.Count;s++) if(vol <= sounds[s].threshold && vol > (s < 1 ? 0 : sounds[s - 1].threshold)) i = sounds[s];

            if (i.sound != null) {
                AudioClip sound = ACList.audioClips[i.sound];
                gameObject.name = sound.name;
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.outputAudioMixerGroup = mixerGroup;
                source.volume = vol * amplify;
                source.minDistance = range.x;
                source.maxDistance = range.y;
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
    public string sound;
    public float threshold;
    public SoundRange(string clip, float _threshold) {
        sound = clip;
        threshold = _threshold;
    }
}


public static class ACList {

    public static Dictionary<string, AudioClip> audioClips;
    static ACList() {
        audioClips = new Dictionary<string, AudioClip>();
        List<AudioClip> temp = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds"));
        foreach (AudioClip ac in temp) {
            var ass = ac.name + ac.GetHashCode();
            //Debug.Log(ass + " : " + ac);
            audioClips[ass] = ac;
        }
    }
}