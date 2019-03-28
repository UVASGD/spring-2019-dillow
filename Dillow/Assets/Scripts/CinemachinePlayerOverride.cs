using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachinePlayerOverride : MonoBehaviour {

	[SerializeField] private bool follow;
	[SerializeField] private bool lookat;

	private CinemachineVirtualCamera cam;

	// Start is called before the first frame update
	void Start () {
		if (null == (cam = GetComponent<CinemachineVirtualCamera>())) {
			return;
		}

		if (true == follow) {
			cam.Follow = GameManager.instance.player.transform;
		}

		if (true == lookat) {
			cam.LookAt = GameManager.instance.player.transform;
		}
	}

	// Update is called once per frame
	void Update () {
        
    }
}
