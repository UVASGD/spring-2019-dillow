using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Messager : MonoBehaviour
{
    public static Messager instance;

    Animator anim;
    int intro_hash, fadeOut_hash;

    public Text text;

    float message_time = 5f, wait_time = 3f;

    private bool displaying = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        intro_hash = Animator.StringToHash("Show");
        fadeOut_hash = Animator.StringToHash("FadeOut");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator DisplayMessage(string message)
    {
        print("DisplayMessage");
        while (displaying)
        {
            print("Queued...");
            yield return null;
        }
        print("Displaying message: " + message);
        displaying = true;
        anim.SetBool(intro_hash, true);
        text.text = message;
        yield return new WaitForSeconds(message_time);
        print("Fading out");
        anim.SetBool(intro_hash, false);
        anim.SetTrigger(fadeOut_hash);
        print("Done");
        yield return new WaitForSeconds(wait_time);
        displaying = false;
    }
}
