using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopArea : NodeArea
{
    public override Node GetNode(Node current)
    {
        return current.GetNext();
    }
}
