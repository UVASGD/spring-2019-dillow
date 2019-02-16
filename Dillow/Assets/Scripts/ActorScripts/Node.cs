using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer

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

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            transform.position = hit.point;
        }
    }

    public Node GetAdjacent()
    {
        return adjacents[Random.Range(0, adjacents.Count)];
    }

    public Node GetNext()
    {
        return adjacents[0];
    }
}
