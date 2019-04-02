using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ASE_to_Unity;

public class CollectibleRehasher : EditorWindow {

    static string MAIN_TITLE = "Collectible Rehasher";

    [MenuItem("Tools/Collectible Rehasher")]
    private static void OpenFromMenu() {
        EditorWindow window = EditorWindow.GetWindow(typeof(CollectibleRehasher));
        window.name = MAIN_TITLE;
        window.minSize = new Vector2(400, 100);
        window.titleContent = new GUIContent(window.name);
    }

    void OnGUI() {
        HeaderGUI();
        BodyGUI();
    }

    /// <summary>
    /// Removes all collectibles from the save file and rehashes its ID
    /// </summary>
    void BodyGUI() {
        if (GUILayout.Button("REHASH")) {
            Collectible[] collectibles = FindObjectsOfType<Collectible>();
            foreach (Collectible collectible in collectibles) {
                GameManager.RemoveCollectible(collectible);
                collectible.ResetID();
            }

            GameManager.SaveFromEditor();
        }
    }

    /// <summary>
    /// GUI for our beautiful header :)
    /// </summary>
    void HeaderGUI() {
        GUILayout.Space(16);

        GUIStyle style = new GUIStyle() {
            alignment = TextAnchor.LowerCenter,
            fontSize = 18,
            fontStyle = FontStyle.Bold
        };
        style.normal.textColor = Color.white;
        style.richText = true;
        Rect rect = AseGUILayout.GUIRect(0, 18);

        GUIStyle shadowStyle = new GUIStyle(style) {
            richText = false
        };

        EditorGUI.DropShadowLabel(rect, MAIN_TITLE, shadowStyle);
        GUI.Label(rect, MAIN_TITLE, style);

        GUILayout.Space(15);
    }
}
