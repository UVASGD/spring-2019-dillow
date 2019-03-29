using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(FadeController))]
public class FadeControllerE : Editor {

    public override void OnInspectorGUI() {
        var fc = target as FadeController;
        var root = PrefabUtility.GetNearestPrefabInstanceRoot(fc);
        serializedObject.Update();

        var serProp = serializedObject.GetIterator();
        while (serProp.NextVisible(true)) {
            switch (serProp.name.ToLower()) {
                case "m_script":
                    break;
                case "speed":
                    EditorGUI.BeginChangeCheck();
                    float speed = EditorGUILayout.FloatField("Speed", fc.speed);

                    if (EditorGUI.EndChangeCheck()) {
                        fc.speed = speed;
   
                        if(root != null)
                            PrefabUtility.UnpackPrefabInstance(root, PrefabUnpackMode.Completely, 
                                InteractionMode.AutomatedAction);
                        fc.anim.SetFloat("Speed", speed);
                    }
                    break;
                case "fadecolor":
                    EditorGUI.BeginChangeCheck();
                    Color color = EditorGUILayout.ColorField("Fade Color", fc.fadeColor);

                    if (EditorGUI.EndChangeCheck()) {
                        fc.fadeColor = color;
                        if(fc.fadeImage != null) {
                            fc.fadeImage.color = color;

                            if (root != null)
                                PrefabUtility.UnpackPrefabInstance(root, PrefabUnpackMode.Completely, 
                                InteractionMode.AutomatedAction);
                            EditorSceneManager.MarkSceneDirty(fc.gameObject.scene);
                        }
                    }
                    break;
                default:
                    EditorGUILayout.PropertyField(serProp);
                    break;
            }
        }
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

}
