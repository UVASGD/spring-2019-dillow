using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays a sound whenever the object gets hit
/// </summary>
public class CollisionSound : MonoBehaviour
{
    public GameObject collisionEffects;
	public float impactThreshold = 15f;
    public float maxThreshold = 50f;
    public bool softLimit = false;

    //Dictionary<GameObject, Vector2> cols;
    //void Start() {
    //    cols = new Dictionary<GameObject, Vector2>();
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //cols[collision.gameObject] = collision.impulse;
        if (collision.impulse.magnitude > impactThreshold && !softLimit || softLimit)
        {
            float vol = Mathf.Clamp01(collision.impulse.magnitude / maxThreshold);
        //Debug.Log(gameObject.name + " | " + vol);
            Fx_Spawner.instance.SpawnFX(collisionEffects, collisionEffects.transform.position, Vector3.forward, vol);
        }
    }

    //private void OnCollisionExit(Collision collision) {
    //    cols.Remove(collision.gameObject);
    //}

    //void OnGizmoDraw() {
    //    foreach(GameObject go in cols.Keys) {
    //        Gizmos.color = Color.Lerp(Color.green, Color.red, cols[go].magnitude);
    //        Gizmos.DrawLine(transform.position, cols[go]);
    //    }
    //}
}
