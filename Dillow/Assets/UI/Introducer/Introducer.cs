using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 has_moved
 has_movedCamera
 has_jumped
 has_dashed
 has_interacted
 has_locked
 has_swapLocked
*/

public class Introducer : MonoBehaviour
{
    public static Introducer instance;

    public bool RESET_MODE = true;

    //using a tuple to maintain order of entries
    List<Tuple<string, string>> intro_messages = new List<Tuple<string, string>>() 
    {
        new Tuple<string, string>("has_moved", "WASD to move"),
        new Tuple<string, string>("has_movedCamera", "ARROW KEYS to move camera"),
        new Tuple<string, string>("has_jumped", "SPACE to jump"),
        new Tuple<string, string>("has_dashed", "MOVE and SHIFT to dash/curl up"),
        new Tuple<string, string>("has_interacted", "F to interact"),
        new Tuple<string, string>("has_locked", "Q to lock on/off"),
        new Tuple<string, string>("has_swapLocked", "E to swap locked target"),
        new Tuple<string, string>("has_paused", "ESC/P to pause"),
    };
    Dictionary<string, string> intro_dict = new Dictionary<string, string>();
    bool interact_detected, movedCamera_detected, pause_detected; //this is an anomalous button, so we need a special flag

    float message_time = 5f, wait_time = 3f;
    bool ready;

    IEnumerator Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        foreach (Tuple<string, string> t in intro_messages)
        {
            if (RESET_MODE)
                PlayerPrefs.SetInt(t.Item1, 0);
            if (PlayerPrefs.GetInt(t.Item1, 0) == 0)
                intro_dict.Add(t.Item1, t.Item2);
        }

        while (!DillowController.instance.body.ready)
            yield return null;
        ready = true;
        DillowController.instance.body.MoveEvent += OnMoved;

        DisplayMessages();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ready)
            return;
        if (!interact_detected && Input.GetButtonDown("Interact"))
        {
            RemoveKey("has_interacted");
        }
        if (!movedCamera_detected && (Mathf.Abs(Input.GetAxisRaw("Camera Y")) > 0 || (Mathf.Abs(Input.GetAxisRaw("Camera X")) > 0)))
        {
            RemoveKey("has_movedCamera");
        } 
        if (!pause_detected && Input.GetButtonDown("Pause"))
        {
            RemoveKey("has_paused");
        }

        if (intro_dict.Count == 0)
        {
            DillowController.instance.body.MoveEvent -= OnMoved;
            gameObject.SetActive(false);
        }
    }

    void OnMoved(bool move, Vector3 dir, int jump, int action, int lockon, int lockswap)
    {
        if (move)
            RemoveKey("has_moved");
        if (jump == 2)
            RemoveKey("has_jumped");
        if (action == 2)
            RemoveKey("has_dashed");
        if (lockon == 2)
            RemoveKey("has_locked");
        if (lockswap == 2)
            RemoveKey("has_swapLocked");
    }

    void RemoveKey(string key)
    {
        if (intro_dict.ContainsKey(key))
        {
            intro_dict.Remove(key);
            PlayerPrefs.SetInt(key, 1);
        }
    }

    void DisplayMessages()
    {
        foreach (Tuple<string, string> t in intro_messages)
        {
            if (intro_dict.ContainsKey(t.Item1))
            {
                StartCoroutine(Messager.instance.DisplayMessage(t.Item2));
            }
        }
    }
}
