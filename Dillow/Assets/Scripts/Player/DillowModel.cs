﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DillowModel : Follower
{
    DillowBody body;
    Rigidbody bodyrb, rb;
    float rot_speed = 50, turn_speed = 1f;
    GameObject spinning_body;
    bool ball;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        spinning_body = rb.gameObject;
        body = transform.parent.GetComponentInChildren<DillowBody>();
        body.TransformEvent += DillowTransform;

        while (!body.ready) {
            yield return null;
        }
        bodyrb = body.rb;
        body.damager.AddFlasher(GetComponentInChildren<Flasher>());
        DillowTransform(body.ball);
    }

    void Update()
    {
        if (!bodyrb)
            return;

        if (ball)
        {
            BallUpdate();
        }
        else
        {
            DillowUpdate();
        }
    }

    private void BallUpdate()
    {
        if (bodyrb.velocity.magnitude > 1f)
        {
            Vector3 velocity = Vector3.ProjectOnPlane(bodyrb.velocity.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(velocity), turn_speed);
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

    void DillowTransform(bool is_ball)
    {
        ball = is_ball;
        if (!is_ball)
        {
            transform.up = Vector3.up;
            spinning_body.transform.up = Vector3.up;
        }
    }
}
