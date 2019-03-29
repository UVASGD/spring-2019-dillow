 using UnityEngine;
 using System.Collections;

[RequireComponent(typeof(MeshFilter))]
 public class MeshWave : MonoBehaviour {
	Mesh mesh;
	float t;
	int positionCount;
	Vector3[] initialPositions;
	Vector3[] transformedPositions;
	float x, y;

	public float magnitude = 10f;
	public float frequency = 1f;

	private void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		initialPositions = mesh.vertices;
		positionCount = initialPositions.Length;
		transformedPositions = new Vector3[positionCount];
		print(positionCount);
	}

	private void Update () {
		for (int i = 0; i < positionCount; i++) {
			x = initialPositions[i].x;
			y = initialPositions[i].y;
			transformedPositions[i] = initialPositions[i] + Vector3.forward * Mathf.Sin(frequency * (x + y) * t) * magnitude;
		}
		mesh.vertices = transformedPositions;
		//mesh
		t += Time.deltaTime;
	}
}