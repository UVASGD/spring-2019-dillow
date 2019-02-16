using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float upwardForce = 500f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HitBack(Vector3 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        var force = Vector3.ProjectOnPlane(direction, Vector3.up);
        force.y = upwardForce;
        rb.AddForce(force);
    }
}
