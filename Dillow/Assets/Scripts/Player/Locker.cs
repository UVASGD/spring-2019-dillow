using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker :  Follower
{
    Camera main;
    Reticle reticle;
    List<GameObject> lockables = new List<GameObject>();
    GameObject locked;

    float range;

    int layermask;

    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        reticle = GameObject.FindGameObjectWithTag("Reticle").GetComponent<Reticle>();
        reticle.gameObject.SetActive(false);
        range = GetComponent<SphereCollider>().radius;

        layermask = LayerMask.GetMask("Player");
        layermask = ~layermask;
    }

    // Update is called once per frame
    void Update()
    {
        if (locked)
        {
            if (locked.GetComponent<Body>())
            {
                if (locked.GetComponent<Body>().dead)
                {
                    Lock(false);
                    return;
                }
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (locked.transform.position - transform.position).normalized, out hit, range, layermask, QueryTriggerInteraction.Ignore))
            {
                reticle.SetReticle(main.WorldToScreenPoint(hit.collider.transform.position));
            }
            else
            {
                Lock(false);
            }
        }
        else
        {
            reticle.Deactivate();
        }
    }

    public void Lock(bool lockon)
    {
        if (!lockon)
        {
            reticle.Deactivate();
            locked = null;
        }
        else
        {
            GameObject best_lock = locked;
            Vector3 target = (locked) ? locked.transform.position : Vector3.zero;
            float best_angle = float.PositiveInfinity;
            for (int i = lockables.Count-1; i >= 0; i--)
            {
                GameObject lockable = lockables[i];
                if (!lockable)
                {
                    lockables.Remove(lockable);
                    continue;
                }
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (lockable.transform.position - transform.position).normalized, out hit, range, layermask, QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.gameObject != locked)
                    {
                        Vector3 lock_angle = Vector3.ProjectOnPlane(hit.collider.gameObject.transform.position - transform.position, Vector3.up);
                        Vector3 cam_angle = Vector3.ProjectOnPlane(main.transform.forward, Vector3.up);
                        float angle = Vector3.Angle(lock_angle, cam_angle);
                        if (angle < best_angle)
                        {
                            best_angle = angle;
                            best_lock = hit.collider.gameObject;
                        }
                    }
                }
            }
            if (best_lock)
            {
                BallController.instance.body.lock_enemy = locked;
                locked = best_lock;
                reticle.gameObject.SetActive(true);
                reticle.Activate(locked);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ILockable>() != null)
        {
            lockables.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        lockables.Remove(other.gameObject);
    }
}
