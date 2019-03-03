using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Body
{

    Ragdoll ragdoll;


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

    // Update is called once per frame
    void Update()
    {
        
    }
}
