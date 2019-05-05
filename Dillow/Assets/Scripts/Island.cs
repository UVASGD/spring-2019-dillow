using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Contains all infoirmation global to the island itself
/// </summary>
public class Island : MonoBehaviour
{

    // There should only be one Island active at a time
    //private void Awake() {
    //    if (current) {
    //        Destroy(this);
    //        print("Why are you adding more than one island!!!");
    //        return;
    //    }

    //    current = this;
    //}

    [Header("Soundtrack")]
    [Tooltip("Overall song for island")]
    public AudioClip IdleMusic;
    [Tooltip("Music to play when entering a generic battle")]
    public AudioClip CombatMusic;
}
