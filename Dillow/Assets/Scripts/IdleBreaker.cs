using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomly activate a trigger after entering state
/// </summary>
public class IdleBreaker : StateMachineBehaviour
{
    [Tooltip("The time range in which to break the current state. This will occur randomly.")]
    public Vector2 idleRange = new Vector2(2, 6);
    [Tooltip("The name of the trigger in which to activate to break the current state.")]
    public string TriggerName = "Idle";
    float activeTime;
    bool triggered;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        float min = Mathf.Min(idleRange.x, idleRange.y);
        float max = Mathf.Min(idleRange.x, idleRange.y);
        activeTime = Random.Range(min, max);
        triggered = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (triggered) return;
        activeTime -= Time.deltaTime;
        if (activeTime <= 0) {
            animator.SetTrigger(TriggerName);
            triggered = true;
        }
    }
}
