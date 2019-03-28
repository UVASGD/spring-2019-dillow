using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hypervisor : MonoBehaviour
{
    public Rotator rotator;
    public Noticer noticer;
    
    public Vector3 originalRotation;

    private bool noticed;
    private GameObject target;

    public GameObject SnowThrower;
    // Start is called before the first frame update
    void Start()
    {
        rotator = GetComponent<Rotator>();
        noticer = GetComponentInChildren<Noticer>();
        noticer.NoticeEvent += OnNotice;
        noticer.UnnoticeEvent += OnUnnotice;

        originalRotation = transform.forward;

        SnowThrower.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (noticed)
        {
            rotator.Face(target, false, false, false);
        }
    }

    public void OnNotice(TagHandler tagHandler)
    {
        if (tagHandler.HasTag(Tag.Player))
        {
            target = tagHandler.gameObject;
            noticed = true;
            SnowThrower.SetActive(true);
        }
    }

    public void OnUnnotice(TagHandler tagHandler)
    {
        if (tagHandler.HasTag(Tag.Player))
        {
            noticed = false;
            rotator.TurnTo(originalRotation, true, false, false);
            SnowThrower.SetActive(false);
        }
    }
}
