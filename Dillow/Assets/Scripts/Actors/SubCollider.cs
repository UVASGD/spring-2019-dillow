using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCollider : MonoBehaviour
{
    public Body body;

    public void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<TagHandler>())
        {
            body.Collide(c.GetComponent<TagHandler>());
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        if (c.collider.GetComponent<TagHandler>())
        {
            body.Collide(c.collider.GetComponent<TagHandler>(), c.contacts[0].normal, c.impulse);
        }
    }

    public void OnParticleCollision(GameObject c)
    {
        if (c.GetComponent<TagHandler>())
        {
            body.Collide(c.GetComponent<TagHandler>());
        }
    }
}