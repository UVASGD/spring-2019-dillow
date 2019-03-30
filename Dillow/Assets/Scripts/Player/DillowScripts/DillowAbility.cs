using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DillowBody))]
public abstract class DillowAbility : MonoBehaviour
{
    protected DillowBody body;
    protected Animator fx_anim;

    protected bool action_ready;
    protected bool in_action;

    protected virtual void Start()
    {
        body = GetComponent<DillowBody>();
        body.MoveEvent += OnAction;
        body.EndEvent += OnEnd;
        action_ready = true;
        in_action = false;
    }

    public virtual void OnAction(bool move, Vector3 dir, int jump, int action)
    {
    }

    public virtual void OnEnd()
    {
    }
}
