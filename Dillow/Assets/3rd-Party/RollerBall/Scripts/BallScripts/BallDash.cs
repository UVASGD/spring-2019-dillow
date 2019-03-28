﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDash : BallAttackAbility
{
    public GameObject dash_fx;

    public override void OnAction(bool move, Vector3 dir, int jump, int action)
    {
		if (action == 2 && move && action_ready && body.CheckPriority(2))
        {
            attack_dir = dir;

            if (body.lock_enemy)
            {
                Vector3 lock_dir = (body.lock_enemy.transform.position - body.transform.position).normalized;
                /*
                attack_dir = (Vector3.Angle(attack_dir, lock_dir) < 90f)
                    ? lock_dir : attack_dir;
                    */
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
        Fx_Spawner.instance.SpawnFX(dash_fx, transform.position, Vector3.up);
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
