using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHandler : MonoBehaviour {
	private void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Collectible")) {
			Collectible collectible = other.GetComponent<Collectible>();

			if (null == collectible) return;


		}
	}

	void Collect (Collectible collectible) {
	
	}
}
