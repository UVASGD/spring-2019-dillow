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
    int fire_amount = 5;
    float fire_interval = 0.25f;
    bool firing = false;

    bool can_act = true; // lock for when game is in transition state

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


        FadeController.FadeInStartedEvent += delegate { can_act = false; };
        FadeController.FadeOutStartedEvent += delegate { can_act = false; };
        FadeController.FadeInCompletedEvent += delegate { can_act = true; };
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
        if (!can_act) return;
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
        if (!can_act) return;
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
            else if (!firing)
            {
                StartCoroutine(Fire());
            }
        }
        else
        {
            Calm();
        }
    }

    void Calm()
    {
        //print("I calmed my tits");
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
            StopAllCoroutines();
            base.Die();
            target = null;
            gun.Activate(false);
            noticer.NoticeEvent -= OnNotice;
            noticer.UnnoticeEvent -= OnUnnotice;
        }
    }

    IEnumerator Fire()
    {
        firing = true;
        int count = fire_amount;
        while (count > 0)
        {
            anim.SetTrigger(blasting_hash);
            gun.Fire(target);
            count--;
            yield return new WaitForSeconds(fire_interval);
        }
        firing = false;
        aim_timer = aim_max;
    } 
}