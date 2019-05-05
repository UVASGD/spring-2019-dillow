using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CannonEffects : MonoBehaviour
{
    [Header("Locations")]
    public GameObject flame_loc;
    public GameObject puff_loc;

    [Header("Effects")]
    public GameObject flame_fx;
    public GameObject cannon_fx;

    CinemachineImpulseSource imp;

    // Start is called before the first frame update
    void Start()
    {
        imp = GetComponent<CinemachineImpulseSource>();
        //Boom();
    }

    public void Boom()
    {
        Fx_Spawner.instance.SpawnFX(flame_fx, flame_loc.transform.position, flame_loc.transform.forward);
        Fx_Spawner.instance.SpawnFX(cannon_fx, puff_loc.transform.position, puff_loc.transform.up);
        if (imp) imp.GenerateImpulse();
    }
}
