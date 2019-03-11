using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public List<Rigidbody> rigidList;
    Rigidbody rb;
    Animator ani;


    void Start() {
        rigidList = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        ani = GetComponent<Animator>();

        rb = gameObject.GetMainRigidbody();

        Body body = GetComponent<Body>();
        foreach(Rigidbody r in rigidList)
        {
            r.gameObject.AddComponent<SubCollider>().body = body;
        }
        ActivateRagdoll(false);
    }
    
    public void ActivateRagdoll(bool activate) { 
        foreach (Rigidbody r in rigidList)
        {
            r.isKinematic = !activate;
        }
        if (ani)
            ani.enabled = !activate;
        if (rb)
            rb.isKinematic = !activate;
    }
}
