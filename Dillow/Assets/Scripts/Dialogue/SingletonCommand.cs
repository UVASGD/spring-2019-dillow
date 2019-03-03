using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Variable",
    "Load From Singleton",
    "Loads a value into a variable from a singleton class variable.")]
public class LoadFronSingleton : Command
{


    public override void OnEnter() {


        Continue();
    }
}
