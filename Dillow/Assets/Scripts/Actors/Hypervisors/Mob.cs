using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BehaviorDel();

public class Mob : Body
{

    protected Ragdoll ragdoll;
    protected BehaviorDel current_behavior;
    protected GameObject target;
    
    //Minimum magnitude of the impact for a mob to die
    public int impactThresh;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        ragdoll = GetComponent<Ragdoll>();

        damager = GetComponent<Damager>(); 
        damager.DamageEndEvent += Destroy;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            transform.position = hit.point;
            transform.up = hit.normal;
        }
    }

    protected void Update()
    {
        if (!dead)
        {
            current_behavior?.Invoke();
        }
    }

    protected virtual void Die(Vector3 dir)
    {
        if (!dead)
        {
            dead = true;
            ragdoll?.ActivateRagdoll(true);
            damager.Damage(dir);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public override void Collide(List<Tag> tags = null, TagHandler t = null,
        Vector3? direction = null, Vector3? impact = null)
    {
        Vector3 dir = (Vector3)((direction == null) ? Vector3.up : direction);
        Vector3 imp = (Vector3)((impact == null) ? Vector3.zero : impact);

        if (t.HasTag(Tag.Player) && imp.magnitude >= impactThresh)
        {
            Die(dir);
        }
    }
}
