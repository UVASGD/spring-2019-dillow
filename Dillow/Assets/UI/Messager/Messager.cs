using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Messager : MonoBehaviour
{
    public static Messager instance;

    [HideInInspector] public Animator anim;
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
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        intro_hash = Animator.StringToHash("Show");
        fadeOut_hash = Animator.StringToHash("FadeOut");
    }

    public IEnumerator DisplayMessage(string message)
    {
        //print("DisplayMessage");
        while (displaying)
        {
            //print("Queued...");
            yield return null;
        }
        //print("Displaying message: " + message);
        displaying = true;
        anim.SetBool(intro_hash, true);
        text.text = message;
        yield return new WaitForSeconds(message_time);
        //print("Fading out");
        anim.SetBool(intro_hash, false);
        anim.SetTrigger(fadeOut_hash);
        //print("Done");
        yield return new WaitForSeconds(wait_time);
        displaying = false;
    }
}
