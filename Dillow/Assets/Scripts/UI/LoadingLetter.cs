using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LoadingLetter : MonoBehaviour {

    public Image fadeImage;

    private TextMeshProUGUI txt;

    // Start is called before the first frame update
    void Start() {
        txt = GetComponent<TextMeshProUGUI>();
        GameManager.StartSceneChange += FixColor;
    }

    /// <summary>
    /// Ensure that the text color can be seen against the background
    /// </summary>
    public void FixColor() {
        System.Drawing.Color col = System.Drawing.Color.FromArgb(
            (int)(fadeImage.color.r*255), (int)(fadeImage.color.g * 255), 
            (int)(fadeImage.color.b * 255));
        txt.color = col.GetBrightness() > 0.5f ? Color.black : Color.white;
    }
}
