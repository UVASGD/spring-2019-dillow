using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerAndEditorGUI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class FoamyWater : MonoBehaviour, IPEGI {

    public Texture2D foamTexture;

    private void Start()
    {
        Shader.SetGlobalTexture("_Foam", foamTexture);
        setFoamDynamics();
    }

    public bool stopTime = false;
    public Vector4 foamParameters;
    float MyTime = 0;
    public float _thickness = 80;
    public float _noise = 0.4f;
    public float _upscale = 1;
    public float shadowStrength = 1;
    public Color shadowColor;

    public void setFoamDynamics()  {
        Shader.SetGlobalVector("_foamDynamics", new Vector4(_thickness, _noise, _upscale, (300-_thickness)));
        shadowColor.a = 1-shadowStrength;
        Shader.SetGlobalColor("_ShadowColor", shadowColor);
    }
    
    void Update () {
        if (!stopTime)
            MyTime += Time.deltaTime;

        foamParameters.x = MyTime; 
        foamParameters.y = MyTime * 0.6f;
        foamParameters.z = transform.position.y;

        Shader.SetGlobalVector("_foamParams", foamParameters);
      

	}

#if !NO_PEGI
    public bool PEGI() {

        bool changed = false;

        changed |= "Thickness:".edit(ref _thickness, 5, 300).nl();
        changed |= "Noise:".edit(ref _noise, 0, 2).nl();
        changed |= "Upscale:".edit(70, ref _upscale, 1, 64).nl();
        changed |= "Shadow color:".edit(ref shadowColor).nl();
        changed |= "Shadow strength:".edit(100, ref shadowStrength, 0.1f, 1).nl();


        bool f = RenderSettings.fog;
        if ("Fog (Recommended)".toggle(ref f).nl())
            RenderSettings.fog = f;

        if (f) {

            Color col = RenderSettings.fogColor;

            if ("Fog Color".edit(ref col).nl())
                RenderSettings.fogColor = col;

        }


        if (changed) {
            setFoamDynamics();
#if UNITY_EDITOR
            if (Application.isPlaying == false)
            {
                SceneView.RepaintAll();
                UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
            }
#endif
        }
        "Water foam mask".edit(ref foamTexture).nl();

        return changed;
    }
#endif
}
