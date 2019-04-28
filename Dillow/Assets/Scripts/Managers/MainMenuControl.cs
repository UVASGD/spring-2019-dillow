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
        Entry,
    }

    public MenuState menuState;
    private Animator anim;

    [Header("Start Menu items")]
    public Animator btnContinue;
    public Animator btnNewGame;
    public Animator btnOptions;
    public Animator btnQuit;

    [Header("Options Menu Items")]
    public Animator btnAudio;
    public Animator btnVisual;
    public Animator btnControls;
    public Animator btnOpBack;

    // These are grid mappings of a buttons
    private List<List<Tuple<Animator, Action>>> startMenuButtons, optionButtons, fileButtons;
    
    [Header("Control")]
    public bool forceLockInput;
    private Vector2 cursor;
    private float buttonDelay;
    private float transitionDelay;

    private bool IsLocked => transitionDelay > 0;

    /// <summary> The cursor boundaries of the current menu </summary>
    public Vector2 CursorBounds => new Vector2(Menu.Count, Menu[(int)cursor.x].Count);
    public List<List<Tuple<Animator, Action>>> Menu {
        get {
            switch (menuState) {
                case MenuState.Options:
                    return optionButtons;
                case MenuState.Files:
                    return fileButtons;
                default: return startMenuButtons;
            }
        }
    }

    private const float BTN_DELAY = 0.35f;
    private const float TRA_DELAY = 1.5f;

    // Start is called before the first frame update
    void Start() {
        cursor = Vector2.zero;
        anim = GetComponent<Animator>();
        startMenuButtons = new List<List<Tuple<Animator, Action>>>();
        optionButtons = new List<List<Tuple<Animator, Action>>>();
        fileButtons = new List<List<Tuple<Animator, Action>>>();

        // instantiate list of 3D buttons
        List<Tuple<Animator, Action>> col = new List<Tuple<Animator, Action>>();
        if (btnContinue) col.Add(new Tuple<Animator, Action>(btnContinue, ContinueLastSave));
        if (btnNewGame) col.Add(new Tuple<Animator, Action>(btnNewGame, NewSave));
        if (btnOptions) col.Add(new Tuple<Animator, Action>(btnOptions, OpenOptionsMenu));
        if (btnQuit) col.Add(new Tuple<Animator, Action>(btnQuit, Quit));
        startMenuButtons.Add(col);

        col = new List<Tuple<Animator, Action>>();
        if (btnAudio) col.Add(new Tuple<Animator, Action>(btnAudio, delegate { /*Do a thing*/ }));
        optionButtons.Add(col);
        
        col = new List<Tuple<Animator, Action>>();
        if (btnVisual) col.Add(new Tuple<Animator, Action>(btnVisual, delegate { /*Do a thing*/ }));
        optionButtons.Add(col);

        col = new List<Tuple<Animator, Action>>();
        if (btnControls) col.Add(new Tuple<Animator, Action>(btnControls, delegate { /*Do a thing*/ }));
        optionButtons.Add(col);

        col = new List<Tuple<Animator, Action>>();
        if (btnOpBack) col.Add(new Tuple<Animator, Action>(btnOpBack, BackToStart));
        optionButtons.Add(col);

        menuState = MenuState.Entry;
        startMenuButtons[0][0].Item1.SetBool("Active", true);
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
            Vector2 old;
            if (Input.GetAxis("Horizontal") > 0) {
                old = new Vector2(cursor.x, cursor.y);
                // deactivate last button
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", false);
                cursor.x = cursor.x < CursorBounds.x - 1 ? cursor.x + 1 : 0;
                // activate last button
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", true);
                if(old!=cursor) buttonDelay = BTN_DELAY;
            } else if (Input.GetAxis("Horizontal") < 0) {
                old = new Vector2(cursor.x, cursor.y);

                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", false);
                cursor.x = cursor.x >= 1 ? cursor.x - 1 : CursorBounds.x - 1;
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", true);
                if (old != cursor) buttonDelay = BTN_DELAY;
            }

            if (Input.GetAxis("Vertical") < 0) {
                old = new Vector2(cursor.x, cursor.y);

                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", false);
                cursor.y = cursor.y < CursorBounds.y - 1 ? cursor.y + 1 : 0;
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", true);
                if (old != cursor) buttonDelay = BTN_DELAY;
            } else if (Input.GetAxis("Vertical") > 0) {
                old = new Vector2(cursor.x, cursor.y);

                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", false);
                cursor.y = cursor.y >= 1 ? cursor.y - 1 : CursorBounds.y - 1;
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", true);
                if (old != cursor) buttonDelay = BTN_DELAY;
            }
        }

        // activate button
        if (Input.GetButtonDown("Jump")) {
            Menu[(int)cursor.x][(int)cursor.y].Item2.Invoke();
        }

    }

    private void ChangeState(MenuState newState) {
        Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", false);
        menuState = newState;
        cursor = Vector2.zero;
        Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", true);

        transitionDelay = TRA_DELAY;
    }

    public void BackToStart() {
        ChangeState(MenuState.Start);
    }

    public void ContinueLastSave() {
        ChangeState(MenuState.Files);

    }

    public void NewSave() {
        //ChangeState(MenuState.Start);

    }

    public void OpenOptionsMenu() {
        ChangeState(MenuState.Options);

    }

    public void Quit() {

    }

    public void CommitQuit() {

    }
}
