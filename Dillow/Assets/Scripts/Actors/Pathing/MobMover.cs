using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobMover : MonoBehaviour
{
    [HideInInspector] public bool walk;
    bool walking;

    public NodeArea nodeArea;
    [HideInInspector] public NavMeshAgent agent;
    float wanderTime = 20f, min_wander_time = 5f;  // Time before NPC chooses another random node seconds

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!nodeArea)
            return;
        if (!walk && agent.isOnNavMesh)
        {
            agent.ResetPath();
        }
        if (walk && !walking)
        {
            StartCoroutine(Move());
        }
        
    }

    public void MoveToTarget(Vector3 target)
    {
        StopAllCoroutines();
        agent.SetDestination(target);
    }

    IEnumerator Move()
    {
        walking = true;
        agent.SetDestination(nodeArea.GetRandomNode(agent.gameObject));
        yield return new WaitForSeconds(Random.Range(min_wander_time, wanderTime));
        walking = false;
    }
}
