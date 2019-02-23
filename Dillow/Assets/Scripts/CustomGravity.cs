using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    public float gravityScale = 1.0f;
    // Gravity vector (ignored if gravitySource is set)
    public Vector3 globalGravity = 9.8f * Vector3.down;
    // Determines direction and magnitude of gravity (can be null)
    public GravitySource gravitySource;

    Rigidbody rb;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = gravitySource != null ? gravitySource.GetGravity(rb.position) : globalGravity;
        Vector3 totGravity = gravity * gravityScale;
        rb.AddForce(totGravity, ForceMode.Acceleration);
    }
}
