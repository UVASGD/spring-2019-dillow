using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class MainMenuControl : MonoBehaviour {

    public static MainMenuControl instance;

    public enum MenuState {
        Start,
        Options,
        Files,
        NewGame,
        Entry,
    }

    public MenuState menuState;
    private Animator anim;

    [Header("Root Menu Buttons")]
    public ThreeDButton startMenu;
    public ThreeDButton optionMenu;
    public ThreeDButton fileMenu;
    public ThreeDButton newMenu;

    [Header("Control")]
    public bool forceLockInput;
    public ThreeDButton cursor;
    private float buttonDelay;
    private float transitionDelay;

    [Header("Save Menu")]
    public List<FileOption> fileOptions;

    [Header("Defaults")]
    public AudioClip menuSound;
    public AudioClip badSound;
    public AudioClip selectSound;
    public string FirstIsland;

    private const string TEMP_FILE = "temporary.json";

    /// <summary>
    /// used for locking input between viewing transitions
    /// </summary>
    private bool IsLocked => transitionDelay > 0;
    
    /// <summary>
    /// The start button for the current menu
    /// </summary>
    public ThreeDButton Menu {
        get {
            switch (menuState) {
                case MenuState.Options: return optionMenu;
                case MenuState.Files: return fileMenu;
                case MenuState.NewGame: return newMenu;
                default: return startMenu;
            }
        }
    }

    // delay constants
    private const float BTN_DELAY = 0.55f;
    private const float TRA_DELAY = 1.5f;

    private void Awake() {
        if (null == instance) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        instance = this;
        StartCoroutine(LoadSaveFiles());
    }
    
    void Start() {
        anim = GetComponent<Animator>();
        
        menuState = MenuState.Entry;
        cursor = Menu;

        AudioManager.PlayMusic("Main Menu", fadeDuration:10f/3f);
        forceLockInput = true;
        // make sure to unlock input by the time the game has finished fading in
        FadeController.FadeInCompletedEvent += FinishEntry;
    }

    // Update is called once per frame
    void Update() {
        anim.SetInteger("State", (int)menuState);

        // update delayed timers
        if (buttonDelay > 0) {
            buttonDelay -= Time.deltaTime;
            if (buttonDelay <= 0) buttonDelay = 0;
        }

        if (transitionDelay > 0) {
            transitionDelay -= Time.deltaTime;
            if (transitionDelay <= 0) transitionDelay = 0;
        }

        if (!forceLockInput && !IsLocked) HandleInput();
    }


    #region ================= UNIVERSAL =================

    /// <summary>
    /// releases a manual lock
    /// </summary>
    public void ReleaseLock() {
        forceLockInput = false;
    }

    public void FinishEntry() {
        FadeController.FadeInCompletedEvent -= FinishEntry;
        menuState = MenuState.Start;
        cursor.Activate();
        ReleaseLock();
    }

    /// <summary>
    /// move through the menus!
    /// this is handled largely by keyboard/controller input
    /// </summary>
    public void HandleInput() {
        // horizontal control
        if (buttonDelay == 0) {
            var old = cursor;
            if ((Input.GetAxis(GameManager.HorizontalAxis1) > 0
                || Input.GetAxis(GameManager.HorizontalAxis2) > 0) && cursor.right) {
                // deactivate last button
                cursor.Deactivate();
                cursor = cursor.right;
                // activate new button
                cursor.Activate();
                buttonDelay = BTN_DELAY;
            } else if ((Input.GetAxis(GameManager.HorizontalAxis1) < 0
                || Input.GetAxis(GameManager.HorizontalAxis2) < 0) && cursor.left) {
                cursor.Deactivate();
                cursor = cursor.left;
                cursor.Activate();
                buttonDelay = BTN_DELAY;
            }

            // vertical control
            if ((Input.GetAxis(GameManager.VerticalAxis1) < 0
                || Input.GetAxis(GameManager.VerticalAxis2) < 0) && cursor.down) {
                cursor.Deactivate();
                cursor = cursor.down;
                cursor.Activate();
                buttonDelay = BTN_DELAY;
            } else if ((Input.GetAxis(GameManager.VerticalAxis1) > 0
                || Input.GetAxis(GameManager.VerticalAxis2) > 0) && cursor.up) {
                cursor.Deactivate();
                cursor = cursor.up;
                cursor.Activate();
                buttonDelay = BTN_DELAY;
            }

            if(cursor!= old && menuSound)
                AudioManager.instance.PlaySound(menuSound, 1f);
        }

        // activate button
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit")) {
            if(selectSound) AudioManager.instance.PlaySound(selectSound, 1f);
            cursor.Invoke();
        }

    }

    /// <summary>
    /// Switch the submenu; basically triggers the cinemachine to move to
    /// the new state and activates/deactivates the proper buttons
    /// </summary>
    /// <param name="newState"></param>
    private void ChangeState(MenuState newState) {
        cursor.Deactivate();
        menuState = newState;
        cursor = Menu;
        cursor.Activate() ;

        transitionDelay = TRA_DELAY;
    }

    public void BackToStart() {
        ChangeState(MenuState.Start);
    }

    #endregion


    #region ================= OPTIONS =================

    public void OpenOptionsMenu() {
        ChangeState(MenuState.Options);
    }

    #endregion


    #region ================= LOAD MENU =================

    private SaveData dataToLoad;

    /// <summary>
    /// preload all save data files and set the File Options to them;
    /// this is pretty trivial since there is a max number of files
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadSaveFiles() {
        yield return null;

        List<FileInfo> datas = new List<FileInfo>();
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + GameManager.SAVE_FOLDER);
        foreach(FileInfo file in dir.GetFiles()) {
            if (file.Extension.ToLower() == ".json" && !file.Name.Contains("temp"))
                datas.Add(file);
        }

        foreach(FileOption option in fileOptions) {
            if (datas.Count == 0) option.SetAsEmpty();
            else {
                try {
                    SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(datas[0].ToString()));
                    data.fileName = datas[0].FullName;
                    option.SetSaveData(data);
                    datas.RemoveAt(0);
                    GameManager.fileCount++;
                } catch(Exception e) {
                    print("Error on Loading: " + datas[0].ToString());
                    Debug.LogError(e);
                    option.SetAsEmpty();
                    datas.RemoveAt(0);
                }
            }
        }
    }

    public void OpenContinueMenu() {
        ChangeState(MenuState.Files);
    }

    /// <summary>
    /// set the current save data to active,
    /// play the cannon animation
    /// </summary>
    /// <param name="option"></param>
    public void LoadSelectedSaveFile(FileOption option) {
        if (option.data == null) {
            if (badSound) AudioManager.instance.PlaySound(badSound, 1f);
            return;
        }

        forceLockInput = true;
        if (selectSound) AudioManager.instance.PlaySound(selectSound, 1f);
        FadeController.FadeOutCompletedEvent += CommitLoadSave;
        dataToLoad = option.data;
        FadeController.instance.DelayFadeOut(time:1f, speed:1/6f);
        StartCoroutine(DelayedFadeOut(1f, 2f));
        GameManager.currentSaveFile = dataToLoad.fileName;
        StartCoroutine(GameManager.Load());
    }

    /// <summary>
    /// delay fading out the music
    /// </summary>
    private IEnumerator DelayedFadeOut(float delay, float time) {
        yield return new WaitForSeconds(delay);
        AudioManager.PlayMusic("", fadeDuration: time);
    }

    /// <summary>
    /// loads the save file when the animation and fade have completed
    /// </summary>
    public void CommitLoadSave() {
        FadeController.FadeOutCompletedEvent -= CommitLoadSave;

        // load save file and start 
        GameManager.LoadLevel(dataToLoad.currentScene);
    }

    #endregion


    #region ================= NEW SAVE =================

    /// <summary>
    /// Creates a temporary save file 
    /// </summary>
    public void NewSave() {
        ChangeState(MenuState.NewGame);
        FadeController.FadeOutCompletedEvent += CommitNewGame;
        FadeController.instance.DelayFadeOut(time: 1f, speed:1/6f);
        AudioManager.PlayMusic("", fadeDuration: 3f);
        forceLockInput = true;

        int N = Enum.GetValues(typeof(CollectibleType)).Cast<int>().Max() + 1;
        int[] saveCollectibleCounts = new int[N];
        SaveData dat = new SaveData(
            Vector3.zero,
            FirstIsland,
            null,
            new List<ulong>(),
            saveCollectibleCounts,
            new List<int>()
        );

        // TODO: allow the player to choose the icon for their game!
        dat.saveIconIndex = UnityEngine.Random.Range(0, GameManager.instance.saveFileSprites.Count);
        
        string jsonData = JsonUtility.ToJson(dat);
        string filePath = Application.dataPath + GameManager.SAVE_FOLDER + "/"
            + TEMP_FILE;
        File.WriteAllText(filePath, jsonData);

        GameManager.currentSaveFile = TEMP_FILE;
    }

    public void CommitNewGame() {
        FadeController.FadeOutCompletedEvent -= CommitNewGame;

        // Load Scene
        GameManager.LoadLevel(FirstIsland);
    }

    #endregion

    public void Quit() {
        forceLockInput = true;
        GameManager.Quit();
    }
}
