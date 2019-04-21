using UnityEngine;
using System.Collections.Generic;

public class ParticleHit : MonoBehaviour {


    public ParticleSystem part;
    List<Tag> hit_tags;

    void Start() {
        part = GetComponent<ParticleSystem>();
        hit_tags = new List<Tag>() { Tag.SuperDamage };
    }

    public void Hit(Collider collider, Vector3 position, Vector3 normal) {
        GameObject go = collider.gameObject;
        if (go.CompareTag("Ground")) {
        } else if (go.GetComponent<Body>()) {
            go.GetComponent<Body>().Collide(position, hit_tags, direction: -normal);
        }
    }

    void OnParticleCollision(GameObject other) {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

        if (other.CompareTag("Player")) {
            int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
            Rigidbody rb = other.GetComponent<Rigidbody>();

            for (int i = 0; i< numCollisionEvents; i++) {
                if (rb) {
                    Vector3 pos = collisionEvents[i].intersection;
                    Vector3 normal = collisionEvents[i].normal;

                    other.GetComponent<Body>().Collide(pos, hit_tags, direction: -normal);
                    break;
                }
            }
        }
    }
}
