using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Skyrim : MonoBehaviour
{
    VideoPlayer v;
    bool started;
    // Start is called before the first frame update
    void Awake()
    {
        v = GetComponent<VideoPlayer>();
        v.targetCamera = Camera.main;
        FadeController.FadeInCompletedEvent += Play;
        v.started += delegate { started = true; };
    }

    void Play()
    {
        v.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (started && !v.isPlaying)
        {
            print("oof");
            Destroy(gameObject);
        }
    }


}
