using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LoadingLetter : MonoBehaviour {

    public Image fadeImage;

    private Text txt;

    // Start is called before the first frame update
    void Start() {
        txt = GetComponent<Text>();
        //GameManager.StartSceneChange += FixColor;
    }

    /// <summary>
    /// Ensure that the text color can be seen against the background
    /// </summary>
    public void FixColor() {
        //Color col = Color.(
        //    (int)(fadeImage.color.r*255), (int)(fadeImage.color.g * 255), 
        //    (int)(fadeImage.color.b * 255));
        //txt.color = col.maxColorComponent > 0.5f ? Color.black : Color.white;
    }
}
