using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public GameObject collisionEffects;
    float impactThreshold = 15f, maxThreshold 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > impactThreshold)
        {
            float vol = Mathf.Clamp01(collision.impulse.magnitude / maxThreshold);
            Fx_Spawner.instance.SpawnFX(collisionEffects, collisionEffects.transform.position, Vector3.forward, vol);
        }
    }
}
