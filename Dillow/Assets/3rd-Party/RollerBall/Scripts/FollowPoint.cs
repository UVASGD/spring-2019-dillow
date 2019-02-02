using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour {

    Transform target;

    Vector3 velocity;

	// Use this for initialization
	void Start () {
        target = transform.parent;
        transform.SetParent(null);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, 0.02f);
	}
}
