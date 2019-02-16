using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public Color flashColor = Color.red;

    private MeshRenderer renderer;
    private Color normalColor;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        normalColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update() {}

    IEnumerator Flash(float flashTime = 0.5f)
    {
        renderer.material.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        renderer.material.color = normalColor;
    }

}
