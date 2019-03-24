using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeArea : MonoBehaviour
{
    public List<Node> nodeList = new List<Node>();

    private void Awake()
    {
        foreach(Transform t in transform)
        {
            Node n = t.GetComponent<Node>();
            nodeList.Add(n);
            n.nodeArea = this;
        }
    }

    public virtual Vector3 GetNode(Node current, GameObject seeker)
    {
        return current.transform.position;
    }

    public Vector3 GetRandomNode(GameObject seeker)
    {
        return nodeList[Random.Range(0, nodeList.Count)].GoTo(seeker);
    }
}
