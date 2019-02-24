using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float smooth_speed = 0.1f;

    public void Face(GameObject target)
    {
        Vector3 direction =  (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(direction), smooth_speed);
    }
}
