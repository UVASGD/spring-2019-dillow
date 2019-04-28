using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuControl : MonoBehaviour {

    public enum MenuState { Start, Options, Files, }
    public MenuState menuState;

    [Header("Start Menu items")]
    public Animator btnContinue, btnNewGame, btnOptions, btnQuit;

    // These are grid mappings of a buttons
    private List<List<Tuple<Animator, Action>>> startMenuButtons, optionButtons, fileButtons;

    private Vector2 cursor;
    private float buttonDelay;
    private bool lockInput;

    /// <summary> The cursor boundaries of the current menu </summary>
    public Vector2 CursorBounds => new Vector2(Menu.Count, Menu[(int)cursor.x].Count);
    public List<List<Tuple<Animator, Action>>> Menu {
        get {
            switch (menuState) {
                case MenuState.Start:
                    return startMenuButtons;
                case MenuState.Options:
                    return optionButtons;
                case MenuState.Files:
                    return fileButtons;
                default: return null;
            }
        }
    }

    private const float BTN_DELAY = 0.5f;

    // Start is called before the first frame update
    void Start() {
        cursor = Vector2.zero;

        startMenuButtons = new List<List<Tuple<Animator, Action>>>();

        // instantiate list of 3D buttons
        List<Tuple<Animator, Action>> col = new List<Tuple<Animator, Action>>();
        if (btnContinue) col.Add(new Tuple<Animator, Action>(btnContinue, ContinueLastSave));
        if (btnNewGame) col.Add(new Tuple<Animator, Action>(btnNewGame, NewSave));
        if (btnOptions) col.Add(new Tuple<Animator, Action>(btnOptions, OpenOptionsMenu));
        if (btnQuit) col.Add(new Tuple<Animator, Action>(btnQuit, Quit));

        menuState = MenuState.Start;
    }

    // Update is called once per frame
    void Update() {
        if (buttonDelay > 0) {
            buttonDelay -= Time.deltaTime;
            if (buttonDelay <= 0) buttonDelay = 0;
        }

        if (!lockInput) HandleInput();
    }

    public void ReleaseLock() {
        lockInput = false;
    }

    /// <summary>
    /// move through the menus!
    /// </summary>
    public void HandleInput() {
        // navigate the menu
        if (buttonDelay == 0) {
            if (Input.GetAxis("Horizontal") > 0) {
                // deactivate last button
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", false);
                cursor.x = cursor.x < CursorBounds.x - 2 ? cursor.x + 1 : 0;
                // activate last button
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", true);
                buttonDelay = BTN_DELAY;
            } else if (Input.GetAxis("Horizontal") < 0) {
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", false);
                cursor.x = cursor.x > 1 ? cursor.x - 1 : CursorBounds.x - 1;
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", true);
                buttonDelay = BTN_DELAY;
            }

            if (Input.GetAxis("Vertical") > 0) {
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", false);
                cursor.y = cursor.y < CursorBounds.y - 2 ? cursor.y + 1 : 0;
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", true);
                buttonDelay = BTN_DELAY;
            } else if (Input.GetAxis("Vertical") < 0) {
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", false);
                cursor.y = cursor.y > 1 ? cursor.y - 1 : CursorBounds.y - 1;
                Menu[(int)cursor.x][(int)cursor.y].Item1.SetBool("Active", true);
                buttonDelay = BTN_DELAY;
            }
        }

        if (Input.GetButtonDown("")) {
            switch (menuState) {
                case MenuState.Start:
                    startMenuButtons[(int)cursor.x][(int)cursor.y].Item2.Invoke();
                    break;
            }
        }

    }

    public void ContinueLastSave() {

    }

    public void NewSave() {

    }

    public void OpenOptionsMenu() {

    }

    public void Quit() {

    }

    public void CommitQuit() {

    }
}
