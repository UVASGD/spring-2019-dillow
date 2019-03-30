using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DillowModel : Follower
{
    DillowBody body;
    Rigidbody bodyrb, rb;
    float rot_speed = 50, turn_speed = 0.25f;
    GameObject spinning_body;

    bool ball;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        spinning_body = rb.gameObject;
        body = transform.parent.GetComponentInChildren<DillowBody>();

        while (!body.ready) {
            yield return null;
        }
        bodyrb = body.rb;
        if (!GetComponentInChildren<Flasher>())
        {
            GetComponentInChildren<SkinnedMeshRenderer>().gameObject.AddComponent<Flasher>();
        }
        body.damager.AddFlasher(GetComponentInChildren<Flasher>());
    }

    void Update()
    {
        if (!bodyrb)
            return;

        spinning_body.transform.localPosition = Vector3.zero;

        if (body.ball)
        {
            ball = true;
            BallUpdate();
        }
        else
        {
            if (ball)
            {
                ball = false;
                spinning_body.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
            }
            DillowUpdate();
        }
    }

    private void BallUpdate()
    {
        if (bodyrb.velocity.magnitude > 1f)
        {
            Vector3 velocity = Vector3.ProjectOnPlane(bodyrb.velocity.normalized, Vector3.up);
            if (velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(velocity), 1f);
            }
        }

        spinning_body.transform.Rotate(Vector3.right * Time.deltaTime * bodyrb.angularVelocity.magnitude * rot_speed);
    }

    private void DillowUpdate()
    {
        if (bodyrb.velocity.magnitude > 2f)
        {
            Vector3 velocity = bodyrb.velocity.normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(velocity), turn_speed);
        }
    }
}
