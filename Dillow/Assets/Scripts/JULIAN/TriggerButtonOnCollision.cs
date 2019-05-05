using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Renderer))]
public class TriggerButtonOnCollision : MonoBehaviour
{

    public Button button;

    private void OnBecameInvisible() {
        button.onClick?.Invoke();
    }
}
