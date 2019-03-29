using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(SphereCollider))]
public class DialogueTrigger : MonoBehaviour
{
	SphereCollider sphere;
    ParticleSystem talkPrompt;

    public Flowchart chart;

    // Start is called before the first frame update
    void Start()
    {
		sphere = GetComponent<SphereCollider>();
        sphere.isTrigger = true;

        talkPrompt = GetComponent<ParticleSystem>();
        Debug.Log("talkPrompt: " + talkPrompt.ToString());
        if (talkPrompt != null && ! talkPrompt.isStopped) {
            talkPrompt.Stop();
        }
    }

    private void ShowPrompt() {
        if (talkPrompt != null) {
            talkPrompt.Play();
        }
    }

    private void HidePrompt() {
        if (talkPrompt != null) {
            talkPrompt.Stop();
            talkPrompt.Clear();
        }
    }

    private void OnTriggerEnter(Collider other) {
        GameObject go = other.gameObject;

        if (go.CompareTag("Player") && BallController.interact == 2)
        { // callcontroller.interact == 2 checks whether 'e' has been pressed
            DoTrigger();
        }
        else if (go.CompareTag("Player")) {
            ShowPrompt();
        }
    }

    private void OnTriggerStay(Collider other) {
        GameObject go = other.gameObject;

        if (go.CompareTag("Player") && BallController.interact == 2)
        { // callcontroller.interact == 2 checks whether 'e' has been pressed
            DoTrigger();
        }
    }

    private void OnTriggerExit(Collider other) {

        GameObject go = other.gameObject;

        if (go.CompareTag("Player")) {
            EndTrigger();
            HidePrompt();
        }
    }

    void DoTrigger() {
        if (chart != null) {
            chart.ExecuteBlock("Trigger");
            //string chatType = chart.GetStringVariable("type"); 
        }
    }

    void EndTrigger() {
        if (chart != null) {
            chart.ExecuteBlock("EndTrigger");
        }
    }
}
