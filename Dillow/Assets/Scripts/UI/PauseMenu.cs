// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

﻿using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour 
{
    public static bool gameIsPaused = false;

    [Header("Menus")]
    public Animator pauseMenuUI;

    AudioLowPassFilter af;
    float filter_level, pause_level = 1000;

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
    }

    public void Resume()
    {
        af.cutoffFrequency = filter_level;
        pauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        af.cutoffFrequency = pause_level;
        pauseMenuUI.SetTrigger("Toggle");
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Quit()
    {
        GameManager.Quit();
    }

}
