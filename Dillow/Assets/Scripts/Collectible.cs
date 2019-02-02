using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleType { Gear, Currency, Powerup, LevelSpecific, };

public class Collectible : MonoBehaviour {
	public CollectibleType type;
	public bool replenishable = false;
	public ulong id;

	private void Start () {
		id = GetID();
		//print("ID: " + id);

		if (false == replenishable) {
			print("Colllectibles: " + GameManager.obtainedCollectibles.Count);
			if (GameManager.obtainedCollectibles.ContainsKey(id)) {
				Destroy(gameObject);
			}
		}
	}

	protected virtual void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")) {
			GameManager.obtainedCollectibles.Add(id, true);
			GameManager.saveData.collectiblesCount[(int)type]++;
			GameManager.Save();
			Destroy(gameObject);
		}
	}

	private ulong GetID () {
		return (ulong)(transform.position.x * 2048 * 2048 + transform.position.z * 2048 + transform.position.y);
	}
}
