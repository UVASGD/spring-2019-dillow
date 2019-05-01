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

    [Header("Control")]
    public bool forceLockInput;
    private ThreeDButton cursor;
    private float buttonDelay;
    private float transitionDelay;

    [Header("Save Menu")]
    public List<FileOption> fileOptions;

    private int fileCount;

    [Header("Defaults")]
    public Scene FirstIsland;
    public Sprite emptyFileSprite;
    public List<Sprite> saveFileSprites;

    private bool IsLocked => transitionDelay > 0;
    
    public ThreeDButton Menu {
        get {
            switch (menuState) {
                case MenuState.Options: return optionMenu;
                case MenuState.Files: return fileMenu;
                default: return startMenu;
            }
        }
    }

    private const float BTN_DELAY = 0.35f;
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

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        
        menuState = MenuState.Entry;
        cursor = Menu;

        forceLockInput = true;
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
        // navigate the menu
        if (buttonDelay == 0) {
            if (Input.GetAxis("Horizontal") > 0 && cursor.right) {
                // deactivate last button
                cursor.Deactivate();
                cursor = cursor.right;
                // activate new button
                cursor.Activate();
                buttonDelay = BTN_DELAY;
            } else if (Input.GetAxis("Horizontal") < 0 && cursor.left) {
                cursor.Deactivate();
                cursor = cursor.left;
                cursor.Activate();
                buttonDelay = BTN_DELAY;
            }

            if (Input.GetAxis("Vertical") < 0 && cursor.down) {
                cursor.Deactivate();
                cursor = cursor.down;
                cursor.Activate();
                buttonDelay = BTN_DELAY;
            } else if (Input.GetAxis("Vertical") > 0 && cursor.up) {
                cursor.Deactivate();
                cursor = cursor.up;
                cursor.Activate();
                buttonDelay = BTN_DELAY;
            }
        }

        // activate button
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit")) {
            cursor.Invoke();
        }

    }

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

    public IEnumerator LoadSaveFiles() {
        yield return null;


        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + GameManager.SAVE_FOLDER);
        foreach(FileInfo file in dir.GetFiles()) {
            //if(file.Extension.ToLower()!= "")
            print(file.Extension);
        }
    }

    public void OpenContinueMenu() {
        ChangeState(MenuState.Files);
    }

    public void LoadSelectedSaveFile(SaveData dat) {
        FadeController.FadeOutCompletedEvent += CommitLoadSave;
        dataToLoad = dat;
        FadeController.instance.DelayFadeOut(time:1f);
    }

    public void CommitLoadSave() {
        FadeController.FadeOutCompletedEvent -= CommitLoadSave;

        // load save file and start 
    }

    #endregion


    #region ================= NEW SAVE =================

    public void NewSave() {
        ChangeState(MenuState.NewGame);
        FadeController.FadeOutCompletedEvent += CommitNewGame;
        FadeController.instance.DelayFadeOut(time: 1f, speed:1/6f);
        forceLockInput = true;

        int N = Enum.GetValues(typeof(CollectibleType)).Cast<int>().Max() + 1;
        int[] saveCollectibleCounts = new int[N];
        SaveData dat = new SaveData(
            Vector3.zero,
            FirstIsland.name,
            null,
            new List<ulong>(),
            saveCollectibleCounts,
            new List<int>()
        );

        string jsonData = JsonUtility.ToJson(dat);
    }

    public void CommitNewGame() {
        FadeController.FadeOutCompletedEvent -= CommitNewGame;

        // Initialize Proper GameKit


        // Load Scene
        SceneManager.LoadScene(FirstIsland.name);
    }

    #endregion


    #region ================= QUIT =================

    public void Quit() {
        forceLockInput = true;
        FadeController.FadeOutCompletedEvent = CommitQuit;
        FadeController.instance.FadeOut(Color.black);
    }

    public void CommitQuit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
        Application.Quit();
#endif
    }

    #endregion
}
