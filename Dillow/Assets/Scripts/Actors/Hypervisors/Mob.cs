using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Body
{

    Ragdoll ragdoll;
    
    //Minimum magnitude of the impact for a mob to die
    public int impactThresh;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        ragdoll = GetComponent<Ragdoll>();
    }

    protected virtual void Die()
    {
        ragdoll.ActivateRagdoll(true);
        damager.Damage(Vector3.up);
    }

    

    public override void Collide(List<Tag> tags = null, TagHandler t = null,
        Vector3? direction = null, Vector3? impact = null)
    {
        Vector3 dir = (Vector3)((direction == null) ? Vector3.up : direction);
        Vector3 imp = (Vector3)((impact == null) ? Vector3.zero : impact);

        if (t.HasTag(Tag.Player) && imp.magnitude >= impactThresh)
        {
            Die();
        }
    }
}
