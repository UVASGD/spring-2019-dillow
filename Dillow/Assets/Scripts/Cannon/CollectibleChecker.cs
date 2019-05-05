using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleChecker : MonoBehaviour
{
    public CollectibleType type;
    public int threshold;

    EnterCannon enter;

    private void Start()
    {
        enter = GetComponent<EnterCannon>();
        enter.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.collectibleCounts[type] > threshold)
            {
                enter.enabled = true;
                enter.OnTriggerEnter(other);
                Messager.instance.DisplayMessage(threshold + "/" + threshold + " (" + type.ToString() + ") Collected!");
                enabled = false;
            }
            else
            {
                Messager.instance.DisplayMessage(GameManager.collectibleCounts[type] + "/" + threshold + " (" + type.ToString() + ") Collected...");
            }
        }
    }
}
