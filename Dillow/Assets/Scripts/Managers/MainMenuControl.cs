using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MainMenuControl : MonoBehaviour {

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

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        
        menuState = MenuState.Entry;
        cursor = Menu;
        cursor.Activate();

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

    public void ReleaseLock() {
        forceLockInput = false;
    }

    public void FinishEntry() {
        FadeController.FadeInCompletedEvent -= FinishEntry;
        menuState = MenuState.Start;
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
        if (Input.GetButtonDown("Jump")) {
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

    public void OpenContinueMenu() {
        ChangeState(MenuState.Files);
    }

    public void OpenOptionsMenu() {
        ChangeState(MenuState.Options);
    }

    public void LoadSelectedSaveFile() {
        FadeController.FadeOutCompletedEvent += CommitLoadSave;
        FadeController.instance.FadeOut();
    }

    public void CommitLoadSave() {
        FadeController.FadeOutCompletedEvent -= CommitLoadSave;

        // load save file and start 
    }

    public void NewSave() {
        ChangeState(MenuState.NewGame);
        FadeController.FadeOutCompletedEvent += CommitNewGame;
        FadeController.instance.DelayFadeOut(time: 0.5f);
        forceLockInput = true;
    }

    public void CommitNewGame() {
        FadeController.FadeOutCompletedEvent -= CommitNewGame;

        // create a new save
    }

    public void Quit() {
        forceLockInput = true;
        FadeController.FadeOutCompletedEvent = CommitQuit;
        FadeController.instance.FadeOut(Color.black);
    }

    public void CommitQuit() {
        print("hufehiugfrhigfdsijefrsijgdrsij");
        Application.Quit();
    }
}
