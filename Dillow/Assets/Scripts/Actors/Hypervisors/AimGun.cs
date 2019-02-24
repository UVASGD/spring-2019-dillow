using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGun : MonoBehaviour
{
    public GameObject player;
    public LaserGun gun;

    private void Update()
    {
        gun.Aim(player);
    }
}
