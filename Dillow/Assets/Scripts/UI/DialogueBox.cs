using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class DialogueBox : MonoBehaviour {
    private DialogBoxData _data;

    private Queue<DialogBoxData> _queue;

    public Button Button1;
    public Button Button2;
    public Button Button3;

    public TMP_InputField Input;
    public TextMeshProUGUI PlaceholderText;
    public TextMeshProUGUI Text;
    private bool _wasDialogShown;

    private bool HasSubscribedToEvents;

    // Use this for initialization
    private void Awake() {
        _queue = new Queue<DialogBoxData>();

        // Disable itself
        if (!_wasDialogShown)
            gameObject.SetActive(false);
    }

    public void SubscribeToEvents() {
        if (HasSubscribedToEvents)
            return;

        HasSubscribedToEvents = true;

        GameManager.Events.Subscribe(GameManager.EventNames.ShowDialogBox, 
            OnDialogEvent);
    }

    private void AfterHandlingClick() {
        if (_queue.Count > 0) {
            ShowDialog(_queue.Dequeue());
            return;
        }

        CloseDialog();
    }

    public void OnLeftClick() {
        if (_data.LeftButtonAction != null)
            _data.LeftButtonAction.Invoke();

        AfterHandlingClick();
    }

    public void OnMiddleClick() {
        if (_data.MiddleButtonAction != null)
            _data.MiddleButtonAction.Invoke();

        AfterHandlingClick();
    }

    public void OnRightClick() {
        if (_data.RightButtonAction != null)
            _data.RightButtonAction.Invoke();

        if (_data.ShowInput)
            _data.InputAction.Invoke(Input.text);

        AfterHandlingClick();
    }

    public void ShowDialog(DialogBoxData data) {
        _wasDialogShown = true;
        ResetAll();

        _data = data;

        if ((data == null) || (data.Message == null))
            return;

        // Show the dialog box
        gameObject.SetActive(true);

        Text.text = data.Message;

        var buttonCount = 0;

        if (!string.IsNullOrEmpty(data.LeftButtonText)) {
            // Setup Left button
            Button1.gameObject.SetActive(true);
            Button1.GetComponentInChildren<TextMeshProUGUI>().text = data.LeftButtonText;
            buttonCount++;
        }

        if (!string.IsNullOrEmpty(data.MiddleButtonText)) {
            // Setup Middle button
            Button2.gameObject.SetActive(true);
            Button2.GetComponentInChildren<TextMeshProUGUI>().text = data.MiddleButtonText;
            buttonCount++;
        }

        if (!string.IsNullOrEmpty(data.RightButtonText)) {
            // Setup Right button
            Button3.gameObject.SetActive(true);
            Button3.GetComponentInChildren<TextMeshProUGUI>().text = data.RightButtonText;
        } else if (buttonCount == 0) {
            // Add a default "Close" button if there are no other buttons in the dialog
            Button3.gameObject.SetActive(true);
            Button3.GetComponentInChildren<TextMeshProUGUI>().text = "Close";
        }

        if (data.ShowInput) {
            Input.gameObject.SetActive(true);
            Input.text = data.DefaultInputText;
        }

        transform.SetAsLastSibling();
    }

    private void OnDialogEvent(object arg1, object arg2) {
        var data = arg1 as DialogBoxData;

        if (gameObject.activeSelf) {
            // We're already showing something
            // Display later by adding to queue
            _queue.Enqueue(data);
            return;
        }

        ShowDialog(data);
    }

    private void ResetAll() {
        Button1.gameObject.SetActive(false);
        Button2.gameObject.SetActive(false);
        Button3.gameObject.SetActive(false);
        Input.gameObject.SetActive(false);
    }

    public void CloseDialog() {
        gameObject.SetActive(false);
    }
}

public class DialogBoxData {
    public string DefaultInputText = "";
    public Action<string> InputAction;
    public Action LeftButtonAction;

    public string LeftButtonText;
    public Action MiddleButtonAction;

    public string MiddleButtonText;
    public Action RightButtonAction;

    public string RightButtonText = "Close";

    public bool ShowInput;

    public DialogBoxData(string message) {
        Message = message;
    }

    public string Message { get; private set; }

    public static DialogBoxData CreateError(string message) {
        return new DialogBoxData(message);
    }

    public static DialogBoxData CreateInfo(string message) {
        return new DialogBoxData(message);
    }

    public static DialogBoxData CreateTextInput(string message, Action<string> onComplete,
        string rightButtonText = "OK") {
        var data = new DialogBoxData(message);
        data.ShowInput = true;
        data.RightButtonText = rightButtonText;
        data.InputAction = onComplete;
        data.LeftButtonText = "Close";
        return data;
    }
}
