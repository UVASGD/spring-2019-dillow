using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DillowModel : MonoBehaviour
{
    BallBody body;
    Rigidbody bodyrb, rb;
    Vector3 euler_velocity;
    float rot_speed = 50, turn_speed = 1f;

    GameObject spinning_body;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        spinning_body = rb.gameObject;
        body = transform.parent.GetComponentInChildren<BallBody>();
        while (!body.ready) {
            yield return null;
        }
        bodyrb = body.rb;
        body.damager.AddFlasher(GetComponentInChildren<Flasher>());
    }

    void Update()
    {
        if (!bodyrb)
            return;
        if (bodyrb.velocity.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(Vector3.ProjectOnPlane(bodyrb.velocity.normalized, Vector3.up)), turn_speed);
        }

        spinning_body.transform.Rotate(Vector3.right * Time.deltaTime * bodyrb.angularVelocity.magnitude * rot_speed);
    }

    /*
    void FixedUpdate()
    {
        euler_velocity = new Vector3(rot_speed * bodyrb.angularVelocity.magnitude, 0, 0);
        Quaternion deltaRotation = Quaternion.Euler(euler_velocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

        /*
         *     
         * 
         * 
        // Update is called once per frame
        void Update()
        { 


            if (!bodyRb)
                return;
            velocity = rb.velocity.normalized;

            rb.angularVelocity = new Vector3(0, bodyRb.angularVelocity.magnitude, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(velocity), turn_speed);

            print("Velocity: " + velocity + " Speed: " + speed);
            print("transform rotation: " + transform.rotation);

            float y = transform.rotation.y;

            //transform.rotation = new Quaternion(transform.rotation.x, y, 0, transform.rotation.w);

            print("transform rotation now: " + transform.rotation);

            model.transform.Rotate(0, rots * Time.deltaTime * speed, 0, Space.World);
        }
        */
    }
