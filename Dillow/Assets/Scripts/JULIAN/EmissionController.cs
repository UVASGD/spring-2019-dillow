using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EmissionController : MonoBehaviour {

    [SerializeField]
    public float intensity = 1;
    private Material m;

    // Start is called before the first frame update
    void Start() {
        Renderer r = GetComponent<MeshRenderer>();
        m = r.material;

    }

    // Update is called once per frame
    void Update() {
        //m.SetColor("_EmissionColor", m.GetColor("EmissionColor") * intensity);
    }
}
