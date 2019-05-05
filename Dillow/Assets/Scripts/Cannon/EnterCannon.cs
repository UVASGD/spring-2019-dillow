using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCannon : MonoBehaviour {

    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;
    [SerializeField] AudioClip eatSound;
	[SerializeField] AudioClip music;

	[SerializeField] bool enteringCannon;

    [SerializeField] float secondsInside;

    Vector3 startPosition;
    Vector3 targetPosition;
    float lerpTime;

    DillowBody player;
    Vector3 velocity;

    MouthController mouth;

    bool trigger;

    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        enteringCannon = false;
        trigger = false;
        secondsInside = 0f;
        lerpTime = 0f;

        startPosition = transform.localPosition;

        targetPosition = new Vector3 (startPosition.x, 0f, startPosition.z);

        if (DillowController.instance)
		    player = DillowController.instance.body;
		print("Dillow: " + player.name);

		//mouth = transform.parent.gameObject.GetComponentInChildren<MouthController> ();

		if (null == GetComponent<AudioSource> ()) {
            audioSource = gameObject.AddComponent<AudioSource> ();
        } else {
            audioSource = gameObject.GetComponent<AudioSource> ();
        }
		audioSource.spatialBlend = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (true == enteringCannon && null != player) {
            if (!trigger) {
				transform.parent.GetComponent<Cannon>().barrel.GetComponent<Collider> ().enabled = false;
				//player.GetComponent<Ball>().DisableGravity();
                audioSource.clip = eatSound;
                audioSource.Play ();
                trigger = true;
				player.rb.velocity = Vector3.zero;
				player.rb.useGravity = false;
				player.TransformToBall();
            }

            player.transform.position = Vector3.SmoothDamp (player.transform.position, transform.position, ref velocity, 0.25f);
            transform.localPosition = Vector3.Lerp (startPosition, targetPosition, lerpTime);

			//if (lerpTime == 0f) {
			//    mouth.SetExpression(MouthController.MouthState.eat);
			//}

            lerpTime += Time.deltaTime;

			if (lerpTime > 2.75f) {
				enteringCannon = false;
				//mouth.SetExpression(MouthController.MouthState.close);
				transform.parent.GetComponent<Cannon>().barrel.GetComponent<Collider>().enabled = true;
				transform.parent.GetComponent<UnityEngine.Playables.PlayableDirector>().Play();
				//AudioManager.PlayMusic(music, false, false, false, 0f, false);
				AudioManager.PlayMusic(music, false, false, false, 0f, false);
				//transform.parent.GetComponent<Cannon>().Aim(player.GetComponent<Rigidbody>());
			}
        }
	}

    public void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Player") && enabled) {
            secondsInside = 0f;
            audioSource.clip = openSound;
            audioSource.Play ();
            //mouth.SetExpression (MouthController.MouthState.open);
        }
    }

    private void OnTriggerStay (Collider other) {
        if (other.CompareTag ("Player") && enabled) {
            secondsInside += Time.deltaTime;
			transform.GetComponentInParent<Animator>().SetBool("CloseBy", true);
            if (secondsInside >= 3f && !trigger) {
                enteringCannon = true;
            }
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player") && enabled) {
			transform.GetComponentInParent<Animator>().SetBool("CloseBy", false);
			secondsInside = 0f;
            audioSource.clip = closeSound;
            audioSource.Play ();
            //mouth.SetExpression (MouthController.MouthState.neutral);
        }
    }

    public void Reset () {
        secondsInside = 0f;
        //mouth.SetExpression (MouthController.MouthState.neutral);
        transform.localPosition = startPosition;
        transform.parent.GetComponent<Cannon>().barrel.GetComponent<Collider> ().enabled = true;
        trigger = false;
    }
}
