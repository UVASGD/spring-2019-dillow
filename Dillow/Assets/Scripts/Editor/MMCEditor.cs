using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MainMenuControl), true)]
public class MMCEditor : Editor {

    /// <summary>
    /// Allows developer to choose a scene asset as the next scene
    /// </summary>
    public override void OnInspectorGUI() {
        var ss = target as MainMenuControl;
        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(ss.FirstIsland);

        serializedObject.Update();

        var serProp = serializedObject.GetIterator();
        while (serProp.NextVisible(true)) {
            switch (serProp.name.ToLower()) {
                case "m_script":
                    break;
                case "firstisland":
                    EditorGUI.BeginChangeCheck();
                    var newScene = EditorGUILayout.ObjectField("First Island", oldScene, typeof(SceneAsset), false) as SceneAsset;

                    if (EditorGUI.EndChangeCheck()) {
                        var newPath = AssetDatabase.GetAssetPath(newScene);
                        serProp.stringValue = newPath;
                    }
                    break;
                default:
                    EditorGUILayout.PropertyField(serProp);
                    break;
            }
        }


        serializedObject.ApplyModifiedProperties();
    }

}