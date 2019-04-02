using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    ParticleSystem interactPrompt;

    public Flowchart chart;

    // Start is called before the first frame update
    void Start()
    {
		GetComponent<Collider>().isTrigger = true;
        interactPrompt = GetComponent<ParticleSystem>();

        if (interactPrompt != null && ! interactPrompt.isStopped) {
            interactPrompt.Stop();
        }
    }

    private void ShowPrompt() {
        if (interactPrompt != null) {
            interactPrompt.Play();
        }
    }

    private void HidePrompt() {
        if (interactPrompt != null) {
            interactPrompt.Stop();
            interactPrompt.Clear();
        }
    }

    private void OnTriggerEnter(Collider other) {
        TagHandler t = other.GetComponent<TagHandler>();

        if (t && t.HasTag(Tag.Player)) {
            ShowPrompt();
        }
    }

    private void OnTriggerStay(Collider other) {
        TagHandler t = other.GetComponent<TagHandler>();

        if ((t && t.HasTag(Tag.Player)) && DillowController.interact == 2)
        { 
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
            DillowController.instance.can_input = false;
            DillowController.instance.body.rb.isKinematic = true;
        }
    }

    void EndTrigger() {
        if (chart != null) {
            chart.ExecuteBlock("EndTrigger");
            FinishDialogue();   
        }
    }

    public void FinishDialogue() {
        DillowController.instance.can_input = true;
        DillowController.instance.body.rb.isKinematic = false;
    }
}
