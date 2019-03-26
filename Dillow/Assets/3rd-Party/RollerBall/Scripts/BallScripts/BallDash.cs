using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDash : BallAttackAbility
{

    public override void OnAction(bool move, Vector3 dir, int jump, int action)
    {
        if (action == 2 && move && action_ready && body.CheckPriority(2))
        {
            attack_dir = dir;
                //(Vector3.Angle(dir, body.rb.velocity.normalized) < 70f)
                //? body.rb.velocity.normalized : dir;

            if (body.lock_enemy)
            {
                Vector3 lock_dir = (body.transform.position - body.lock_enemy.transform.position).normalized;
                attack_dir = (Vector3.Angle(attack_dir, lock_dir) < 50f)
                    ? lock_dir : attack_dir;
                attack_dir = lock_dir;
            }

            StartCoroutine(Action());
        }
    }

    protected override void StartAction()
    {
        body.rb.velocity += attack_dir * attack_speed;
        intensity = 1f;
        fx_anim?.SetTrigger("Start");
        //body.collision_state.AddState(CollisionState.attacking);
    }

    protected override void EndAction()
    {
        intensity = 0f;
        fx_anim?.SetTrigger("Stop");
        //body.collision_state.RemoveState(CollisionState.attacking);
        StartCoroutine(Recharge());
    }
}
