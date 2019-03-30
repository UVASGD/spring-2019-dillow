using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hypervisor : Mob
{
    Rotator rotator;
    
    public Vector3 originalRotation;

    public GameObject SnowThrower;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rotator = GetComponent<Rotator>();

        originalRotation = Vector3.ProjectOnPlane(Random.insideUnitSphere, Vector3.up).normalized;
        rotator.TurnTo(originalRotation, true, false, true);

        SnowThrower.SetActive(false);
    }

	protected override void OnNotice (TagHandler tagHandler)
    {
		print("Child notice!");
		base.OnNotice(tagHandler);

		if (tagHandler.HasTag(Tag.Player))
        {
            target = tagHandler.gameObject;
            SnowThrower.SetActive(true);
            current_behavior = Aggro;
        }
    }

	protected override void OnUnnotice (TagHandler tagHandler)
    {
		print("Child unnotice!");
		base.OnUnnotice(tagHandler);

		if (tagHandler.HasTag(Tag.Player))
        {
            current_behavior = null;
            Calm();
        }
    }

    void Aggro()
    {
        if (target)
        {
            rotator.Face(target, false, false, false);
        }
        else
        {
            current_behavior = null;
            Calm();
        }
    }

    void Calm()
    {
        rotator.TurnTo(originalRotation, false, false, false);
        SnowThrower.SetActive(false);
    }

    protected override void Die(Vector3 dir, Vector3 pos)
    {
        SnowThrower.SetActive(false);
        base.Die(dir, pos);
    }
}
