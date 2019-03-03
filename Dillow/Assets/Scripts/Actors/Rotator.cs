using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float smooth_speed = 0.1f;

    public void Face(GameObject target)
    {
        StopAllCoroutines();
        Vector3 direction =  (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(direction), smooth_speed);
    }

    public void Face(Vector3 dir)
    {
        StopAllCoroutines();
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(dir), smooth_speed);
    }

    public void TurnTo(Vector3 ogDirection)
    {
        StartCoroutine(turnOriginal(ogDirection));
    }

    private IEnumerator turnOriginal( Vector3 ogDirection )
    {
        while( Vector3.Angle(transform.forward,ogDirection) != 0 )
        {
            Face(ogDirection);
            yield return null;
        }
    }
}
