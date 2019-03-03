using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

	Rigidbody rb;
	TrailRenderer trail;
	[SerializeField] float secondsPastThreshold;

	bool aboveThreshold;
	bool aboveThresholdLF;

	private void Start () {
		rb = transform.parent.GetComponent<Rigidbody>();
		trail = GetComponent<TrailRenderer>();
		secondsPastThreshold = 0f;
		aboveThreshold = false;
		aboveThresholdLF = aboveThreshold;
	}

	private void Update () {
		if (rb.velocity.magnitude > 18f) {
			aboveThreshold = true;

			if (!aboveThresholdLF) {
				secondsPastThreshold = 0f;
			}

			if (secondsPastThreshold > 1f) {
				trail.emitting = true;
			}
		} else {
			aboveThreshold = false;

			if (aboveThresholdLF) {
				secondsPastThreshold = 0f;
				trail.emitting = false;
			}
		}

		secondsPastThreshold += Time.deltaTime;
		aboveThresholdLF = aboveThreshold;
	}
}
