using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Node : MonoBehaviour
{

    public bool manual_adjacents = false;
    public float radius;
    public int adj_count;
    public List<Node> adjacents;
    public GameObject obj;

    private NodeArea nodeArea;

    // Start is called before the first frame update
    void Start()
    {
        if (manual_adjacents)
            return;

        int curr_adj_count = 0;
        foreach (Node adj in nodeArea.nodeList)
        {
            if (adj != this)
            {
                if (Vector3.Distance(adj.transform.position, transform.position) < radius)
                {
                    adjacents.Add(adj);
                    curr_adj_count++;
                }
            }

            if (curr_adj_count >= adj_count)
                break;
        }

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 10, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
    }

    public Vector3 RandomLocation(Vector3 position)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += position;
        NavMeshHit hit;
        Vector3 finalPosition = position;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public Vector3 GetGroundPoint(Vector3 position) {
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit))
        {
            position = hit.point;
        }

        return position;
    }

    public Vector3 GetAdjacent(GameObject _obj)
    {
        Node ret_node = adjacents[Random.Range(0, adjacents.Count)];
        return ret_node.GoTo(_obj);
    }

    public Vector3 GetNext(GameObject _obj)
    {
        return adjacents[0].GoTo(_obj);
    }

    public Vector3 GoTo(GameObject _obj)
    {
        Vector3 target = (!obj) ? transform.position : RandomLocation(transform.position);
        obj = _obj;
        return target;
    }
}
