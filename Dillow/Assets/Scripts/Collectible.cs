using System;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleType { Gear, Currency, Powerup, LevelSpecific, };

public class Collectible : MonoBehaviour {
	public CollectibleType type;
	public bool replenishable = false;
	public string id;

    private static HashSet<string> ids;

	private void Start () {
		if (ids.Contains(id)) {
            throw new InvalidOperationException("Duplicate id: " + id);
        }
        ids.Add(id);

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
}
