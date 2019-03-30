using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobMover : MonoBehaviour
{
    public bool walk;

    [HideInInspector] public NavMeshAgent agent;
    public NodeArea nodeArea;
    public float wanderTime;  // Time before NPC chooses another random node seconds
    float nextWanderTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!walk && agent.isOnNavMesh)
        {
            agent.ResetPath();
        }
        if (walk && Time.time > nextWanderTime + Random.Range(-3f,3f) && nodeArea)
        {
            nextWanderTime = Time.time + wanderTime;
            agent.SetDestination(nodeArea.GetRandomNode(agent.gameObject));
        }
        
    }

    public void MoveToTarget(Vector3 target)
    {
        agent.SetDestination(target);
    }
}
