using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSounds : MonoBehaviour
{
    public List<GameObject> sounds = new List<GameObject>();
    public float soundTime = 10f;
    bool playing;

    // Update is called once per frame
    void Update()
    {
        if (!playing && sounds.Count > 0)
        {
            StartCoroutine(PlayIdle());
        }
    }

    IEnumerator PlayIdle()
    {
        playing = true;
        yield return new WaitForSeconds(Random.Range(1f, soundTime));
        GameObject fx = sounds[Random.Range(0, sounds.Count)];
        Fx_Spawner.instance.SpawnFX(fx, fx.transform.position, fx.transform.forward);
        playing = false;
    }
}
