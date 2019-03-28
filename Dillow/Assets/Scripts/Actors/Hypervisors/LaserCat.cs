using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCat : Mob
{

    LaserGun gun;

    Noticer noticer;
    Rotator rotator;

    float aim_timer; //current timer
    float aim_max = 1.75f; //time it takes to fire


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        gun = GetComponentInChildren<LaserGun>();
        aim_timer = aim_max;
        noticer = GetComponentInChildren<Noticer>();
        rotator = GetComponent<Rotator>();

        noticer.NoticeEvent += OnNotice;
        noticer.UnnoticeEvent += OnUnnotice;
    }

    void OnNotice(TagHandler t)
    {
        if (t.HasTag(Tag.Player)) {
            target = t.gameObject;
            gun.Activate(true);
            current_behavior = Aggro;
        }
    }

    void OnUnnotice(TagHandler t)
    {
        if (t.HasTag(Tag.Player))
        {
            target = null;
            Calm();
        }
    }

    void Idle()
    {
    }

    void Aggro()
    {
        if (target)
        {
            if (aim_timer > 0)
            {
                rotator.Face(target, lockY: false);
                if (!gun.Aim(target)) {
                    Calm();
                }
                aim_timer -= Time.deltaTime;
            }
            else
            {
                gun.Fire(target);
                aim_timer = aim_max;
            }
        }
        else
        {
            Calm();
        }
    }

    void Calm()
    {
        aim_timer = aim_max;
        target = null;
        gun.Activate(false);
        noticer.Blink();
        current_behavior = Idle;
    }

    protected override void Die(Vector3 dir, Vector3 pos)
    {
        if (!dead)
        {
            current_behavior = null;
            target = null;
            gun.Activate(false);
            noticer.NoticeEvent -= OnNotice;
            noticer.UnnoticeEvent -= OnUnnotice;
            base.Die(dir, pos);
        }
    }
}