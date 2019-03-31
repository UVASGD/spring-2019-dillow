using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour, IMortal
{
    public CollisionDel CollisionEvent;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public TagHandler tagH;
    [HideInInspector] public Damager damager;

    public bool can_be_damaged = true, next_hit_kills;
    public bool dead;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        tagH = GetComponent<TagHandler>();
        rb = gameObject.GetMainRigidbody();
        if (!rb.GetComponent<MainBody>())
        {
            rb.gameObject.AddComponent<MainBody>().DeathEvent += Die;
        }
        anim = GetComponent<Animator>();
        damager = GetComponent<Damager>();
    }

    public virtual void OnCollisionEnter(Collision c)
    {
        if (c.collider.GetComponent<TagHandler>() )
        {
            TagHandler th = c.collider.GetComponent<TagHandler>();
            Collide(c.contacts[0].point, tagHandler: th, direction: c.contacts[0].normal, impact: c.impulse);
        }
    }

    public virtual void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<TagHandler>())
        {
            TagHandler th = c.GetComponent<TagHandler>();
            Collide(transform.position, tagHandler: th, direction: transform.position - c.transform.position);
        }
    }
    
    public virtual void OnParticleCollision(GameObject c)
    {
        if (c.GetComponent<TagHandler>()) 
        {
            Collide(transform.position, tagHandler: c.GetComponent<TagHandler>());
        }
    }

    public void Collide(Vector3 pos, TagHandler tagHandler,
        Vector3? direction = null, Vector3? impact = null)
    {
        Collide(pos, tagHandler.tagList, tagHandler.gameObject, direction, impact);
    }

    public void Collide(Vector3 pos, List<Tag> tags = null, GameObject obj = null,
        Vector3? direction = null, Vector3? impact = null)
    {
        Vector3 dir = direction.GetValueOrDefault(Vector3.up);
        Vector3 imp = impact.GetValueOrDefault(Vector3.zero);
        Collide(pos, tags, obj, dir, imp);
    }

    public virtual void Collide(Vector3 pos, List<Tag> tags, GameObject obj, Vector3 direction, Vector3 impact) {
        if (tags.Contains(Tag.Water))
        {
            rb.velocity = Vector3.down;
            obj.GetComponent<Water>().AddVictim(rb.gameObject, rb.GetComponent<Collider>());
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
