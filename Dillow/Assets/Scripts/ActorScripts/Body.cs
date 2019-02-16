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

    public void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<TagHandler>()) 
        {
            Collide(c.GetComponent<TagHandler>());
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        if (c.collider.GetComponent<TagHandler>() )
        {
            Collide(c.collider.GetComponent<TagHandler>());
        }
    }
    
    public void OnParticleCollision(GameObject c)
    {
        if (c.GetComponent<TagHandler>()) 
        {
            Collide(c.GetComponent<TagHandler>());
        }
    }   

    public virtual void Collide(TagHandler t)
    {

    }
}
