using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleImpact : MonoBehaviour {

	Rigidbody rb;
	ParticleSystem system;

	float speed;
	float speedLF;

	private void Start () {
		rb = transform.parent.gameObject.GetComponent<Rigidbody>();
		system = GetComponent<ParticleSystem>();
		speedLF = 0f;
	}

	private void Update () {
		speed = rb.velocity.magnitude;
		float difference = speed - speedLF;
		if (difference < -5f) {
			system.Play();
		}

		speedLF = speed;
	}
}
