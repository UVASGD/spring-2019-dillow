using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {
	public string enterMusic;
	public string exitMusic;
	public bool loop = true;
	public bool crossfade = true;
	public bool fadein = true;
	public float fadeDuration = 5f;
	public bool syncTimes = true;


	private void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")) {
			AudioManager.PlayMusic(enterMusic, loop, crossfade, fadein, fadeDuration, syncTimes);
		}
	}

	private void OnTriggerExit (Collider other) {
		if (other.CompareTag("Player")) {
			AudioManager.PlayMusic(exitMusic, loop, crossfade, fadein, fadeDuration, syncTimes);
		}
	}
}
