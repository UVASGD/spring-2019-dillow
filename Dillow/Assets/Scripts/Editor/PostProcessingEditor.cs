using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEditor;

public class PostProcessingEditor : MonoBehaviour {

	[MenuItem("Tools/Post-Processing/Toggle %#t")]
	public static void Toggle () {
		PostProcessLayer layer;
		if (null != (layer = FindObjectOfType<PostProcessLayer>())) {
			layer.enabled = !layer.enabled;
		}
	}
}
