using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySource : MonoBehaviour
{
    public float gravity = 9.8f;

    // Start is called before the first frame update
    public Vector3 GetGravity(Vector3 pos)
    {
        return (transform.position - pos) * gravity;
    }
}
