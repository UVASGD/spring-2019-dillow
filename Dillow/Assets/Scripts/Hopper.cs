using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hopper : MonoBehaviour
{
    Rigidbody rb;
    public void Start()
    {
        rb = gameObject.GetMainRigidbody();
    }
    public void Hop(int hopTime,int hopInterval )
    {
        StartCoroutine(HopHop(hopTime,hopInterval));
    }

    private IEnumerator HopHop(int hopTime,int hopInterval)
    {
        for(int i=0; i < hopTime; i++)
        {
            rb.AddForce(Vector3.up * Random.Range(5,30), ForceMode.Impulse);
            yield return new WaitForSeconds(hopInterval);
        }
        yield return null;
    }
}
