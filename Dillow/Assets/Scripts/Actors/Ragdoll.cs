using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public List<Rigidbody> rigidList;
    [HideInInspector] public Rigidbody rb;
    Animator ani;


    void Awake() {
        rigidList = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        ani = GetComponent<Animator>();

        rb = gameObject.GetMainRigidbody();

        Body body = GetComponent<Body>();

        for (int i = rigidList.Count-1; i >= 0; i--)
        {
            Rigidbody r = rigidList[i];
            if (r.CompareTag("Follower")) rigidList.Remove(r);
            else r.gameObject.AddComponent<SubCollider>().body = body;
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
