using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public float range = 50;

    public GameObject barrel;
    public LineRenderer laser;

    float aimAngleThreshold = 75f;
    Vector3 aimDirection;

    List<Tag> hit_tags;

    float laser_time = 0.25f, laserCD = 5f;
    bool can_fire = true;

    public LineRenderer aim_laser;
    public bool activated;

    // Start is called before the first frame update
    void Start()
    {
        aimDirection = barrel.transform.forward;
        laser.enabled = false;
        laser.useWorldSpace = true;
        aim_laser.enabled = false;
        aim_laser.useWorldSpace = true;

        hit_tags = new List<Tag>() { Tag.SuperDamage };

        //InvokeRepeating("HipFire", 1f, 1f);
    }

    void Update()
    {
        if (activated)
        {
            aim_laser.SetPosition(0, barrel.transform.position);

            RaycastHit hit;
            if (Physics.Raycast(barrel.transform.position, aimDirection, out hit, range))
            {
                aim_laser.SetPosition(1, hit.point);
            }
            else
            {
                aim_laser.SetPosition(1, barrel.transform.position + (aimDirection * range));
            }
        }
    }

    public void Activate(bool act = true)
    {
        activated = act;
        aim_laser.enabled = act;
    }

    public void Aim(GameObject target)
    {
        Vector3 targetDir = (target.transform.position - barrel.transform.position).normalized;
        float angleDiff = Vector3.Angle(barrel.transform.forward, targetDir);
        if (angleDiff < aimAngleThreshold)
        {
            aimDirection = targetDir;
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

    public void Fire(GameObject target = null)
    {
        if (!can_fire)
            return;

        if (target)
        {
            aimDirection = target.transform.position - barrel.transform.position;
        }

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
            hit.GetComponent<Body>().Collide(position, hit_tags, direction: -normal);
        }
    }
}
