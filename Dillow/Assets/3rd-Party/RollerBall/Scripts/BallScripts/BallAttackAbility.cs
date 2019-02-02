using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttackAbility : BallAbility
{
    [Header("Action Duration")]
    public float max_action_time;
    protected float action_timer;

    [Header("Recharge Duration")]
    public float max_recharge_time;

    public float attack_speed;
    protected Vector3 attack_dir;

    protected float intensity;

    protected virtual IEnumerator Action()
    {
        action_ready = false;
        in_action = true;
        action_timer = max_action_time;
        StartAction();

        while (action_timer > 0f)
        {
            MidAction();
            action_timer -= Time.deltaTime;
            yield return null;
        }

        EndAction();
    }

    protected virtual void StartAction()
    { 
    }

    protected virtual void MidAction()
    {
    }

    protected virtual void EndAction()
    {
    }

    protected virtual IEnumerator Recharge()
    {
        action_ready = false;
        in_action = false;
        yield return new WaitForSeconds(max_recharge_time);
        action_ready = true;
    }

    public override void OnEnd()
    {
        EndAction();
        StartCoroutine(Recharge());
    }
}
