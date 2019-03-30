using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDash : DillowAttackAbility
{
    public GameObject dash_fx;
    bool locked;
    float dash_multiplier = 1.5f;

    public override void OnAction(bool move, Vector3 dir, int jump, int action)
    {
		if (action == 2 && move && action_ready && body.CheckPriority(2))
        {
            body.can_be_damaged = false;
            attack_dir = dir;

            if (body.lock_enemy)
            {
                locked = true;
                Vector3 lock_dir = (body.lock_enemy.transform.position - body.transform.position).normalized;
                attack_dir = (Vector3.Angle(attack_dir, lock_dir) < 100f)
                    ? lock_dir : attack_dir;
            }
            else
            {
                locked = false;
            }

            StartCoroutine(Action());
        }
    }

    protected override void StartAction()
    {
        if (locked)
        {
            body.rb.velocity = attack_speed * attack_dir * dash_multiplier;
        }
        else
        {
            body.rb.velocity += attack_dir * attack_speed;
        }
        intensity = 1f;
        fx_anim?.SetTrigger("Start");
        Fx_Spawner.instance.SpawnFX(dash_fx, transform.position, Vector3.up);
    }

    protected override void EndAction()
    {
        body.can_be_damaged = true;
        intensity = 0f;
        fx_anim?.SetTrigger("Stop");
        //body.collision_state.RemoveState(CollisionState.attacking);
        StartCoroutine(Recharge());
    }

    IEnumerator AddDashing()
    {
        body.tagH.Add(Tag.Dashing);
        yield return new WaitForSeconds(max_action_time + max_recharge_time);
        body.tagH.RemoveAll(Tag.Dashing);
    }
}
