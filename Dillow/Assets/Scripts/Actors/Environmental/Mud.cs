using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{
    Dictionary<Rigidbody, float> drags = new Dictionary<Rigidbody, float>();
    public float dragValue = 5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody r =  other.GetComponent<Rigidbody>();
        if (r)
        {   
            if( !drags.ContainsKey(r) )
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
            r.drag = drags[r];
        }
    }
}
