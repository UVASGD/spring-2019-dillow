using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public List<Rigidbody> rigidList;
    Rigidbody rb;
    public Animator ani;


    void Start() {
        rigidList = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Body body = GetComponent<Body>();
        foreach(Rigidbody r in rigidList)
        {
            r.gameObject.AddComponent<SubCollider>().body = body;
        }
        ActivateRagdoll(false);
    }
    
    void ActivateRagdoll(bool activate) { 
        foreach (Rigidbody r in rigidList)
        {
            r.isKinematic = !activate;
        }
        ani.enabled = !activate;
        rb.isKinematic = !activate;
        
    }

    
}
