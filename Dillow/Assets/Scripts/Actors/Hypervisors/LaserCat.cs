using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCat : Mob
{

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

    protected override void Die(Vector3 dir)
    {
        if (!dead)
        {
            OnCalm(false);
            noticer.NoticeEvent -= OnNotice;
            base.Die(dir);
        }
    }

    void OnNotice(TagHandler t)
    {
        if (t.HasTag(Tag.Player))
        {
            target = t.gameObject;
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
        gun.Activate();
        if (current_behavior != null)
            StopCoroutine(current_behavior);
        current_behavior = StartCoroutine(Aggro());
    }

    void OnCalm(bool alive = true)
    {
        target = null;
        gun.Activate(false);
        if (alive)
            noticer.Blink();
        if (current_behavior != null)
            StopCoroutine(current_behavior);
        current_behavior = StartCoroutine(Idle());
    }

    IEnumerator Aggro()
    {
        while (target)
        {
            rotator.Face(target, lockY:false);
            gun.Aim(target);
            yield return null;
        }
        OnCalm();
    }

    IEnumerator Idle()
    {
        yield return null;
    }

    IEnumerator Move()
    {
        yield return null;
    }
}