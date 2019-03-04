using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour
{

    Renderer r;
    public Color flashColor = Color.red;
    public float flashTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
    }

    public void Flash()
    {
        StartCoroutine(FlashCo());
    }

    IEnumerator FlashCo()
    {
        Color normColor = r.material.color;
        r.material.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        r.material.color = normColor;
    }
}
