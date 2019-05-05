using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Island), true)]
public class IslandEditor : Editor {

    /// <summary>
    /// Allows developer to choose a scene asset as the next scene
    /// </summary>
    public override void OnInspectorGUI() {
        var ss = target as Island;
        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(ss.NextLevel);

        serializedObject.Update();

        var serProp = serializedObject.GetIterator();
        while (serProp.NextVisible(true)) {
            switch (serProp.name.ToLower()) {
                case "m_script":
                    break;
                case "nextlevel":
                    EditorGUI.BeginChangeCheck();
                    var newScene = EditorGUILayout.ObjectField("Next Island", oldScene, typeof(SceneAsset), false) as SceneAsset;

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