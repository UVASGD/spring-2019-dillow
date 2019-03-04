using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCat : Mob
{
    public List<Rigidbody> arm_parts;

    LaserGun gun;

    Noticer noticer;
    Rotator rotator;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        gun = GetComponentInChildren<LaserGun>();
        noticer = GetComponentInChildren<Noticer>();
        rotator = GetComponent<Rotator>();

        noticer.NoticeEvent += OnNotice;
        noticer.UnnoticeEvent += OnUnnotice;
    }

    void SlackArm(bool slack = true)
    {
        foreach (Rigidbody r in arm_parts)
        {
            r.isKinematic = !slack;
        }
    }

    void OnNotice(TagHandler t)
    {
        if (t.HasTag(Tag.Player))
        {
            OnAggro();
        }
    }

    void OnUnnotice(TagHandler t)
    {
        if (t.HasTag(Tag.Player))
        {
            OnCalm();
        }
    }

    void OnAggro()
    {
        //gun.Activate();
        SlackArm();
        current_behavior = Aggro;
    }

    void OnCalm()
    {
        //gun.Activate(false);
        SlackArm(false);
        noticer.Blink();
        current_behavior = Idle;
    }

    void Aggro()
    {

    }

    void Idle()
    {

    }

    void Move()
    {

    }
}