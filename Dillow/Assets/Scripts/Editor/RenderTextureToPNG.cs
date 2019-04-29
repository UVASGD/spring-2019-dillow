using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RTTPNG))]
public class RenderTextureToPNG : Editor {

    private string SaveLoc => Application.dataPath + "/p.png";

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        
        if(GUILayout.Button("Convert RT to PNG", GUILayout.Height(32))) {
            MakeSquarePngFromVirtual();
        }
    }

    public void MakeSquarePngFromVirtual() {
        // capture the virtuCam and save it as a square PNG.
        RTTPNG rt = (RTTPNG)target;
        Camera vCam = rt.virtualCamera;

        int sqr = vCam.targetTexture ? vCam.targetTexture.width : 512;
        vCam.aspect = 1.0f;
        // recall that the height is now the "actual" size from now on
        
        // the 24 can be 0,16,24, formats like
        // RenderTextureFormat.Default, ARGB32 etc.
        
        vCam.Render();
    
        RenderTexture.active = vCam.targetTexture? vCam.targetTexture : new RenderTexture(sqr, sqr, 24);
        Texture2D virtualPhoto = new Texture2D(sqr, sqr, rt.format, false);
        //Texture2D virtualPhoto =  new Texture2D(sqr, sqr);
        // false, meaning no need for mipmaps
        virtualPhoto.ReadPixels(new Rect(0, 0, sqr, sqr), 0, 0);

        RenderTexture.active = null; //can help avoid errors 
        //vCam.targetTexture = null;
        // consider ... Destroy(tempRT);

        byte[] bytes;
        bytes = virtualPhoto.EncodeToPNG();

        System.IO.File.WriteAllBytes(
            SaveLoc, bytes);
    }


}