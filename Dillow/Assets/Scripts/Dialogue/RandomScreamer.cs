using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
///<summary> You're gonna love this. I guarantee it</summary>
public class RandomScreamer : MonoBehaviour {

    AudioSource src;
    Coroutine screamer;

    public Vector2 screamTime = new Vector2(12f, 18f);
    
    void Start() {
        src = GetComponent<AudioSource>();
    }
    
    void Update() {
        if (screamer == null)
            screamer = StartCoroutine(IdleScream());
    }

    public IEnumerator IdleScream() {
        yield return new WaitForSeconds(Random.Range(
            Mathf.Min(screamTime.x, screamTime.y),
            Mathf.Max(screamTime.x, screamTime.y)));
        src.Play();
        StartCoroutine(Scream());
    }

    public IEnumerator Scream() {
        yield return new WaitForSeconds(src.clip.length);
        screamer = null;
    }
}
