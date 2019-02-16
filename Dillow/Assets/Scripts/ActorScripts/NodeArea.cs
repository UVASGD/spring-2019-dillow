using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeArea : MonoBehaviour
{
    public Node[] nodeList;

    public virtual Node GetNode(Node current)
    {
        return current;
    }

    public Node GetRandomNode()
    {
        return nodeList[Random.Range(0, nodeList.Length - 1)];
    }
}
