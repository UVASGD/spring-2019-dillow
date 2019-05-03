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
        icon.sprite = MainMenuControl.instance.saveFileSprites[data.saveIconIndex];
        levelText.text = data.currentScene;
        fileText.text = data.fileName;
    }

    public void SetAsEmpty() {
        icon.sprite = MainMenuControl.instance.emptyFileSprite;
        levelText.text = "Empty";
        //fileText.text =
    }
}
