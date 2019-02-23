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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void SpawnFX(GameObject target, Vector3 position, Vector3 rotation)
    {
        GameObject spawned_fx = Instantiate(target, position, Quaternion.identity);
        spawned_fx.transform.position = position;
        spawned_fx.transform.forward = rotation;
    }

    void SpawnFX(FXType effectName, Vector3 position, Vector3 rotation)
    {
        SpawnFX(fx_objs[(int)effectName], position, rotation);
    }
}

