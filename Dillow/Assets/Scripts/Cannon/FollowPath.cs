using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FollowPath : MonoBehaviour {
	public float speed = 20f;
	private Vector3 horizontalVelocity;
	private float horizontalSpeed;
	private Vector3 velocity;

	public int currentIndex;
	private int lastIndex;
	private Vector3 currentTarget;
	private Vector3 previousTarget;
	private Vector3 dampVel; //Smooth damp reference
	private Rigidbody rb;

	private AnimationCurve speedMultiplier;

	public Vector3[] pathNodes;

	public bool traversing = false;

	private Transform guide;

	private float distanceTraveledPerNode;
	private float percentDone;
	private bool sceneChanged;

	public void SetPath (Vector3[] nodes) {
		currentIndex = 1;
		pathNodes = nodes;
		lastIndex = nodes.Length;
		currentTarget = nodes[0];
		rb = GetComponent<Rigidbody>();
	}

	public void SetPath (Vector3[] nodes, float speed) {
		currentIndex = 1;
		this.speed = speed;
		velocity = (nodes[1] - nodes[0]).normalized * speed;
		horizontalVelocity = Vector3.ProjectOnPlane(velocity, Vector3.up);
		pathNodes = nodes;
		lastIndex = nodes.Length;
		currentTarget = nodes[0];
		rb = GetComponent<Rigidbody>();
	}

	public void SetPath (Vector3[] nodes, float speed, bool autoTraverse) {
		currentIndex = 1;
		this.speed = speed;
		velocity = (nodes[1] - nodes[0]).normalized * speed;
		horizontalVelocity = Vector3.ProjectOnPlane(velocity, Vector3.up);
		horizontalSpeed = horizontalVelocity.magnitude;
		print("Horizontal Vel: " + horizontalVelocity);
		print("Speed: " + speed);
		pathNodes = nodes;
		lastIndex = nodes.Length;
		currentTarget = nodes[0];
		rb = GetComponent<Rigidbody>();

		if (true == autoTraverse && null != rb) {
			TraversePath();
		} else {
			print("No rigidbody!");
		}
	}

	public void SetPath (Vector3[] nodes, float speed, bool autoTraverse, AnimationCurve speedMultiplier) {
		this.speedMultiplier = speedMultiplier;
		SetPath(nodes, speed, autoTraverse);
	}

	private void FixedUpdate () {
		if (false == traversing) return;

		if (pathNodes.Length > 0) {
			if (currentIndex < lastIndex) {
				currentTarget = pathNodes[currentIndex];
				previousTarget = pathNodes[currentIndex - 1];

				Vector3 unscaledVelocity = currentTarget - previousTarget;
				float distance = unscaledVelocity.magnitude;
				//print("Speed Multiplier: " + speedMultiplier.Evaluate(percentDone));
				Vector3 velocity = unscaledVelocity / unscaledVelocity.y * horizontalSpeed * 
				                   (speedMultiplier.Evaluate(percentDone)) * Time.fixedDeltaTime;

				//print("Horizontal Speed: " + horizontalSpeed);
				//print("Velocity: " + velocity);
				guide.position += velocity;

				distanceTraveledPerNode += velocity.magnitude;
				float t = distanceTraveledPerNode / distance;
				guide.position = Vector3.Lerp(previousTarget, currentTarget, t);
				percentDone = (currentIndex - 1 + t) / (lastIndex - 1);


				if (t > 1f) {
					//print("REACHED POINT");
					distanceTraveledPerNode = 0f;
					guide.position = currentTarget;
					currentIndex++;
				}
				
				
			}

			if (percentDone > 0.5f && !sceneChanged) {
				sceneChanged = true;
				GameManager.LoadLevel("MainMenu");
			}

			if (Physics.CheckSphere(rb.position, GetComponent<SphereCollider>().radius * 1.1f, LayerMask.GetMask("Default")) && percentDone > 0.5f) {
				//GetComponent<Ball>().EnableGravity();
				traversing = false;
				Destroy(guide.gameObject);
				Destroy(this);
			}

			if (percentDone == 1f) {
				Destroy(guide.gameObject, 1.5f);
				Destroy(this, 1.5f);
			}
		}

		rb.position = Vector3.SmoothDamp(rb.position, guide.position, ref dampVel, 0.2f);
	}

	public IEnumerator LoadScene (string sceneName) {
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
		yield return new WaitForEndOfFrame();
	}

	public void TraversePath () {
		traversing = true;
		distanceTraveledPerNode = 0f;
		percentDone = 0f;
		sceneChanged = false;
	
		GameObject guideGO = new GameObject("Guide");
		guide = guideGO.transform;
		guide.position = transform.position;
		guide.transform.SetParent(transform.parent);
	}
}
