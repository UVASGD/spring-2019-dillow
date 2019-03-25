using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DamagerDel();

public class Damager : MonoBehaviour
{
    public float push_force = 50f, up_force = 50f;
    Rigidbody rb;

    bool can_be_damaged = true;
    public DamagerDel StunEvent, StunEndEvent, DamageAllowEvent, DamageEndEvent, DeathEvent;
    public float StunTime, DamageTime, EndTime;

    public Color flashColor = Color.red;

    List<Flasher> flashers;

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
            flashers.Add(f);
            f.flashColor = flashColor;
            f.flashTime = Mathf.Max(StunTime + DamageTime, 0.5f);
        }
    }

    public void Damage(Vector3 dir)
    {
        if (can_be_damaged)
        {
            StartCoroutine(DamageCo());
            foreach (Flasher f in flashers)
            {
                f.Flash();
            }
            Knockback(dir);
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
                            dir.y * up_force,
                            force.z * push_force);
        if (rb)
        {
            rb.AddForce(force);
        }
    }
}
