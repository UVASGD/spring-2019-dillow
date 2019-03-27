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
            body.Collide(c.contacts[0].point, t:c.collider.GetComponent<TagHandler>(), direction:c.contacts[0].normal, impact:c.impulse);
        }
    }

    public virtual void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<TagHandler>())
        {
            TagHandler th = c.GetComponent<TagHandler>();
            body.Collide(transform.position, t: th, direction: transform.position - c.transform.position);
        }
    }

    public void OnParticleCollision(GameObject c)
    {
        if (c.GetComponent<TagHandler>())
        {
            body.Collide(transform.position, t:c.GetComponent<TagHandler>());
        }
    }
}