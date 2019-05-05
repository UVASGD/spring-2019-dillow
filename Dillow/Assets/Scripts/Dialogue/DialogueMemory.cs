using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogueMemory : MonoBehaviour
{
    Flowchart flowchart;

    DialogueManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = DialogueManager.instance;
        Flowchart chart = manager.flowchart;
        List<Variable> variables = chart.Variables;

        foreach (Variable variable in variables) {
            string variableName = variable.Key;
            GrabDat(variableName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GrabDat(string _name)
    {
        string type = _name.Substring(0,3);
        string name = _name.Substring(4);

        Debug.Log("type: " + type + " ; name: " + name);
    }
}
