using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour {

	#region VARIABLES
	//Calculation Info
	public Transform barrel;
    Vector3 origin;
    Vector3 target;
    Vector3 apex;
	Vector3 cross;
	float aimAngle;
	float rotateAngle;
	bool clockwise;

	
	//Time Info
	[SerializeField] private float rotateSpeed = 30;
	[SerializeField] private AnimationCurve pathSpeedMultiplier;
	[SerializeField] public float speed;

	
	//Status Info
	public bool aimingTriggered;
	bool aiming;
	public bool firingTriggered;
	bool firing;
	public bool expressingTriggered;
	bool expressing;
	
	bool inoperable;
	float angleTraversed;
	
	
	//Path info
	CannonPath path;
	Vector3[] pathNodes;
	#endregion


	#region PREPROCESSING
	// Use this for initialization
	void Start () {
		path = transform.Find("CannonPath").GetComponent<CannonPath>();
		if (null == path) {
			inoperable = true;
			return;
		}

		pathNodes = path.pathNodes;
		if (null == pathNodes) {
			inoperable = true;
			return;
		}

		if (null == barrel) {
			inoperable = true;
		}
		inoperable = false;
		aiming = false;

		CalculateAngles();
	}
	
	void CalculateAngles () {
		origin = path.start.position;
		target = path.end.position;

		Vector3 shotVector = pathNodes[1] - pathNodes[0];

		aimAngle = 90f - Vector3.Angle(shotVector, Vector3.ProjectOnPlane(shotVector, Vector3.up));

        rotateAngle = Vector3.Angle(new Vector3(transform.up.x, 0f, transform.up.z),
                                    new Vector3(target.x, 0f, target.z) - new Vector3(origin.x, 0f, origin.z));

        cross = Vector3.Cross(new Vector3(transform.up.x, 0f, transform.up.z),
                              new Vector3(target.x, 0f, target.z) - new Vector3(origin.x, 0f, origin.z));

        if (cross.y < 0f) {
            clockwise = false;
        } else {
			clockwise = true;
		}
	}
	#endregion


	#region UPDATE
	private void Update () {
		TriggerHandler();

		if (aiming && !firing) {
			DillowController.instance.body.transform.position = barrel.position;
		}
	}

	void TriggerHandler () {
		if (aimingTriggered && !aiming) {
			StartCoroutine(Aim());
		}
		
		if (firingTriggered && !firing) {
			Fire();
		}
	}
	#endregion
	

	#region TRIGGERED EVENTS
	IEnumerator Aim (Rigidbody projectile = null) {
        aiming = true;
		DillowController.instance.can_input = false;
		if (null == projectile) {
			projectile = GameManager.instance.player.GetComponent<Rigidbody>();
		}
		
		yield return new WaitForSeconds (3f);

        angleTraversed = 0;

        while (angleTraversed < rotateAngle) {
            //transform.Rotate(transform.up, (clockwise ? 1 : -1) * rotateSpeed * Time.deltaTime);
			transform.eulerAngles += Vector3.up * (clockwise ? 1 : -1) * rotateSpeed * Time.deltaTime;
			angleTraversed += rotateSpeed * Time.deltaTime;

            projectile.position = barrel.position;
            projectile.velocity = Vector3.zero;

            yield return new WaitForEndOfFrame();
        }

        angleTraversed = 0;

        yield return new WaitForSeconds(3f);
        
        while (angleTraversed < aimAngle) {
            barrel.localEulerAngles -= Vector3.right * rotateSpeed * Time.deltaTime;
            angleTraversed += rotateSpeed * Time.deltaTime;

            projectile.position = barrel.position;
            projectile.velocity = Vector3.zero;

            yield return new WaitForEndOfFrame();
        }
	}

	//void SetExpression (MouthController.MouthState state) {
	//	expressing = true;
	//	GetComponentInChildren<MouthController>().SetExpression(state);
	//}
	
	void Fire (Rigidbody projectile = null) {
		firing = true;
		DillowController.instance.body.GetComponent<Rigidbody>().useGravity = true;
		if (null == projectile) {
			projectile = GameManager.instance.player.GetComponent<Rigidbody>();
		}
		projectile.gameObject.AddComponent<FollowPath>().SetPath(pathNodes, speed, true, pathSpeedMultiplier);
		transform.GetComponent<Animator>().SetTrigger("Explode");
		DillowController.instance.can_input = true;
	}
	#endregion
}
