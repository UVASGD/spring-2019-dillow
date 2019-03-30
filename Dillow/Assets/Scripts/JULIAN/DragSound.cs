using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DragEvent();

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class DragSound : MonoBehaviour {

    private readonly float maxSpeed = 10f, pitch_range = 0.2f;
    float startPitch;

    AudioSource sound;
    Rigidbody rb;
    public DragEvent StartDrag, StopDrag;
    int groundCount;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
        sound.loop = true;

        startPitch = sound.pitch;
    }

    // Update is called once per frame
    void Update() {
        if (groundCount >= 0) {
            float speed = Mathf.Clamp01(rb.velocity.magnitude / maxSpeed);
            sound.volume = speed;
        } else {
            sound.volume = 0;
        }
    }

    private void OnTriggerEnter(Collider other) {
        groundCount++;

        if (groundCount == 1) {
            StartDrag?.Invoke();
            sound.pitch += Random.Range(-pitch_range, pitch_range);
        }
    }

    private void OnTriggerExit(Collider other) {
        groundCount--;

        if (groundCount <= 0) {
            groundCount = 0;
            StopDrag?.Invoke();
        }
    }
}
