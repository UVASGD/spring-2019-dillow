using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CollisionDel(TagHandler handler);

public class Noticer : MonoBehaviour
{
    public CollisionDel CollisionEnterEvent, CollisionExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TagHandler>())
            CollisionEnterEvent?.Invoke(other.GetComponent<TagHandler>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TagHandler>())
            CollisionExitEvent?.Invoke(other.GetComponent<TagHandler>());
    }
}