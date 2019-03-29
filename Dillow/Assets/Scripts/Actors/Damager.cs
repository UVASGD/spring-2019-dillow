﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DamagerDel();

public class Damager : MonoBehaviour
{
    public float push_force = 10f, up_force = 15f;
    Rigidbody rb;

    bool can_be_damaged = true, dead, knockbackable = true;
    float knockbackCD = 0.2f;
    public DamagerDel StunEvent, StunEndEvent, DamageAllowEvent, DamageEndEvent, DeathEvent;
    public float StunTime, DamageTime, EndTime;

    public Color flashColor = Color.red;

    List<Flasher> flashers;

    public GameObject damage_fx;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetMainRigidbody();

        HashSet<Renderer> re = new HashSet<Renderer>(GetComponentsInChildren<Renderer>());
        if (GetComponent<Renderer>())
            re.Add(GetComponent<Renderer>());
        flashers = new List<Flasher>();
        foreach (Renderer r in re)
        {
            Flasher f = r.gameObject.AddComponent<Flasher>();
            AddFlasher(f);
        }
    }

    public void AddFlasher(Flasher f)
    {
        flashers.Add(f);
        f.flashColor = flashColor;
        f.flashTime = Mathf.Max(StunTime + DamageTime + EndTime, 0.5f);
    }

    public void Damage(Vector3 dir, Vector3 pos, bool kill = false)
    {
        if (can_be_damaged)
        {
            if (damage_fx)
            {
                Fx_Spawner.instance.SpawnFX(damage_fx, pos, dir);
            }

            StartCoroutine(DamageCo());

            foreach (Flasher f in flashers)
            {
                if (f.gameObject.activeInHierarchy) f.Flash();
            }
        }
        if (!dead && knockbackable)
        {
            dead = kill;
            Knockback(dir);
            KnockbackCo();
        }
    }

    IEnumerator DamageCo()
    {
        can_be_damaged = false;
        StunEvent?.Invoke(); // stop moving, no damage, will die if damaged again
        yield return new WaitForSeconds(StunTime);
        StunEndEvent?.Invoke(); // can move again
        yield return new WaitForSeconds(DamageTime);
        DamageAllowEvent?.Invoke(); // can be damaged again
        yield return new WaitForSeconds(EndTime);
        DamageEndEvent?.Invoke(); // won't die if damaged again
        can_be_damaged = true;
    }

    void Knockback(Vector3 dir)
    {
        var force = Vector3.ProjectOnPlane(dir, Vector3.up);
        force = new Vector3(force.x * push_force, 
                            1f * up_force,
                            force.z * push_force);
        if (rb)
        {
            rb.isKinematic = false;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }

    IEnumerator KnockbackCo()
    {
        knockbackable = false;
        yield return new WaitForSeconds(knockbackCD);
        knockbackable = true;
    }
}
