using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public List<GameObject> collisionEffects = new List<GameObject>();
	float impactThreshold = 15f;
    float maxThreshold = 50f;

    Animator anim;
    int collide_hash;

    private void Start()
    {
        anim = GetComponent<Animator>();
        collide_hash = Animator.StringToHash("Collide");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > impactThreshold)
        {
            if (anim)
                anim.SetTrigger(collide_hash);
            float vol = Mathf.Clamp01(collision.impulse.magnitude / maxThreshold);
            if (collisionEffects.Count > 0) {
                GameObject fx = collisionEffects[Random.Range(0, collisionEffects.Count)];
                Fx_Spawner.instance.SpawnFX(fx, collision.contacts[0].point, collision.contacts[0].normal, vol);
            }
        }
    }
}
