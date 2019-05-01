using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(Button))]
public class ThreeDButton : MonoBehaviour {

    private Animator anim;
    private Button method;

    [Header("Navigation")]
    public ThreeDButton up;
    public ThreeDButton down;
    public ThreeDButton left;
    public ThreeDButton right;


    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        method = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void Invoke() {
        method.onClick?.Invoke();
    }

    public void Activate() {
        anim.SetBool("Active", true);
    }

    public void Deactivate() {
        anim.SetBool("Active", false);
    }
}
