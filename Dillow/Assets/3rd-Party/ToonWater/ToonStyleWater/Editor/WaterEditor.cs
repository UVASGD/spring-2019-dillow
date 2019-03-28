using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;

[CustomEditor(typeof(FoamyWater))]
public class WaterEditor : Editor {
    public override void OnInspectorGUI() => ((FoamyWater)target).Inspect(serializedObject);
}