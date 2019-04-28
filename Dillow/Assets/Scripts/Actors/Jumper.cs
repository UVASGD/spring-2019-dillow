using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : Mob
{
    [Header("JUMPER")]
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

    //public GameObject target;

    Rotator rotator;
    TagHandler tagHandler;

    short stage = 4;  // Either 0 for leaping, 1 for tracking, 2 for waiting, and 3 for dropping. 4 for waiting

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        jumpDelayTime = jumpDelay;
        rotator = GetComponent<Rotator>();
        tagHandler = GetComponent<TagHandler>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(active && target != null && !tagH.HasTag(Tag.Dead))
        {
            //print(tagHandler.HasTag(Tag.Dead));
            // Go from waiting to leaping (stage = 4 -> 0)
            if(stage == 4 && Time.time > jumpDelayTime)
            {
                stage = 0;
                jumpStartTime = Time.time;

                ground = transform.position;
                apex = new Vector3(ground.x, ground.y + height, ground.z);

                rb.useGravity = false;
            }
            // Currently leaping
            else if(stage == 0)
            {
                //print("Leaping");

                if(SlerpTo(apex, ground, jumpStartTime, upSpeed))
                {
                    stage = 1;
                    trackEndTime = Time.time + trackingTime;
                }
            }
            // Currently Tracking
            else if(stage == 1)
            {
                //print("Tracking");
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
                //print("Waiting to drop");
                if(Time.time > dropTime)
                {
                    stage = 3;

                    tagH.Add(Tag.SuperDamage);
                    ground = target.transform.position;
                    apex = transform.position;
                    rb.useGravity = true;
                }
            }
            // Currently Dropping
            else if(stage == 3)
            {
                //print("Dropping");  

                if (SlerpTo(ground, apex, dropTime, downSpeed))
                {
                    
                    stage = 4;
                    jumpDelayTime = Time.time + jumpDelay;
                }

                
            }
            else
            {
                if (tagH.HasTag(Tag.SuperDamage)) tagH.Remove(Tag.SuperDamage);
                //print("Idle");
                rotator.TurnTo(target, lockY:false);
            }

        }
        else
        {
            if (tagH.HasTag(Tag.SuperDamage)) tagH.Remove(Tag.SuperDamage);
            rb.useGravity = true;
            stage = 4;
            jumpDelayTime = jumpDelay + Time.time;
        }

        if(tagH.HasTag(Tag.Dead))
        {
            print("Dead");
        }
    }

    protected override void OnNotice(TagHandler t)
    {
        base.OnNotice(t);
        //print("Noticed!");
        if (t.HasTag(Tag.Player))
        {
            active = true;
            target = t.gameObject;
        }
    }

    protected override void OnUnnotice(TagHandler t)
    {
        base.OnUnnotice(t);
        //print("Unnoticed!");
        if (t.HasTag(Tag.Player))
        {
            active = false;
            target = null;
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
        Vector3 aboveTarget = target.transform.position + new Vector3(0, height, 0);
        transform.position = Vector3.MoveTowards(transform.position, aboveTarget, horizSpeed);
    }
}
