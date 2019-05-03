using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FileOption : MonoBehaviour {

    [Header("Objects")]
    public Image icon;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI fileText;
    public SaveData data;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void SetSaveData(SaveData dat) {
        data = dat;
        icon.sprite = GameManager.instance.saveFileSprites[data.saveIconIndex];
        levelText.text = data.currentScene;
        var key = data.fileName.Contains("/") ? "/" : "\\";
        int d = data.fileName.Length - data.fileName.IndexOf(".json")-1;
        fileText.text = data.fileName.Substring(data.fileName.LastIndexOf(key) + 1, d);
    }

    public void SetAsEmpty() {
        icon.sprite = GameManager.instance.emptyFileSprite;
        levelText.text = "Empty";
    }
}
