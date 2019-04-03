using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    Dictionary<GameObject, Collider> victims = new Dictionary<GameObject, Collider>();
    Collider coll;

    private void Start()
    {
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> removed = new List<GameObject>();
        foreach (GameObject victim in victims.Keys)
        {
            if (victims[victim].bounds.max.y < transform.position.y)
            {
                if (!victim.HasTag(Tag.Marine))
                {
                    victim.GetComponent<IMortal>().Die();
                    removed.Add(victim);
                }
            }
            else if ((victims[victim].transform.position.y - (victims[victim].bounds.size.y * 2f)) > transform.position.y)
            {
                removed.Add(victim);
            }
        }
        foreach (GameObject victim in removed)
        {
            if (victim)
                Physics.IgnoreCollision(coll, victims[victim], false);
            victims.Remove(victim);
        }
    }

    public void AddVictim(GameObject victim, Collider drowning_collider)
    {
        if (victim.GetComponent<IMortal>() != null && !victims.ContainsKey(victim))
        {
            victims.Add(victim, drowning_collider);
            Physics.IgnoreCollision(coll, drowning_collider);
        }
    }
}
