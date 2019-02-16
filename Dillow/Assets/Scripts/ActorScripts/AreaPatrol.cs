using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPatrol : NodeArea
{
    public override Node GetNode(Node current)
    {
        return current.GetAdjacent();
    }
}
