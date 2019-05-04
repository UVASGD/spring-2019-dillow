using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour {
    Camera main;
    CinemachineFreeLook free_cam;
    CinemachineVirtualCamera lock_cam;
    Reticle reticle;
    List<GameObject> lockables = new List<GameObject>();
    public GameObject locked;

    CinemachineTargetGroup ctg;
    Transform playerLoc;

    float range;

    int layermask;

    // Start is called before the first frame update
    void Start() {
        main = Camera.main;
        free_cam = FindObjectOfType<CinemachineFreeLook>();
        lock_cam = FindObjectOfType<CinemachineVirtualCamera>();
        ctg = FindObjectOfType<CinemachineTargetGroup>();

        reticle = GameObject.FindGameObjectWithTag("Reticle").GetComponent<Reticle>();
        reticle.gameObject.SetActive(false);
        range = GetComponent<SphereCollider>().radius * 2f;

        layermask = ~LayerMask.GetMask("Player", "Ground");
    }

    // Update is called once per frame
    void Update() {
        transform.rotation = main.transform.rotation;

        if (locked) {
            if (locked.HasTag(Tag.Dead)) {
                Lock(false);
                return;
            }

            if (Vector3.Distance(transform.position, locked.transform.position) < range) {
                reticle.SetReticle(main.WorldToScreenPoint(locked.transform.position));
            } else {
                Lock(false);
            }
        } else {
            reticle.Deactivate();
        }
    }

    public void LockCamera(GameObject target = null)
    {
        UnlockCamera();
        if (target)
        {
            free_cam.enabled = false;
            lock_cam.enabled = true;
            ctg.AddMember(target.transform, 1, 0);
        }
        else if (locked)
        {
            free_cam.enabled = false;
            lock_cam.enabled = true;
            ctg.AddMember(locked.transform, 1, 0);
        }
    }

    public void UnlockCamera()
    {
        free_cam.enabled = true;
        lock_cam.enabled = false;
        if (ctg.m_Targets.Length > 1) ctg.RemoveMember(ctg.m_Targets[1].target.transform);
    }

    ///
    public void Lock(bool lockon) {
        if (!lockon) {
            reticle.Deactivate();
            UnlockCamera();
            locked = null;
        } else {
            GameObject best_lock = locked;
            Vector3 target = (locked) ? locked.transform.position : Vector3.zero;
            float best_angle = float.PositiveInfinity;
            for (int i = lockables.Count - 1; i >= 0; i--) {
                GameObject lockable = lockables[i];
                if (!lockable) {
                    lockables.Remove(lockable);
                    continue;
                }

                if (Physics.Raycast(transform.position, (lockable.transform.position - transform.position).normalized,
                    out RaycastHit hit, range, layermask, QueryTriggerInteraction.Ignore)) {
                    if (hit.collider.gameObject != locked) {
                        Vector3 lock_angle = Vector3.ProjectOnPlane(hit.collider.gameObject.transform.position - transform.position, Vector3.up);
                        Vector3 cam_angle = Vector3.ProjectOnPlane(main.transform.forward, Vector3.up);
                        float angle = Vector3.Angle(lock_angle, cam_angle);
                        if (angle < best_angle) {
                            best_angle = angle;
                            best_lock = hit.collider.gameObject;
                        }
                    }
                }
            }
            if (best_lock) {
                if (locked != best_lock && locked)
                    UnlockCamera();
                    
                locked = best_lock;
                reticle.gameObject.SetActive(true);
                reticle.Activate(locked);
                //if( locked != best_lock) 
                if(ctg) LockCamera();
            }
        }
        DillowController.instance.body.lock_enemy = locked;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<ILockable>() != null) {
            lockables.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        lockables.Remove(other.gameObject);
    }
}
