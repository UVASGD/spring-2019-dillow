using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public void Start()
    {
        rb = gameObject.GetMainRigidbody();
    }
    void Spin(int spinTime, float torque)
    {
        StartCoroutine(SpinSpin(spinTime, torque));
    }
    public IEnumerator SpinSpin(int spinTime, float torque)
    {
        for (int i = 0; i < spinTime; i++)
        {
            rb.AddTorque(Vector3.up * torque);
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }
}
