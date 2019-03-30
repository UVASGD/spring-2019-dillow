using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum FXType {Generic, Wood}

public class Fx_Spawner : MonoBehaviour {
    public AudioMixerGroup mixer;
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


    public GameObject SpawnFX(GameObject fx,  Vector3 position, Vector3 rotation, float vol = -1, Transform parent = null)
    {
        GameObject spawned_fx = Instantiate(fx, position, Quaternion.identity);
        if (parent)
        {
            spawned_fx.transform.parent = parent;
        }
        if (rotation != Vector3.zero)
            spawned_fx.transform.forward = rotation;
        Fx_Object fx_obj = spawned_fx.GetComponent<Fx_Object>();
        fx_obj.vol = vol;
        fx_obj.mixerGroup = mixer;

        return spawned_fx;
    }

    GameObject SpawnFX(FXType effectName, Vector3 position, Vector3 rotation)
    {
        return SpawnFX(fx_objs[(int)effectName], position, rotation);
    }
}

