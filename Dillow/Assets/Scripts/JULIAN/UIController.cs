using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour {

    public static UIController instance;

    [Header("UI Objects")]
    public Animator GearsAnim;
    public TextMeshProUGUI gearsText;
    private const float TIMEOUT = 5;
    float gearTimeout;



    void Awake() {
        if (instance != null) {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (gearTimeout > 0) {
            gearTimeout -= Time.deltaTime;
            if (gearTimeout <= 0) {
                gearTimeout = 0;
                GearsAnim.SetTrigger("Toggle");
            }
        }
    }

    /// <summary>
    /// Sets the display of gears to new gear count
    /// </summary>
    /// <param name="newVal"></param>
    public void ShowGear(int newVal) {
        if (GearsAnim == null) return;
        gearsText.text = ""+newVal;
        if(gearTimeout==0)
            GearsAnim.SetTrigger("Toggle");
        gearTimeout = TIMEOUT;
    }
}
