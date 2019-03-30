using System;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleType { Gear, Currency, Powerup, LevelSpecific, };

public class Collectible : MonoBehaviour {
	public CollectibleType type;
	public bool replenishable = false;
	public ulong id;

    private void Start () {
		if (!replenishable) {
			if (GameManager.HasCollectible(this)) {
				Destroy(gameObject);
			}
		}
	}

    protected virtual void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")) {
			GameManager.AddCollectible(this);
			Destroy(gameObject);
		}
	}

#if UNITY_EDITOR
    private static System.Random rand = new System.Random();

    private void Reset()
    {
        byte[] buf = new byte[8];
        rand.NextBytes(buf);
        id = BitConverter.ToUInt64(buf, 0);
    }
#endif
}
