using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(SphereCollider))]
public class DialogueTrigger : MonoBehaviour
{
	SphereCollider sphere;

    public Flowchart chart;

    // Start is called before the first frame update
    void Start()
    {
		sphere = GetComponent<SphereCollider>();
        sphere.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) {
        GameObject go = other.gameObject;

        if (go.CompareTag("Player") && BallController.interact == 2) {
            DoTrigger();
        }
    }

    private void OnTriggerStay(Collider other) {
        GameObject go = other.gameObject;

        if (go.CompareTag("Player") && BallController.interact == 2) {
            DoTrigger();
        }
    }

    private void OnTriggerExit(Collider other) {

        GameObject go = other.gameObject;

        if (go.CompareTag("Player")) {
            chart.ExecuteBlock("EndTrigger");
        }
    }

    void DoTrigger() {

        if (chart != null) {
            chart.ExecuteBlock("Trigger");
        }
    }
}
