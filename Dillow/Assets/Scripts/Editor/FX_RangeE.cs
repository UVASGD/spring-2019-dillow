using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ASE_to_Unity;
using System.Reflection;

[CustomEditor(typeof(Fx_Object_Ranged))]
public class FX_RangeE : Editor {

    bool foldout = true;
    Fx_Object_Ranged fx;


    void OnEnable() {
        fx = (Fx_Object_Ranged)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        serializedObject.Update();

        var serProp = serializedObject.GetIterator();
        while (serProp.NextVisible(true)) {
            switch (serProp.name.ToLower()) {
                case "m_script":
                    break;
                case "sounds":
                    Show(serProp);
                    break;
                case "vol":
                case "mixergroup":
                case "range":
                    EditorGUILayout.PropertyField(serProp);
                    break;
                default:
                    //Debug.Log(serProp.name);
                    //EditorGUILayout.PropertyField(serProp);
                    break;
            }
        }
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

    public void Show(SerializedProperty list) {
        if (foldout = AseGUILayout.BeginFold(foldout, list.name)) {
            for (int i = 0; i < list.arraySize; i++) {
                EditorGUILayout.BeginHorizontal();
                var property = list.GetArrayElementAtIndex(i);
                SoundRange sndRange = (SoundRange)GetTargetObjectOfProperty(property);
                string[] ill = ACList.audioClips.Keys.ToArray();
                int init = -1;
                if (!string.IsNullOrEmpty(sndRange.sound)){
                    for (int s = 0; s < ACList.audioClips.Count;s++) {
                        if (ill[s].Equals(sndRange.sound)) {
                            init = s; break;
                        }
                    }
                }

                int temp = EditorGUILayout.Popup(init, ill);
                if (temp != init) {
                    sndRange.sound = temp >= 0 ? ill[temp] : "";
                    list.InsertArrayElementAtIndex(list.arraySize);
                    list.DeleteArrayElementAtIndex(list.arraySize - 1);
                }
                sndRange.threshold = EditorGUILayout.FloatField(sndRange.threshold);
                fx.sounds[i] = sndRange;
                
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+") && list.arraySize < 99) {
                list.InsertArrayElementAtIndex(list.arraySize);
            }
            if (GUILayout.Button("-") && list.arraySize > 1) {
                list.DeleteArrayElementAtIndex(list.arraySize - 1);
            }
            EditorGUILayout.EndHorizontal();
        }
    }


    /// <summary>
    /// Gets the object the property represents.
    /// </summary>
    /// <param name="prop"></param>
    /// <returns></returns>
    public static object GetTargetObjectOfProperty(SerializedProperty prop) {
        if (prop == null) return null;

        var path = prop.propertyPath.Replace(".Array.data[", "[");
        object obj = prop.serializedObject.targetObject;
        var elements = path.Split('.');
        foreach (var element in elements) {
            if (element.Contains("[")) {
                var elementName = element.Substring(0, element.IndexOf("["));
                var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                obj = GetValue_Imp(obj, elementName, index);
            } else {
                obj = GetValue_Imp(obj, element);
            }
        }
        return obj;
    }

    private static object GetValue_Imp(object source, string name) {
        if (source == null)
            return null;
        var type = source.GetType();

        while (type != null) {
            var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (f != null)
                return f.GetValue(source);

            var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (p != null)
                return p.GetValue(source, null);

            type = type.BaseType;
        }
        return null;
    }

    private static object GetValue_Imp(object source, string name, int index) {
        var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
        if (enumerable == null) return null;
        var enm = enumerable.GetEnumerator();
        //while (index-- >= 0)
        //    enm.MoveNext();
        //return enm.Current;

        for (int i = 0; i <= index; i++) {
            if (!enm.MoveNext()) return null;
        }
        return enm.Current;
    }

}
