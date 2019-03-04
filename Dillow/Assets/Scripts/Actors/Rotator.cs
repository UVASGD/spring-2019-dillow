﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float smooth_speed = 0.1f;

    public void Face(GameObject target, bool lockX = true, bool lockY = true, bool lockZ = true)
    {
        Vector3 direction =  (target.transform.position - transform.position).normalized;
        Face(direction, lockZ, lockY, lockZ);
    }

    public void Face(Vector3 dir, bool lockX = true, bool lockY = true, bool lockZ = true)
    {

        float x = transform.rotation.x;
        float y = transform.rotation.y;
        float z = transform.rotation.z;

        StopAllCoroutines();
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(dir), smooth_speed);

        transform.rotation = new Quaternion((lockX) ? x : transform.rotation.x, 
                                            (lockY) ? y : transform.rotation.y, 
                                            (lockZ) ? z : transform.rotation.z,
                                            transform.rotation.w);
    }

    public void TurnTo(Vector3 direction, bool lockX = true, bool lockY = true, bool lockZ = true)
    {
        StopAllCoroutines();
        StartCoroutine(TurnToCo(direction, lockX, lockY, lockZ));
    }

    private IEnumerator TurnToCo( Vector3 direction, bool lockX = true, bool lockY = true, bool lockZ = true)
    {
        while( Vector3.Angle(transform.forward, direction) != 0 )
        {
            Face(direction, lockX, lockY, lockZ);
            yield return null;
        }
    }
}
