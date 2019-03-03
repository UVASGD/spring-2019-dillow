using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public CollisionDel CollisionEvent;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public TagHandler tagH;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        tagH = GetComponent<TagHandler>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public virtual void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<TagHandler>()) 
        {
            Collide(c.GetComponent<TagHandler>());
        }
    }

    public virtual void OnCollisionEnter(Collision c)
    {
        if (c.collider.GetComponent<TagHandler>() )
        {
            Collide(c.collider.GetComponent<TagHandler>(), c.contacts[0].normal, c.impulse);
        }
    }
    
    public virtual void OnParticleCollision(GameObject c)
    {
        if (c.GetComponent<TagHandler>()) 
        {
            Collide(c.GetComponent<TagHandler>());
        }
    }

    public virtual void Collide(TagHandler t, Vector3? direction = null, Vector3? impact = null)
    {
        Vector3 dir = (Vector3)((direction == null) ? Vector3.up : direction);
        Vector3 imp = (Vector3)((impact == null) ? Vector3.zero : impact);
    }

    public virtual void Collide(List<Tag> tags, Vector3? direction = null, Vector3? impact = null)
    {
        Vector3 dir = (Vector3)((direction == null) ? Vector3.up : direction);
        Vector3 imp = (Vector3)((impact == null) ? Vector3.zero : impact);
    }
}
