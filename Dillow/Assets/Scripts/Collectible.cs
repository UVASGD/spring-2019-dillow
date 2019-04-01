using System;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleType { Gear, Currency, Powerup, LevelSpecific, };

[RequireComponent(typeof(AudioSource))]
public class Collectible : MonoBehaviour {
	public CollectibleType type;
	public bool replenishable = false;
	public ulong id;

    public Animator anim;
    private AudioSource sound;

    private void Start () {
        sound = GetComponent<AudioSource>();
		if (!replenishable) {
			if (GameManager.HasCollectible(this)) {
				Destroy(gameObject);
			}
		}
	}

    protected virtual void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")) {
            CollectibleSoundController.instance.PlaySound(sound, sound.volume);
			GameManager.AddCollectible(this);
            if (anim != null)
                anim.SetTrigger("Kill");
            else
                Destroy(gameObject);
		}
	}

#if UNITY_EDITOR
    private static System.Random rand = new System.Random();

    public void ResetID()
    {
        byte[] buf = new byte[8];
        rand.NextBytes(buf);
        id = BitConverter.ToUInt64(buf, 0);
    }
#endif
}
