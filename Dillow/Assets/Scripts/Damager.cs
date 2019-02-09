using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DamagerDel();

public class Damager : MonoBehaviour
{

    public DamagerDel StunEvent, StunEndEvent, DamageAllowEvent, DamageEndEvent;
    public float StunTime, DamageTime, EndTime;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage() 
    {
        StunEvent?.Invoke();
        StartCoroutine(DamageCo());
        
    }

    IEnumerator DamageCo()
    {
        
        yield return new WaitForSeconds(StunTime);
        StunEndEvent?.Invoke();
        yield return new WaitForSeconds(DamageTime);
        DamageAllowEvent?.Invoke();
        yield return new WaitForSeconds(EndTime);
        DamageEndEvent?.Invoke();
    }

    
}
