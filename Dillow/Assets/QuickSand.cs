using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSand : MonoBehaviour
{
    Dictionary<Rigidbody, float> drags = new Dictionary<Rigidbody, float>();
    public float dragValue = 20f;
    int layermask;
    Collider ground;
    // Start is called before the first frame update
    void Start()
    {
        layermask = LayerMask.GetMask("Ground");
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (transform.up * 5f), -transform.up, out hit, 10f, layermask))
        {
            ground = hit.collider;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Rigidbody r in drags.Keys)
        {
            r.AddForce(-(Vector3.up * 5f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody r = other.GetComponent<Rigidbody>();
        if (r)
        {
            Physics.IgnoreCollision(r.GetComponent<Collider>(), ground);
            r.useGravity = false;
            if (!drags.ContainsKey(r))
            {
                drags.Add(r, r.drag);
            }
            r.drag = dragValue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody r = other.GetComponent<Rigidbody>();
        if (r)
        {
            Physics.IgnoreCollision(r.GetComponent<Collider>(), ground, false);
            r.useGravity = true;
            r.drag = drags[r];
            drags.Remove(r);
        }
    }
}
