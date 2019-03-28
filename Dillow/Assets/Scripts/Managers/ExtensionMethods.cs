using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Rigidbody GetMainRigidbody(this GameObject g)
    {
        foreach (Rigidbody r in g.GetComponentsInChildren<Rigidbody>())
        {
            if (r.CompareTag("MainBody")) return r;
        }
        return g.GetComponent<Rigidbody>();
    }
}
