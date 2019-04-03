using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSpin : MonoBehaviour
{
    public float spin_speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, spin_speed, 0) * Time.deltaTime);
    }
}
