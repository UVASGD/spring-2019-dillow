using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    public static DialogueManager instance {
        get {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DialogueManager>();
            }
            return _instance;
        }
    }

    public Flowchart flowchart;

    // Start is called before the first frame update
    void Start()
    {
        flowchart = GetComponent<Flowchart>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
