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
        return (g.GetComponent<Rigidbody>()) ? g.GetComponent<Rigidbody>():g.GetComponentInChildren<Rigidbody>();
    }

    public static bool HasTags(this GameObject g, bool all = false, params Tag[] t)
    {
        TagHandler th = g.GetComponent<TagHandler>();
        if (!th) th = g.GetComponentInParent<TagHandler>();

        if (!th) return false;
        else
        {
            bool found = all;
            foreach (Tag tag in t)
            {
                if (!th.HasTag(tag))
                    if (all)
                        return false;
                else
                    found = true;
            }
            return found;
        }
    }

    public static bool HasTag(this GameObject g, Tag t)
    {
        TagHandler th = g.GetComponent<TagHandler>();
        if (!th) th = g.GetComponentInParent<TagHandler>();

        if (!th) return false;
        else return th.HasTag(t);
    }

    public static T GetAnyComponent<T>(this GameObject g, bool in_parent = true, bool in_children = true, int neighbor_depth = 0) where T : Component
    {
        if (g.GetComponent<T>() != null)
        {
            Debug.Log("PLEASE " + (g.GetComponent<T>() == null));
            return g.GetComponent<T>();
        }
        else if (in_children && g.GetComponentInChildren<T>() != null)
        {
            return g.GetComponentInChildren<T>();
        }
        else if (in_parent)
            if (g.GetComponentInParent<T>() != null)
                return g.GetComponentInParent<T>();

        GameObject current = g;
        while (neighbor_depth > 0)
        {
            current = current.transform.parent.gameObject;
            if (!current)
                break;
            if (current.GetComponentInChildren<T>() != null)
            {
                return current.GetComponentInChildren<T>();
            }
            neighbor_depth--;
        }

        return g.GetComponent<T>();
    } 
}
