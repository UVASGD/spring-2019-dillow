using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBody : Body
{
    int talking_hash, wiggle_hash;

    float wiggle_threshold = 15f;

    IdleSounds idleSounds;

    public GameObject collision_fx;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        talking_hash = Animator.StringToHash("Talking");
        wiggle_hash = Animator.StringToHash("Wiggle");

        idleSounds = GetComponent<IdleSounds>();
    }

    public override void Collide(Vector3 pos, List<Tag> tags, GameObject obj, Vector3 direction, Vector3 impact)
    {
        base.Collide(pos, tags, obj, direction, impact);

        if (impact.magnitude > wiggle_threshold)
        {
            if (collision_fx) Fx_Spawner.instance.SpawnFX(collision_fx, pos, direction);
            anim?.SetTrigger(wiggle_hash);
        }
    }

    public void Talk()
    {
        if (idleSounds) idleSounds.enabled = false;
        anim?.SetBool(talking_hash, true);
    }

    public void StopTalk()
    {
        if (idleSounds) idleSounds.enabled = true;
        anim?.SetBool(talking_hash, false);
    }
}
