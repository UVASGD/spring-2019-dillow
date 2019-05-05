using UnityEngine;
using System.Collections;

/// <summary>
/// an object there should only ever be one of
/// </summary>
public class GameKit : MonoBehaviour {

    public static GameKit instance;
    protected virtual void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
}
