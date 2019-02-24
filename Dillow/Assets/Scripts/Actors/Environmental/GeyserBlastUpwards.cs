using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserBlastUpwards : MonoBehaviour
{
    
    public bool activateOnInterval;
    public float activeTime;
    public float waitTime;

    float nextActiveTime;
    float nextWaitTime;

    bool active;

    private void Start()
    {
        if(!activateOnInterval)
        {
            active = true;
            activeTime = 0f;
            waitTime = 0f;
            GetComponent<ParticleSystem>().Play();
        }
        nextActiveTime = waitTime;
        nextWaitTime = waitTime + activeTime;
    }

    public float force;

    private 

    // Update is called once per frame
    void Update()
    {
        if(activateOnInterval && !active && Time.time >= nextActiveTime)
        {
            active = true;
            nextActiveTime += activeTime + waitTime;
            GetComponent<ParticleSystem>().Play();
        }
        if(activateOnInterval && active && Time.time >= nextWaitTime)
        {
            active = false;
            nextWaitTime += waitTime + activeTime;
            GetComponent<ParticleSystem>().Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (active)
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * force);
        }
    }
}
