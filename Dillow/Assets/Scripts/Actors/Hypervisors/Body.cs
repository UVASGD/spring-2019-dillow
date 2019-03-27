using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public CollisionDel CollisionEvent;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public TagHandler tagH;
    [HideInInspector]public Damager damager;

    protected bool can_be_damaged = true;
    protected bool dead;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        tagH = GetComponent<TagHandler>();
        rb = gameObject.GetMainRigidbody();
        anim = GetComponent<Animator>();
        damager = GetComponent<Damager>();
    }

    public virtual void OnCollisionEnter(Collision c)
    {
        if (c.collider.GetComponent<TagHandler>() )
        {
            TagHandler th = c.collider.GetComponent<TagHandler>();
            Collide(c.contacts[0].point, t:th, direction: c.contacts[0].normal, impact: c.impulse);
        }
    }

    public virtual void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<TagHandler>())
        {
            TagHandler th = c.GetComponent<TagHandler>();
            Collide(transform.position, t:th, direction: transform.position - c.transform.position);
        }
    }
    
    public virtual void OnParticleCollision(GameObject c)
    {
        if (c.GetComponent<TagHandler>()) 
        {
            Collide(transform.position, t:c.GetComponent<TagHandler>());
        }
    }

    public virtual void Collide(Vector3 pos, List<Tag> tags = null, TagHandler t = null, 
        Vector3? direction = null, Vector3? impact = null)
    {
        Vector3 dir = (Vector3)((direction == null) ? Vector3.up : direction);
        Vector3 imp = (Vector3)((impact == null) ? Vector3.zero : impact);
    }
}
