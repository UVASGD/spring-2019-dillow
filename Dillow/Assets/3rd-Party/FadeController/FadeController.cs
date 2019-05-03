using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void FadeEvent();

/// <summary>
/// Handles Fading in and out
/// </summary>
[RequireComponent(typeof(Animator))]
public class FadeController : MonoBehaviour {

    public static FadeController instance;
    public Animator anim;

    public bool startOpen;
    public bool animateOnStart;

    public float speed = 1;
    public Color fadeColor = Color.black;
    public Image fadeImage;
    private bool automatic;

    public static FadeEvent FadeOutCompletedEvent, FadeInCompletedEvent, 
        FadeInStartedEvent, FadeOutStartedEvent;

    private object lastColor;
    private bool OverrideColor => lastColor != null;

    /// <summary>
    /// Whether the fade controller has completely faded out
    /// </summary>
    private bool faded;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private int openHash, speedHash;
    void Start() {
        anim = GetComponent<Animator>();
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        openHash = Animator.StringToHash("Open");
        speedHash = Animator.StringToHash("Speed");

        anim.SetBool(openHash, startOpen);
        anim.SetBool("AnimateOnStart", animateOnStart);
        anim.SetFloat(speedHash, speed);
    }

    /// <summary>
    /// Start the fade out animation. Default transition lasts 1/3 s
    /// </summary>
    /// <param name="auto">Whether or not to start fading in automatically</param>
    public void FadeOut(float speed = 1f, bool auto = false) {
        if (faded) return;
        automatic = faded;
        faded = true;
        anim.SetFloat(speedHash, this.speed = speed);
        anim.SetBool(openHash, false);
        FadeOutStartedEvent?.Invoke();
    }

    /// <summary>
    /// Start the fade out animation with a different color than the default.
    /// Default transition lasts 1/3 s
    /// </summary>
    /// <param name="auto">Whether or not to start fading in automatically</param>
    public void FadeOut(Color fadeColor, float speed = 1f, bool auto = false) {
        lastColor = this.fadeColor;
        fadeImage.color = this.fadeColor = fadeColor;
        
        FadeOut(speed, auto);
    }

    /// <summary>
    /// Start the fade in animation. Default transition lasts 1/3 s
    /// </summary>
    public void FadeIn() {
        FadeIn(1f);
    }

    /// <summary>
    /// Fade in after a delay
    /// </summary>
    /// <param name="time">time in seconds to delay</param>
    /// <param name="speed"></param>
    public void DelayFadeIn(float time = 1f, float speed = 1f) {
        StartCoroutine(DelayFadeInCoroutine(time, speed));
    }

    private IEnumerator DelayFadeInCoroutine(float time, float speed) {
        yield return new WaitForSeconds(time);
        FadeIn(speed: speed);
    }

    /// <summary>
    /// Fade out after a delay
    /// </summary>
    /// <param name="time">time in seconds to delay</param>
    /// <param name="speed"></param>
    public void DelayFadeOut(float time = 1f, float speed = 1f, bool auto = false) {
        StartCoroutine(DelayFadeOutCoroutine(time, speed, auto));
    }

    private IEnumerator DelayFadeOutCoroutine(float time, float speed, bool auto) {
        yield return new WaitForSeconds(time);
        FadeOut(speed: speed, auto: auto);
    }

    /// <summary>
    /// Start the fade in animation with a speed
    /// </summary>
    public void FadeIn(float speed) {
        if (!faded) return;
        faded = false;
        anim.SetFloat(speedHash, speed);
        anim.SetBool(openHash, true);
        FadeInStartedEvent?.Invoke();
    }

    /// <summary>
    /// Method Called by animation event when screen has completely faded to black;
    /// Triggers FadeOut Event
    /// </summary>
    public void HandleFadeOut() {
        FadeOutCompletedEvent?.Invoke();
        if (automatic) FadeIn();
    }

    /// <summary>
    /// Triggered when animation has completely faded in
    /// </summary>
    public void HandleFadedIn() {
        if (OverrideColor) {
            fadeImage.color = fadeColor = (Color)lastColor;
        }
        FadeInCompletedEvent?.Invoke();
    }
}
