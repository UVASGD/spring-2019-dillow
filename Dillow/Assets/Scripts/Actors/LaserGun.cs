using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public float range = 50;
    public float aimForce = 10;

    public GameObject barrel;

    public Rigidbody rb;
    LineRenderer laser;

    public float aimAngleThreshold = 15f;
    Vector3 aimDirection;

    List<Tag> hit_tags;

    float laser_time = 0.25f, laserCD = 5f;
    bool can_fire = true;

    // Start is called before the first frame update
    void Start()
    {
        aimDirection = barrel.transform.forward;
        laser = barrel.GetComponent<LineRenderer>();
        laser.enabled = false;

        hit_tags = new List<Tag>() { Tag.SuperDamage };

        InvokeRepeating("HipFire", 1f, 1f);
    }

    public void Aim(GameObject target)
    {
        Vector3 diff = (target.transform.position - barrel.transform.position).normalized;
        rb?.AddForceAtPosition(diff * aimForce, barrel.transform.position);
        float angleDiff = Vector3.Angle(barrel.transform.forward, diff);
        if (angleDiff < aimAngleThreshold)
        {
            aimDirection = diff;
        }
        else
        {
            aimDirection = barrel.transform.forward;
        }
        
    }

    public void HipFire()
    {
        aimDirection = barrel.transform.forward;
        Fire();
    }

    public void Fire()
    {
        if (!can_fire)
            return;

        RaycastHit hit;

        laser.SetPosition(0, barrel.transform.position);

        if (Physics.Raycast(barrel.transform.position, aimDirection, out hit, range))
        {
            laser.SetPosition(1, hit.point);
            Hit(hit.collider.gameObject, hit.point, hit.normal);
        }
        else
        {
            laser.SetPosition(1, barrel.transform.position + (barrel.transform.forward * range));
        }

        StartCoroutine(LaserFlash());
    }

    IEnumerator LaserFlash()
    {
        can_fire = false;
        laser.enabled = true;
        yield return new WaitForSeconds(laser_time);
        laser.enabled = false;
        yield return new WaitForSeconds(laserCD);
        can_fire = true;
    }

    public void Hit(GameObject hit, Vector3 position, Vector3 normal)
    {
        if (hit.CompareTag("Ground"))
        {
        }
        else if(hit.GetComponent<Body>())
        {
            hit.GetComponent<Body>().Collide(hit_tags);
        }
    }
}
