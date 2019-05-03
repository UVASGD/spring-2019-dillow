// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour 
{
    public static bool gameIsPaused = false;

    public Selectable defaultCursor;
    public Selectable defaultSaveCursor;

    [Header("Menus")]
    public Animator pauseMenuUI;
    public Animator savesMenu;

    [Header("Control")]
    public Selectable cursor;
    private float buttonDelay;

    AudioLowPassFilter af;
    float filter_level, pause_level = 1000;
    private const float BTN_DELAY = 0.25f;

    private EventSystem ev;
    private bool InSavesMenu => savesMenu? savesMenu.gameObject.activeSelf : false;
    private bool InDialogMenu => GameManager.instance.dialogue.DialogueShown;

    private void Start()
    {
        af = Camera.main.GetComponent<AudioLowPassFilter>();
        filter_level = af.cutoffFrequency;
        ev = FindObjectOfType<EventSystem>();
    }

    private void Update()
    {
       if( Input.GetButtonDown("Pause") )
        {
            if(gameIsPaused)
            {
                if (InDialogMenu)
                    GameManager.instance.dialogue.CloseDialog();
                else
                    Resume();
            }
            else
            {
                Pause();
            }
        }

        if (buttonDelay > 0) {
            buttonDelay -= Time.fixedDeltaTime;
            if (buttonDelay <= 0) buttonDelay = 0;
        }

        HandleInput();
    }

    /// <summary>
    /// handle Input based on pause screen
    /// </summary>
    private void HandleInput() {
        if (gameIsPaused) {
            // horizontal control
            if (buttonDelay == 0) {
                if ((Input.GetAxisRaw(GameManager.HorizontalAxis1) > 0
                    || Input.GetAxisRaw(GameManager.HorizontalAxis2) > 0) && cursor.FindSelectableOnRight()) {
                    Highlight(cursor = cursor.FindSelectableOnRight());
                    buttonDelay = BTN_DELAY;
                } else if ((Input.GetAxisRaw(GameManager.HorizontalAxis1) < 0
                    || Input.GetAxisRaw(GameManager.HorizontalAxis2) < 0) && cursor.FindSelectableOnLeft()) {

                    Highlight(cursor.FindSelectableOnLeft());
                    buttonDelay = BTN_DELAY;
                }

                // vertical control
                if ((Input.GetAxisRaw(GameManager.VerticalAxis1) < 0
                    || Input.GetAxisRaw(GameManager.VerticalAxis2) < 0)) {
                    if (cursor.FindSelectableOnDown()) {
                        
                        Highlight(cursor.FindSelectableOnDown());
                        buttonDelay = BTN_DELAY;
                    }
                } else if ((Input.GetAxisRaw(GameManager.VerticalAxis1) > 0
                    || Input.GetAxisRaw(GameManager.VerticalAxis2) > 0) && cursor.FindSelectableOnUp()) {

                    Highlight(cursor.FindSelectableOnUp());
                    buttonDelay = BTN_DELAY;
                }

                // keyboard controller input for selecting button
                if (cursor && (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))) {
                    if (cursor is Button) {
                        ((Button)cursor).onClick?.Invoke();
                    }
                }
            }
        }
    }

    public void OnMouseOver(Selectable item) {
        ev.SetSelectedGameObject(null);
        cursor = item;
    }

    private void Highlight(Selectable item) {
        ev.SetSelectedGameObject(null);
        cursor = item;
        cursor.Select();
    }

    public void ReturnToMenu() {
        GameManager.StartReturnToMenu += CommitReturn;
        Highlight(GameManager.instance.dialogue.GetFirstButton());
        GameManager.ReturnToMenu();
    }

    public void CommitReturn() {
        GameManager.StartReturnToMenu -= CommitReturn;
        Resume();
    }

    public void Resume()
    {
        pauseMenuUI.SetTrigger("Toggle");
        af.cutoffFrequency = filter_level;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.gameObject.SetActive(true);
        af.cutoffFrequency = pause_level;
        Time.timeScale = 0f;
        gameIsPaused = true;

        buttonDelay = 0;
        cursor = defaultCursor;
        cursor.Select();
    }

    /// <summary>
    /// Opens save file window to allowplayer to choose save file location
    /// </summary>
    public void ShowSaves() {
        //savesMenu.SetBool("Open", true);
        //savesMenu.gameObject.SetActive(true);
        //ev.SetSelectedGameObject(null);
        //cursor = defaultSaveCursor;
        //cursor.Select();

        // TODO: Fix save file window
        if (GameManager.currentSaveFile.Contains("temp")) {
            GameManager.currentSaveFile = "save " + GameManager.fileCount;
        }

        StartCoroutine(GameManager.Save());
        Resume();
    }

    public void HideSaves() {
        //savesMenu.SetBool("Open", false);
        savesMenu.gameObject.SetActive(false);
        ev.SetSelectedGameObject(null);
        cursor = defaultCursor;
        cursor.Select();
    }

    public void LoadSave(FileOption option) {

    }

    public void Quit() {
        Resume();
        GameManager.Quit();
    }

}
