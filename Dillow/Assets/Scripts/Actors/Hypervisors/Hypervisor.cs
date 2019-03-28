using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hypervisor : Mob
{
    Rotator rotator;
    Noticer noticer;
    
    public Vector3 originalRotation;

    public GameObject SnowThrower;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rotator = GetComponent<Rotator>();
        noticer = GetComponentInChildren<Noticer>();
        noticer.NoticeEvent += OnNotice;
        noticer.UnnoticeEvent += OnUnnotice;

        originalRotation = transform.forward;
        originalRotation.z = Random.Range(0, 360);

        rotator.Face(originalRotation, false, false, false);

        SnowThrower.SetActive(false);
    }

    public void OnNotice(TagHandler tagHandler)
    {
        if (tagHandler.HasTag(Tag.Player))
        {
            target = tagHandler.gameObject;
            SnowThrower.SetActive(true);
            current_behavior = Aggro;
        }
    }

    public void OnUnnotice(TagHandler tagHandler)
    {
        if (tagHandler.HasTag(Tag.Player))
        {
            current_behavior = null;
            Calm();
        }
    }

    void Aggro()
    {
        if (target)
        {
            rotator.Face(target, false, false, false);
        }
        else
        {
            current_behavior = null;
            Calm();
        }
    }

    void Calm()
    {
        rotator.TurnTo(originalRotation, false, false, false);
        SnowThrower.SetActive(false);
    }

    protected override void Die(Vector3 dir, Vector3 pos)
    {
        SnowThrower.SetActive(false);
        base.Die(dir, pos);
    }
}
