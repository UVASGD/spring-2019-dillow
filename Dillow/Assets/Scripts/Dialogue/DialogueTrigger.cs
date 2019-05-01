using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    InteractableBody body;
    ParticleSystem interactPrompt;
    DialogSpin dialogSpin;

    public Flowchart chart;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetAnyComponent<InteractableBody>(in_children:false, neighbor_depth:1);

        Collider dialog_trigger = GetComponent<Collider>();
        dialog_trigger.isTrigger = true;

        interactPrompt = GetComponent<ParticleSystem>();
        if (interactPrompt)
        {
            var sh = interactPrompt.shape;
            sh.radius = dialog_trigger.bounds.extents.x;
        }

        dialogSpin = GetComponent<DialogSpin>();

        HidePrompt();
    }

    private void ShowPrompt() {
        if (dialogSpin) dialogSpin.enabled = true;
        if (interactPrompt) interactPrompt?.Play();
    }

    private void HidePrompt() {
        if (dialogSpin) dialogSpin.enabled = false;
        if (interactPrompt) interactPrompt?.Stop();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.HasTag(Tag.Player)) {
            ShowPrompt();
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.HasTag(Tag.Player) && DillowController.interact == 2) { 
            DoTrigger();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.HasTag(Tag.Player)) {
            EndTrigger();
            HidePrompt();
        }
    }

    public void DoTrigger() {
        if (chart != null) {
            if (body) body.Talk();
            chart.ExecuteBlock("Trigger");
            DillowController.instance.body.TransformToDillow();
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
        if (body) body.StopTalk();
        DillowController.instance.can_input = true;
        DillowController.instance.body.rb.isKinematic = false;
        DillowController.instance.body.rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }
}
