using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//[RequireComponent(typeof(AudioSource))]
public class CollectibleSoundController : MonoBehaviour {

    public static CollectibleSoundController instance;
    public Vector2 range = 5 * Vector2.one;
    public AudioMixerGroup mixer;
    List<AudioSource> sounds;

    void Awake() {
        if (instance != null) {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        //source = GetComponent<AudioSource>();
        sounds = new List<AudioSource>();
    }

    void Update() {
        List<AudioSource> toRemove = new List<AudioSource>();
        foreach(AudioSource src in sounds) {
            if (!src.isPlaying) {
                Destroy(src.gameObject);
                toRemove.Add(src);
            }
        }
        foreach (AudioSource v in toRemove) sounds.Remove(v);
    }

    public void PlaySound(AudioSource audio, float volume = 1) {
        PlaySound(audio.clip, volume);
    }

    public void PlaySound(AudioClip clip, float volume = 1) {
        GameObject g = new GameObject(clip.name + "(" + volume + ")");
        g.transform.SetParent(transform);
        AudioSource source = g.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = mixer;
        source.minDistance = range.x;
        source.maxDistance = range.y;
        source.volume = volume;
        source.clip = clip;
        source.Play();
        sounds.Add(source);
    }
}
