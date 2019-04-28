using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : MonoBehaviour
{
    public bool dash;
    public bool continuousDash;

    public float dashForce;
    public float chargeTime;
    private float nextChargeTime;

    public Transform target;

    public Transform front;

    bool readyDash = false;

    private void Update()
    {
        // Single dash and stop
        if (!continuousDash)
        {
            if (dash && !readyDash)
            {
                readyDash = true;
                nextChargeTime = Time.time + chargeTime;
                DashTowards(target.position);
            }
            else if (dash)
            {
                DashTowards(target.position);
            }
        }
        // Continuous dashing until obstacle
        else
        {
            if (dash && !readyDash)
            {
                readyDash = true;
                nextChargeTime = Time.time + chargeTime;
                ContinuousDashTowards(target.position);
            }
            else if (dash)
            {
                ContinuousDashTowards(target.position);
            }
        }
    }

    public void DashTowards(Vector3 target)
    {
        Vector3 dir = (target - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

        if (true)  // if(!Obstructed())
        {
            if (Time.time > nextChargeTime)
            {
                Dash();
                readyDash = false;
                dash = false;
            }
        }
        else
        {
            print("Obstructed");
            dash = false;
            readyDash = false;
        }
    }

    public void ContinuousDashTowards(Vector3 target)
    {
        Vector3 dir = (target - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

        if (true)  // if(!Obstructed())
        {
            if (Time.time > nextChargeTime)
            {
                Dash();
                readyDash = false;
            }
        }
        else
        {
            print("Obstructed");
            dash = false;
            readyDash = false;
        }
    }

    private void Dash()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * dashForce, ForceMode.Impulse);
    }

    private bool Obstructed()
    {
        return Physics.Raycast(front.position, transform.forward, dashForce);
    }
}
