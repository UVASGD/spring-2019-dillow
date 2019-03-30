using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DillowController : MonoBehaviour
{
    public DillowBody body;
    public static DillowController instance;

    private Vector3 move;
    // the world-relative desired move direction, calculated from the camForward and user input.

    private Transform cam; // A reference to the main camera in the scenes transform
    private Vector3 camForward; // The current forward direction of the camera
    private int jump; // whether the jump button is currently pressed
    private int action;
    public static int interact;

    public bool can_input = true;

    private Locker locker;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Set up the reference.
        body = GetComponent<DillowBody>();
        locker = transform.parent.GetComponentInChildren<Locker>();

        // get the transform of the main camera
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
            // we use world-relative controls in this case, which may not be what the user wants, but hey, we warned them!
        }

        FadeController.FadeInStartedEvent += delegate { can_input = false; body.can_be_damaged = false; };
        FadeController.FadeOutStartedEvent += delegate { can_input = false; body.can_be_damaged = false; };
        FadeController.FadeInCompletedEvent += delegate { can_input = true; body.can_be_damaged = true; };
    }

    private void Update()
    {
        // Get the axis and jump input.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        GetButton(ref jump, "Jump");
        GetButton(ref action, "Action");
        GetButton(ref interact, "Interact");

        // calculate move direction
        if (cam != null)
        {
            // calculate camera relative direction to move:
            camForward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up); //Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            move = (v * camForward + h * cam.right).normalized;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            move = (v * Vector3.forward + h * Vector3.right).normalized;
        }

        if (Input.GetButtonDown("LockOn"))
        {
            locker.Lock(!locker.locked);
        }
        else if (Input.GetButtonDown("Swap"))
        {
            locker.Lock(true);
        }
    }

    private void FixedUpdate()
    {
        if (can_input)
            body.Input(move.magnitude > 0f, move, jump, action);
    }

    private void GetButton(ref int button, string axisName)
    {
        button = Input.GetAxis(axisName) > 0f ? 1 : 0;

        if (button == 0)
        {
            button = Input.GetButtonUp(axisName) ? -1 : 0;
        }
        else if (button == 1)
        {
            button = Input.GetButtonDown(axisName) ? 2 : 1;
        }
    }
}
