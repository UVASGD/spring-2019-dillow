using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Reticle : MonoBehaviour
{
    RectTransform rect;
    Vector2 velocity = Vector2.zero;
    float smooth_time = 0.4f;

    CinemachineFreeLook main;

    // Start is called before the first frame update
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        main = GameObject.FindGameObjectWithTag("PlayerCam").GetComponent<CinemachineFreeLook>();
    }

    public void SetReticle(Vector2 screen_coords)
    {
        Vector2 move_to = Vector2.SmoothDamp(rect.position, screen_coords, ref velocity, smooth_time);
        rect.position = screen_coords;
    }

    public void Deactivate()
    {
        //play the deactivate sound
        if (main)
        {
            //main.GetRig(1).LookAt = BallController.body.transform;
        }
        gameObject.SetActive(false);
    }

    public void Activate(GameObject locked)
    {
        //main.GetRig(1).LookAt = locked.transform;
        BallController.body.lock_enemy = locked;
        //play the activate sound
        //do the twirl
    }
}
