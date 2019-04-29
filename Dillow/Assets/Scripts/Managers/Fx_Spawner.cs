using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum FXType {Generic, Wood}

public class Fx_Spawner : MonoBehaviour {
    public AudioMixerGroup mixer;
    public List<GameObject> fx_objs = new List<GameObject>();
    private GameObject holder;

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

        holder = new GameObject("FX Objects");
    }


    public GameObject SpawnFX(GameObject fx,  Vector3 position, Vector3 rotation, float vol = -1, Transform parent = null)
    {
        if(fx == null) return null;

        GameObject spawned_fx = Instantiate(fx, position, Quaternion.identity);
        spawned_fx.transform.parent = parent? parent : holder.transform;

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

