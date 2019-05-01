﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public bool active;

    public float horizSpeed;
    public float upSpeed;
    public float downSpeed;
    public float dropDelay;
    public float jumpDelay;
    public float trackingTime;

    float jumpDelayTime;
    float jumpStartTime;

    float trackEndTime;

    float dropTime;

    public float height;
    Vector3 ground;  // Ground when we start to leap
    Vector3 apex;  // Top point of leap. SHould be Ground + height in Y

    public Transform target;

    short stage = 4;  // Either 0 for leaping, 1 for tracking, 2 for waiting, and 3 for dropping. 4 for waiting

    // Start is called before the first frame update
    void Start()
    {
        jumpDelayTime = Time.time + jumpDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            // Go from waiting to leaping (stage = 4 -> 0)
            if(stage == 4 && Time.time > jumpDelayTime)
            {
                stage = 0;
                jumpStartTime = Time.time;

                ground = transform.position;
                apex = new Vector3(ground.x, ground.y + height, ground.z);

                GetComponent<Rigidbody>().useGravity = false;
            }
            // Currently leaping
            else if(stage == 0)
            {
                

                if(SlerpTo(apex, ground, jumpStartTime, upSpeed))
                {
                    stage = 1;
                    trackEndTime = Time.time + trackingTime;
                }
            }
            // Currently Tracking
            else if(stage == 1)
            {
                TrackTarget();

                if(Time.time > trackEndTime)
                {
                    stage = 2;
                    dropTime = Time.time + dropDelay;
                }
            }
            // Currently Waiting to drop
            else if(stage == 2)
            {
                if(Time.time > dropTime)
                {
                    stage = 3;

                    ground = target.position;
                    apex = transform.position;
                    GetComponent<Rigidbody>().useGravity = true;
                }
            }
            // Currently Dropping
            else if(stage == 3)
            {
                
                

                if (SlerpTo(ground, apex, dropTime, downSpeed))
                {
                    stage = 4;
                    jumpDelayTime = Time.time + jumpDelay;
                }
            }

        }
    }

    // Stage 0 helper
    bool SlerpTo(Vector3 target, Vector3 start, float startTime, float vertiSpeed)
    {
        float distCovered = (Time.time - startTime) * vertiSpeed;
        float fracJourney = distCovered / Vector3.Distance(start, target);
        transform.position = Vector3.Slerp(start, target, fracJourney);

        if(fracJourney >= 1)
            return true; 

        return false;
    }

    void TrackTarget()
    {
        Vector3 aboveTarget = new Vector3(target.position.x, target.position.y + height, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, aboveTarget, horizSpeed);
    }
}
