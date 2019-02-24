using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPatrol : NodeArea
{
    public override Vector3 GetNode(Node current, GameObject seeker)
    {
        return current.GetAdjacent(seeker);
    }
}
