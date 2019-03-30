using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Follower : MonoBehaviour
{
    PositionConstraint pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PositionConstraint>();
        ConstraintSource cs = new ConstraintSource();
        cs.sourceTransform = transform.parent.GetComponentInChildren<DillowBody>().transform;
        cs.weight = 1;
        if (pc.sourceCount == 0)
            pc.AddSource(cs);
        else
        {
            pc.SetSource(0, cs);
        }
    }
}
