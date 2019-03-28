using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FXType {}

public class Fx_Spawner : MonoBehaviour
{
    public List<GameObject> fx_objs = new List<GameObject>();

    // Singleton code
    public static Fx_Spawner instance;
    private void Awake()
    {
        if (null == instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }


    public void SpawnFX(GameObject fx,  Vector3 position, Vector3 rotation, float vol = -1, Transform parent = null)
    {
        GameObject spawned_fx = Instantiate(fx, position, Quaternion.identity);
        if (parent)
        {
            spawned_fx.transform.parent = parent;
        }
        if (rotation != Vector3.zero)
            spawned_fx.transform.forward = rotation;
        spawned_fx.GetComponent<Fx_Object>().vol = vol;
    }

    void SpawnFX(FXType effectName, Vector3 position, Vector3 rotation)
    {
        SpawnFX(fx_objs[(int)effectName], position, rotation);
    }
}

