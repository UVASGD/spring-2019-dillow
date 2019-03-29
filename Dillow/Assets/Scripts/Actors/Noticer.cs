using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CollisionDel(TagHandler handler);

public class Noticer : MonoBehaviour
{
    public CollisionDel NoticeEvent, UnnoticeEvent;

    Collider col;

    bool detecting = true;

    private void Start()
    {
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TagHandler>())
            NoticeEvent?.Invoke(other.GetComponent<TagHandler>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (detecting && other.GetComponent<TagHandler>())
            UnnoticeEvent?.Invoke(other.GetComponent<TagHandler>());
    }

    public void Blink()
    {
        StartCoroutine(BlinkCo());
    }

    IEnumerator BlinkCo()
    {
        detecting = false;
        col.enabled = false;
        yield return null;
        detecting = true;
        col.enabled = true;
    }
}