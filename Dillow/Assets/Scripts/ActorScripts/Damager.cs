using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DamagerDel();

public class Damager : MonoBehaviour
{
    public float push_force = 50f, up_force = 50f;
    Rigidbody rb;

    public DamagerDel StunEvent, StunEndEvent, DamageAllowEvent, DamageEndEvent, DeathEvent;
    public float StunTime, DamageTime, EndTime;

    public Color flashColor = Color.red;
    private Color normalColor;
    private MeshRenderer r;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        r = GetComponent<MeshRenderer>();
        normalColor = r.material.color;
    }

    public void Damage(Vector3 dir)
    {
        StartCoroutine(DamageCo());
        StartCoroutine(Flash());
        Knockback(dir);
    }

    IEnumerator DamageCo()
    {
        StunEvent?.Invoke(); // stop moving, no damage, will die if damaged again
        yield return new WaitForSeconds(StunTime);
        StunEndEvent?.Invoke(); // can move again
        yield return new WaitForSeconds(DamageTime);
        DamageAllowEvent?.Invoke(); // can be damaged again
        yield return new WaitForSeconds(EndTime);
        DamageEndEvent?.Invoke(); // won't die if damaged again
    }

    void Knockback(Vector3 dir)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        var force = Vector3.ProjectOnPlane(dir, Vector3.up);
        force = new Vector3(force.x * push_force, 
                            force.y * up_force,
                            force.z * push_force);
        rb.AddForce(force);
    }

    IEnumerator Flash(float flashTime = 0.5f)
    {
        r.material.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        r.material.color = normalColor;
    }
}
