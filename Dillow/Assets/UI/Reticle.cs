﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    RectTransform rect;
    Vector2 velocity = Vector2.zero;
    float smooth_time = 0.1f, max_speed = 0.01f;

    CinemachineFreeLook main;

    AudioSource aud;

    Animation anim;
    Image reticle;

    // Start is called before the first frame update
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        main = GameObject.FindGameObjectWithTag("PlayerCam").GetComponent<CinemachineFreeLook>();
        aud = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
        reticle = GetComponent<Image>();
    }

    public void SetReticle(Vector2 screen_coords)
    {
        //reticle.enabled = visible;
        Vector2 move_to = Vector2.SmoothDamp(rect.position, screen_coords, ref velocity, smooth_time, max_speed);
        rect.position = screen_coords;
    }

    public void Deactivate()
    {
        aud.Stop();
        aud.pitch = 0.75f;
        aud.Play();
        if (gameObject.activeInHierarchy)
            StartCoroutine(WaitForBloop());
    }

    IEnumerator WaitForBloop()
    {
        if (aud.isPlaying)
        {
            yield return null;
        }
        gameObject.SetActive(false);
    }

    public void Activate(GameObject locked)
    {
        velocity = Vector2.zero;
        aud.Stop();
        aud.pitch = 1f;
        aud.Play();
        anim.Play();
        BallController.instance.body.lock_enemy = locked;
    }
}
