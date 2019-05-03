// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour 
{
    public static bool gameIsPaused = false;

    public Selectable defaultCursor;
    public Selectable defaultSaveCursor;

    [Header("Menus")]
    public Animator pauseMenuUI;
    public Animator savesMenu;

    private Selectable cursor;
    private float buttonDelay;

    AudioLowPassFilter af;
    float filter_level, pause_level = 1000;
    private const float BTN_DELAY = 0.55f;

    private bool InSavesMenu => savesMenu? savesMenu.gameObject.activeSelf : false;

    private void Start()
    {
        af = Camera.main.GetComponent<AudioLowPassFilter>();
        filter_level = af.cutoffFrequency;
    }

    private void Update()
    {
       if( Input.GetButtonDown("Pause") )
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // handle Input based on pause Input
        if (gameIsPaused) {
            // horizontal control
            if (buttonDelay == 0) {
                var old = cursor;
                if ((Input.GetAxis(GameManager.HorizontalAxis1) > 0
                    || Input.GetAxis(GameManager.HorizontalAxis2) > 0) && cursor.FindSelectableOnRight()) {
                    cursor = cursor.FindSelectableOnRight();
                    cursor.Select();
                    buttonDelay = BTN_DELAY;
                } else if ((Input.GetAxis(GameManager.HorizontalAxis1) < 0
                    || Input.GetAxis(GameManager.HorizontalAxis2) < 0) && cursor.FindSelectableOnLeft()) {
                    cursor = cursor.FindSelectableOnLeft();
                    cursor.Select();
                    buttonDelay = BTN_DELAY;
                }

                // vertical control
                if ((Input.GetAxis(GameManager.VerticalAxis1) < 0
                    || Input.GetAxis(GameManager.VerticalAxis2) < 0) && cursor.FindSelectableOnDown()) {
                    cursor = cursor.FindSelectableOnDown();
                    cursor.Select();
                    buttonDelay = BTN_DELAY;
                } else if ((Input.GetAxis(GameManager.VerticalAxis1) > 0
                    || Input.GetAxis(GameManager.VerticalAxis2) > 0) && cursor.FindSelectableOnUp()) {
                    cursor = cursor.FindSelectableOnUp();
                    cursor.Select();
                    buttonDelay = BTN_DELAY;
                }

                // keyboard controller input for selecting button
                if (cursor && (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))) {
                    if (cursor is Button)
                        ((Button)cursor).onClick?.Invoke();
                }
            }
        }
    }

    public void OnMouseOver(Selectable item) {
        cursor = item;
    }

    public void ReturnToMenu() {
        GameManager.StartReturnToMenu += CommitReturn;
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

        cursor = defaultCursor;
        cursor.Select();
    }

    public void ShowSaves() {
        savesMenu.SetBool("Open", true);
        cursor = defaultSaveCursor;
        cursor.Select();
    }

    public void HideSaves() {
        savesMenu.SetBool("Open", false);
        cursor = defaultCursor;
        cursor.Select();
    }

    public void LoadSave(FileOption option) {

    }

    public void Quit()
    {
        GameManager.Quit();
    }

}
