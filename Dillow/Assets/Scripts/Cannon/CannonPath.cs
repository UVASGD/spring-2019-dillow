using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CannonPath : MonoBehaviour {

	[Header("Node Info")]
    public Transform start;
	private Vector3 startPosition;
    public Transform mid;
	private Vector3 midPosition;
	public Transform end;
	private Vector3 endPosition;
	public int resolution = 20;

	[Header("Automation Info")]
	[Tooltip("Must have a GameObject with the name 'Barrel'")]
	public bool autoConfigureStartpoint = false;
	public bool autoConfigureMidpoint = false;
	[Tooltip("Midpoint will be double this value")] 
	public float curveHeight = 200f;
	public bool autoConfigureEndpoint = false;
	public Transform endTarget;

	[Header("Debug Info")]
	public bool drawNodes = true;
	public float nodeRadius = 10f;
	public bool drawPath = true;
	public float pathRadius = 10f;

	public Vector3[] pathNodes;

	private void Start () {
		if (Application.isPlaying) {
			if (autoConfigureEndpoint) {
				autoConfigureEndpoint = false;
			}

			UpdatePath();
		}
	}

	private void Update () {
		if (null == start) {
			start = transform.Find("Start");

			if (null == start) {
				start = transform.GetChild(0);
			}
		}
		if (null == mid) {
			mid = transform.Find("Mid");

			if (null == mid) {
				mid = transform.GetChild(1);
			}
		}
		if (null == end) {
			end = transform.Find("End");

			if (null == end) {
				end = transform.GetChild(2);
			}
		}

		if (resolution < 3) resolution = 3;

		if (false == Application.isPlaying) {
			if (true == autoConfigureStartpoint) {
				Transform barrel = transform.parent.Find("Barrel");

				if (null == barrel) {
					autoConfigureStartpoint = false;
					Debug.LogWarning("Cannot autoconfigure startpoint! Make sure there is a Barrel attached to parent.");
				} else {
					start.position = barrel.position;
				}
			}

			if (true == autoConfigureMidpoint) {
				Vector3 targetPosition = (start.position + end.position) / 2;
				targetPosition.y = curveHeight * 2f;
				mid.position = targetPosition;
			}

			if (true == autoConfigureEndpoint && null != endTarget) {
				end.position = endTarget.position;
			}
			UpdatePath();
		} else {
			if (Input.GetKeyDown(KeyCode.L)) {
				GameObject.Find("RollerBall").AddComponent<FollowPath>();
				GameObject.Find("RollerBall").GetComponent<FollowPath>().SetPath(pathNodes, 200f, true);
			}
		}
	}

	public void UpdatePath () {
        if (start && mid && end) {
			startPosition = start.position;
			midPosition = mid.position;
			endPosition = end.position;
            pathNodes = GetPath (start.position, mid.position, end.position, resolution);
        }
    }

    Vector3 GetPoint (Vector3 p0, Vector3 p1, Vector3 p2, float t) {
        return Vector3.Lerp (Vector3.Lerp (p0, p1, t), Vector3.Lerp (p1, p2, t), t);
    }

    Vector3[] GetPath (Vector3 p0, Vector3 p1, Vector3 p2, int resolution = 20) {
        Vector3[] path = new Vector3[resolution];
        for (int i = 0; i < resolution; i++) {
            path[i] = GetPoint (p0, p1, p2, (float)i / (resolution-1));
        }
        return path;
    }

    private void OnDrawGizmos () {
		if (true == drawNodes) {
			if (null != start && false == autoConfigureStartpoint) {
				Gizmos.color = Color.green;
				Gizmos.DrawSphere(startPosition, nodeRadius);
			}
			if (null != mid && false == autoConfigureMidpoint) {
				Gizmos.color = Color.blue;
				Gizmos.DrawSphere(midPosition, nodeRadius);
			}
			if (null != end) {
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(endPosition, nodeRadius);
			}
		}

        if (true == drawPath && pathNodes.Length > 1) {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < pathNodes.Length - 1; i++) {
                DrawLine (pathNodes[i], pathNodes[i + 1], 10f);
            }
        }
    }

	public void DrawLine (Vector3 p1, Vector3 p2, float width) {
		int count = Mathf.CeilToInt(width); // how many lines are needed.
		if (count == 1)
			Gizmos.DrawLine(p1, p2);
		else {
			Camera c = Camera.current;
			if (c == null) {
				Debug.LogError("Camera.current is null");
				return;
			}
			Vector3 v1 = (p2 - p1).normalized; // line direction
			Vector3 v2 = (c.transform.position - p1).normalized; // direction to camera
			Vector3 n = Vector3.Cross(v1, v2); // normal vector
			for (int i = 0; i < count; i++) {
				Vector3 o = n * width * ((float)i / (count - 1) - 0.5f);
				Gizmos.DrawLine(p1 + o, p2 + o);
			}
		}
	}
}
