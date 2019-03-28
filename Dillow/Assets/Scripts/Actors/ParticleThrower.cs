using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleThrower : MonoBehaviour
{
    public GameObject impact_fx;

    private ParticleSystem part;
    List<ParticleCollisionEvent> coll_list = new List<ParticleCollisionEvent>();

    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other != gameObject)
        {
            part.GetCollisionEvents(gameObject, coll_list);
            Fx_Spawner.instance.SpawnFX(impact_fx, coll_list[0].intersection, coll_list[0].normal);
        }
    }
}
