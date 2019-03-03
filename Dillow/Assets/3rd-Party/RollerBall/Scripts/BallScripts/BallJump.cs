using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void JumpDel();

public class BallJump : MonoBehaviour
{
    Collider jump_detector;

    float leeway = 0.2f;

    public JumpDel CanJumpEvent, StopJumpEvent;
    int groundCount;

    // Start is called before the first frame update
    void Start()
    {
        jump_detector = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Ground Terrain"))
        {
            groundCount++;

            if (groundCount == 1)
            {
                StopAllCoroutines();
                CanJumpEvent?.Invoke();
            }
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Ground Terrain"))
        {
            groundCount--;

            if (groundCount <= 0)
            {
                groundCount = 0;
                StartCoroutine(StopJump());
            }
        }
    }

    IEnumerator StopJump ()
    {
        yield return new WaitForSeconds(leeway);
        StopJumpEvent?.Invoke();
    }
}
