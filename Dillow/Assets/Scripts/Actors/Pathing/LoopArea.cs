using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopArea : NodeArea
{
    public override Vector3 GetNode(Node current, GameObject seeker)
    {
        return current.GetNext(seeker);
    }
}
