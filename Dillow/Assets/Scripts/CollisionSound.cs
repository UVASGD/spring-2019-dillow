using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays a sound whenever the object gets hit
/// </summary>
public class CollisionSound : MonoBehaviour
{
    public List<GameObject> collisionEffects = new List<GameObject>();
	float impactThreshold = 15f;
    float maxThreshold = 50f;

    Animator anim;
    int collide_hash;

    public bool softLimit = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        collide_hash = Animator.StringToHash("Collide");
    }

    //Dictionary<GameObject, Vector2> cols;
    //void Start() {
    //    cols = new Dictionary<GameObject, Vector2>();
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //cols[collision.gameObject] = collision.impulse;
        if (collision.impulse.magnitude > impactThreshold && !softLimit || softLimit)
        {
            if (anim)
                anim.SetTrigger(collide_hash);
            float vol = Mathf.Clamp01(collision.impulse.magnitude / maxThreshold);
            if (collisionEffects.Count > 0) {
                GameObject fx = collisionEffects[Random.Range(0, collisionEffects.Count)];
                Fx_Spawner.instance.SpawnFX(fx, collision.contacts[0].point, collision.contacts[0].normal, vol);
            }
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
