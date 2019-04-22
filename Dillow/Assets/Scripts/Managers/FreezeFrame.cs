using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFrame : MonoBehaviour
{
    public static FreezeFrame instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    public void Freeze(float freeze_time = 0.2f)
    {
        StartCoroutine(FreezeCo(freeze_time));
    }

    IEnumerator FreezeCo(float freeze_time = 0.2f)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(freeze_time);
        Time.timeScale = 1f;
    }
}
