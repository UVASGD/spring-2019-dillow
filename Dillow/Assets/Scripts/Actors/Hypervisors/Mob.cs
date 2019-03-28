using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BehaviorDel();

public class Mob : Body, ILockable
{
    [Header("POSITIONING")]
    public bool start_on_ground = true;


    protected Ragdoll ragdoll;
    protected BehaviorDel current_behavior;
    protected GameObject target, main_body;

    public GameObject death_fx;
    
    //Minimum magnitude of the impact for a mob to die
    public float impactThresh = 20f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        ragdoll = GetComponent<Ragdoll>();

        damager = GetComponent<Damager>(); 
        damager.DamageEndEvent += Destroy;

        if (start_on_ground)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                transform.position = hit.point;
                transform.up = hit.normal;
            }
        }

        main_body = (ragdoll) ? ragdoll.rb.gameObject : gameObject;
        main_body.AddComponent<MainBody>();
    }

    protected virtual void Update()
    {
        if (!dead)
        {
            current_behavior?.Invoke();
        }
    }

    protected virtual void Die(Vector3 dir, Vector3 pos)
    {
        if (!dead)
        {
            dead = true;
            ragdoll?.ActivateRagdoll(true);
            current_behavior = null;
            damager.Damage(dir, pos);
        }
    }

    public void Destroy()
    {
        if (death_fx)
        {
            Fx_Spawner.instance.SpawnFX(death_fx, main_body.transform.position, Vector3.up);
        }
        Destroy(gameObject);
    }

    public override void Collide(Vector3 pos, List<Tag> tags = null, TagHandler t = null,
        Vector3? direction = null, Vector3? impact = null)
    {
        Vector3 dir = (Vector3)((direction == null) ? Vector3.up : direction);
        Vector3 imp = (Vector3)((impact == null) ? Vector3.zero : impact);

        if (t.HasTag(Tag.Player) && imp.magnitude >= impactThresh)
        {
            Die(dir, pos);
        }
    }
}
