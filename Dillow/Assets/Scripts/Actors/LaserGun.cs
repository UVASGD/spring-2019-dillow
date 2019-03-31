using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [HideInInspector] public float range = 500;

    public GameObject barrel;
    public LineRenderer laser;

    float aimAngleThreshold = 75f;
    Vector3 aimDirection;

    List<Tag> hit_tags;

    float laser_time = 0.25f;
    bool can_fire = true;

    public LineRenderer aim_laser;
    public bool activated;

    public GameObject shot_fx;
    public GameObject hit_fx;
    public GameObject charge_fx;
    float charge_time = 1f; 
    bool charging;

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
            print("I'm still aiming, fuck you");
            aim_laser.SetPosition(0, barrel.transform.position);

            RaycastHit hit;
            if (Physics.Raycast(barrel.transform.position, aimDirection, out hit, range, Physics.AllLayers, QueryTriggerInteraction.Ignore))
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
        print("Disable that stupid laser");
    }

    public bool Aim(GameObject target)
    {
        Vector3 targetDir = (target.transform.position - barrel.transform.position).normalized;
        float angleDiff = Vector3.Angle(barrel.transform.forward, targetDir);
        if (angleDiff < aimAngleThreshold)
        {
            aimDirection = targetDir;
            return true;
        }
        else
        {
            aimDirection = barrel.transform.forward;
            return false;
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
            TagHandler t = target.GetComponent<TagHandler>();
            if (t && t.HasTag(Tag.Dashing))
            {
                aimDirection = (target.transform.position - (target.GetComponent<Rigidbody>().velocity / 10f)) - barrel.transform.position;
            }
            else
            {
                aimDirection = target.transform.position - barrel.transform.position;
            }
        }

        aimDirection = aimDirection.normalized;

        if (shot_fx) Fx_Spawner.instance.SpawnFX(shot_fx, barrel.transform.position, barrel.transform.forward);

        laser.SetPosition(0, barrel.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(barrel.transform.position, aimDirection, out hit, range, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            laser.SetPosition(1, hit.point);
            Hit(hit.collider.gameObject, hit.point, hit.normal);
            if (hit_fx) Fx_Spawner.instance.SpawnFX(hit_fx, hit.point, hit.normal);
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
        //yield return new WaitForSeconds(laserCD);
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

    public void Charge(float aim_time)
    {
        if (can_fire && !charging && aim_time < charge_time) StartCoroutine(ChargeCo());
    }

    IEnumerator ChargeCo()
    {
        charging = true;
        if (charge_fx) Fx_Spawner.instance.SpawnFX(charge_fx, barrel.transform.position, barrel.transform.forward);
        yield return new WaitForSeconds(charge_time);
        charging = false;
    }
}
