using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCat : Mob
{

    LaserGun gun;
    
    Rotator rotator;
    MobMover mover;

    float aim_timer; //current timer
    float aim_max = 1.75f; //time it takes to fire

    int speed_hash, idle_hash, aim_hash, blasting_hash;
    float speed_rate, anim_speed_multiplier = 2f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        gun = GetComponentInChildren<LaserGun>();
        gun.range = noticer.range;
        aim_timer = aim_max;
        rotator = GetComponent<Rotator>();
        mover = GetComponent<MobMover>();

        noticer.NoticeEvent += OnNotice;
        noticer.UnnoticeEvent += OnUnnotice;

        speed_hash = Animator.StringToHash("Speed");
        idle_hash = Animator.StringToHash("Idle");
        aim_hash = Animator.StringToHash("Aim");
        blasting_hash = Animator.StringToHash("Blasting");
    }

	protected override void OnNotice (TagHandler t)
    {
		base.OnNotice(t);

        if (t.HasTag(Tag.Player)) {
            target = t.gameObject;
            gun.Activate(true);
            current_behavior = Aggro;
        }
    }

    protected override void OnUnnotice(TagHandler t)
    {
		base.OnUnnotice(t);

		if (t.HasTag(Tag.Player))
        {
            target = null;
            Calm();
        }
    }

    protected override void Act()
    {
        base.Act();

        speed_rate = mover.agent.velocity.magnitude / mover.agent.speed;
        speed_rate *= anim_speed_multiplier;
        anim.SetFloat(speed_hash, speed_rate);
    }

    void Idle()
    {
        mover.walk = true;
    }

    void Aggro()
    {
        //print("Aggro");
        mover.walk = false;
        //mover.MoveToTarget(target.transform.position);
        if (target)
        {
            if (aim_timer > 0)
            {
                rotator.Face(target, lockY: false);
                if (!gun.Aim(target))
                {
                    Calm();
                }
                else
                {
                    anim.SetBool(aim_hash, true);
                    aim_timer -= Time.deltaTime;
                    gun.Charge(aim_timer);
                }
            }
            else
            {
                anim.SetTrigger(blasting_hash);
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
        anim.SetBool(aim_hash, false);
        aim_timer = aim_max;
        target = null;
        gun.Activate(false);
        noticer.Blink();
        current_behavior = Idle;
    }

    public override void Die()
    {
        if (!dead)
        {
            base.Die();
            target = null;
            gun.Activate(false);
            noticer.NoticeEvent -= OnNotice;
            noticer.UnnoticeEvent -= OnUnnotice;
        }
    }
}