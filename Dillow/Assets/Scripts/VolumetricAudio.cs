using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumetricAudio : MonoBehaviour
{
    public GameObject aud;
    List<Collider> bounds;

    public bool occlude;
    public float occlude_dist;

    float range = 10000;
    // Start is called before the first frame update
    void Start()
    {
        if (!aud) aud = transform.GetChild(0).gameObject;
        bounds = new List<Collider>(GetComponentsInChildren<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        aud.transform.position = GetClosestPoint(DillowController.instance.body.transform.position);
        if (occlude && Physics.Raycast(aud.transform.position, (DillowController.instance.body.transform.position - aud.transform.position).normalized,
            out RaycastHit hit, range, LayerMask.GetMask("Ground"), queryTriggerInteraction: QueryTriggerInteraction.Ignore))
            if (Vector3.Distance(DillowController.instance.body.transform.position, hit.point) > occlude_dist)
            {
                aud.SetActive(false);
                return;
            }
        aud.SetActive(true);
    }

    Vector3 GetClosestPoint(Vector3 point)
    {
        float best_dist = float.PositiveInfinity;
        Vector3 best_loc = Vector3.zero;
        foreach (Collider c in bounds)
        {
            Vector3 loc = c.ClosestPoint(point);
            float dist = Vector3.Distance(loc, point);
            if (dist < best_dist)
            {
                best_dist = dist;
                best_loc = loc;
            }
        }
        return best_loc;
    }
}
