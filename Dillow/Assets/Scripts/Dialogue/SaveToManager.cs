using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Variable",
    "Save To Manager",
    "Saves a value to the Dialogue Manager.")]
public class SaveToManager : Command
{
    public string varName = "";
    public bool isDone = false;
    Flowchart masterChart;
    
    void Start()
    {
        masterChart = DialogueManager.instance.flowchart;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        masterChart.SetBooleanVariable(varName, isDone);

        Continue();
    }
}
