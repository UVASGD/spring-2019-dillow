using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//using Cinemachine.Timeline;
using Cinemachine.Utility;

public class CutsceneHandler : MonoBehaviour {
	public static CutsceneHandler instance;

	private void Awake () {
		if (null == instance) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	public void StartCutscene () {

	}
}
