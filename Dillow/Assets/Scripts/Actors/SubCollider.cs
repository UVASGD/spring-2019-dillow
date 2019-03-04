using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCollider : MonoBehaviour
{
    public Body body;

    public void OnCollisionEnter(Collision c)
    {
        if (c.collider.GetComponent<TagHandler>())
        {
            body.Collide(t:c.collider.GetComponent<TagHandler>(), direction:c.contacts[0].normal, impact:c.impulse);
        }
    }

    public void OnParticleCollision(GameObject c)
    {
        if (c.GetComponent<TagHandler>())
        {
            body.Collide(t:c.GetComponent<TagHandler>());
        }
    }
}