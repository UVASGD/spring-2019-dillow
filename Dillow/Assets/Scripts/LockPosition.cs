using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockPosition : MonoBehaviour
{
    GameObject target, player;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<CinemachineTargetGroup>().gameObject;
        player = DillowController.instance.gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + (player.transform.position - target.transform.position);
    }
}
