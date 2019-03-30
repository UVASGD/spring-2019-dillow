// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Music manager which provides basic music and sound effect functionality.
/// Music playback persists across scene loads.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public MusicList musicList;

	protected AudioSource[] audioSource;
	private int sourceNum;

	private float timeSinceActive;

	private bool switchingMusic;


    protected virtual void Awake()
    {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}

		musicList.Init();

		timeSinceActive = 0f;
		sourceNum = 0;

		switchingMusic = false;

		audioSource = new AudioSource[2];
		audioSource[0] = gameObject.GetComponent<AudioSource>();
		audioSource[1] = gameObject.AddComponent<AudioSource>();

		audioSource[1].playOnAwake = audioSource[0].playOnAwake;
		audioSource[1].loop = audioSource[0].loop;
		audioSource[1].priority = audioSource[0].priority;
		audioSource[1].volume = 0;
		audioSource[1].pitch = audioSource[0].pitch;
		audioSource[1].panStereo = audioSource[0].panStereo;
		audioSource[1].spatialBlend = audioSource[0].spatialBlend;
		audioSource[1].reverbZoneMix = audioSource[0].reverbZoneMix;
	}

	protected virtual void Update () {
		timeSinceActive += Time.deltaTime;
	}

	protected virtual AudioSource[] GetAudioSources () {
		AudioSource previous = audioSource[sourceNum];
		sourceNum = (sourceNum + 1) % audioSource.Length;
		return new AudioSource[] { audioSource[sourceNum], previous };
	}

	#region Public members

	public static void PlayMusic (string name, bool loop = true, bool crossfade = true, bool fadein = true, float fadeDuration = 2f, bool syncTimes = false) {
		if (true == instance.switchingMusic) return;

		AudioSource[] sources = instance.GetAudioSources ();
		AudioClip clip = instance.musicList.GetMusic(name);

		if (null != clip) {
			instance.StartCoroutine(instance.MusicCoroutine(clip, sources[0], sources[1], loop, crossfade, fadein, fadeDuration, syncTimes));
		}
	}

	public IEnumerator MusicCoroutine (AudioClip clip, AudioSource current, AudioSource previous, 
										bool loop, bool crossfade, bool fadein, float fadeDuration, bool syncTimes) {
		switchingMusic = true;
		print("Switching to " + clip.name);
		current.loop = loop;
		current.clip = clip;

		if (syncTimes) {
			current.time = previous.time % clip.length;
		} else {
			current.time = 0f;
		}

		float fadeTime = 0f;
		float previousStartVolume = previous.volume;
		float currentStartVolume = current.volume;
		if (crossfade) {
			current.Play();
			while (fadeTime < fadeDuration) {
				previous.volume = Mathf.Lerp(previousStartVolume, 0f, fadeTime / fadeDuration);
				current.volume = Mathf.Lerp(currentStartVolume, previousStartVolume, fadeTime / fadeDuration);
				fadeTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			previous.volume = 0f;
			currentStartVolume = previousStartVolume;
		} else {
			while (fadeTime < fadeDuration) {
				previous.volume = Mathf.Lerp(previousStartVolume, 0f, fadeTime / fadeDuration);
				fadeTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			previous.volume = 0f;

			fadeTime = 0f;

			if (!fadein) {
				fadeDuration = 0.25f;
			}

			current.Play();

			while (fadeTime < fadeDuration) {
				current.volume = Mathf.Lerp(currentStartVolume, previousStartVolume, fadeTime / fadeDuration);
				fadeTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			current.volume = previousStartVolume;
		}

		switchingMusic = false;
	}

    /// <summary>
    /// Plays a sound effect once, at the specified volume.
    /// </summary>
    /// <param name="soundClip">The sound effect clip to play.</param>
    /// <param name="volume">The volume level of the sound effect.</param>
    public virtual void PlaySound(AudioClip soundClip, float volume)
    {
        audioSource[0].PlayOneShot(soundClip, volume);
    }

    /// <summary>
    /// Shifts the game music pitch to required value over a period of time.
    /// </summary>
    /// <param name="pitch">The new music pitch value.</param>
    /// <param name="duration">The length of time in seconds needed to complete the pitch change.</param>
    /// <param name="onComplete">A delegate method to call when the pitch shift has completed.</param>
    public virtual void SetAudioPitch(float pitch, float duration, System.Action onComplete)
    {
        if (Mathf.Approximately(duration, 0f))
        {
            audioSource[0].pitch = pitch;
            if (onComplete != null)
            {
                onComplete();
            }
            return;
        }

        LeanTween.value(gameObject, 
            audioSource[0].pitch, 
            pitch, 
            duration).setOnUpdate( (p) => {
                audioSource[0].pitch = p;
            }).setOnComplete( () => {
                if (onComplete != null)
                {
                    onComplete();
                }
            });
    }

    /// <summary>
    /// Fades the game music volume to required level over a period of time.
    /// </summary>
    /// <param name="volume">The new music volume value [0..1]</param>
    /// <param name="duration">The length of time in seconds needed to complete the volume change.</param>
    /// <param name="onComplete">Delegate function to call when fade completes.</param>
    public virtual void SetAudioVolume(float volume, float duration, System.Action onComplete)
    {
        if (Mathf.Approximately(duration, 0f))
        {
			if (onComplete != null)
			{
				onComplete();
			}				
            audioSource[0].volume = volume;
            return;
        }

        LeanTween.value(gameObject, 
            audioSource[0].volume, 
            volume, 
            duration).setOnUpdate( (v) => {
                audioSource[0].volume = v;
            }).setOnComplete( () => {
                if (onComplete != null)
                {
                    onComplete();
                }
            });
    }

    /// <summary>
    /// Stops playing game music.
    /// </summary>
    public virtual void StopMusic()
    {
		for (int i = 0; i < audioSource.Length; i++) {
			audioSource[i].Stop();
			audioSource[i].clip = null;
		}
    }

    #endregion
}