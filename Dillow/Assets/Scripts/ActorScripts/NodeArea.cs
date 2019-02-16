using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeArea : MonoBehaviour
{
    public Node[] nodeList;

    public virtual Vector3 GetNode(Node current, GameObject seeker)
    {
        return current.transform.position;
    }

    public Vector3 GetRandomNode(GameObject seeker)
    {
        return nodeList[Random.Range(0, nodeList.Length)].GoTo(seeker);
    }
}
