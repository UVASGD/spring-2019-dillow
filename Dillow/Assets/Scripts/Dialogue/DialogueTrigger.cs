using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(SphereCollider))]
public class DialogueTrigger : MonoBehaviour
{
	SphereCollider sphere;

    // Start is called before the first frame update
    void Start()
    {
		sphere = GetComponent<SphereCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider other) {
		
	}
}
