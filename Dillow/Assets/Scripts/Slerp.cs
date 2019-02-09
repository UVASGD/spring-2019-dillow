using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slerp : MonoBehaviour
{
    public float smooth_speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Face(GameObject target)
    {
        Vector3 direction =  (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(direction), smooth_speed);
    }
}
