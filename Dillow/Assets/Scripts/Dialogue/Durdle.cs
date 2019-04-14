using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(AudioSource))]
public class Durdle : MonoBehaviour
{

    AudioSource source;
    public Flowchart chart;

    bool forceScream = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        print(gameObject.GetAnyComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        if (forceScream)
        {
            source.volume = 1f;
        }
        else {
            source.volume = (Vector3.Angle(transform.up, Vector3.up) / 180f);
        }
        if (source.volume > 0.2f)
        {
            chart.SetBooleanVariable("screaming", true);
        }
        else {
            chart.SetBooleanVariable("screaming", false);
        }

    }

    public void Scream()
    {
        forceScream = true;
    }
}
