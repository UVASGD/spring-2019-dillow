using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleChecker : MonoBehaviour
{
    public CollectibleType type;
    public int threshold;

    EnterCannon enter;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        enter = GetComponent<EnterCannon>();
        enter.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.collectibleCounts[type] >= threshold)
            {
                enter.enabled = true;
                enter.OnTriggerEnter(other);
                StartCoroutine(Messager.instance.DisplayMessage(threshold + "/" + threshold + " (" + type.ToString() + ") Collected!"));

                enabled = false;
            }
            else
            {
                StartCoroutine(Messager.instance.DisplayMessage(GameManager.collectibleCounts[type] + "/" + threshold + " (" + type.ToString() + ") Collected..."));
            }
        }
    }
}
