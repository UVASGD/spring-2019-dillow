using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public float range;
    public float aimForce;

    public GameObject barrel;

    public Rigidbody rb;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = barrel.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Aim(GameObject target)
    {
        Vector3 diff = (target.transform.position - barrel.transform.position).normalized;
        rb.AddForceAtPosition(diff * aimForce, barrel.transform.position);
    }

    public void Fire()
    {
        RaycastHit hit;

        if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hit, range))
        {
            Hit(hit.collider.gameObject, hit.point, hit.normal);
        }

        

        lineRenderer.SetPositions();
    }

    public void Hit(GameObject go, Vector3 position, Vector3 normal)
    {
        if(CompareTag("Ground"))
        {
            //Dink
        }
        else if(CompareTag("Player"))
        {
            //Ouch
        }
    }
}
