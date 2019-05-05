using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Variable",
    "Load From Manager",
    "Loads a value into a variable from the Dialogue Manager.")]
public class LoadFromManager : Command
{
    public string varName = "";

    Flowchart masterChart;
    Flowchart thisChart;

    // Start is called before the first frame update
    void Start()
    {
        masterChart = DialogueManager.instance.flowchart;
        thisChart = GetFlowchart();
    }

    public override void OnEnter()
    {
        base.OnEnter();

        bool val;
        try
        {
            val = masterChart.GetBooleanVariable(varName);
        }
        catch {
            val = false;
        }
        thisChart.SetBooleanVariable(varName, val);

        Continue();
    }
}
