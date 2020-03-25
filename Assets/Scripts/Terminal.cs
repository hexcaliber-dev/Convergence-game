using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Terminal User Input
// When they click enter, text gets logged and
// can be interpreted
public class Terminal : MonoBehaviour {

    public TMP_InputField input;
    public TMP_Text output, fileNameText;
    public ScrollRect scroll;
    const int MAX_LINES = 12;
    public List<TextAsset> userAvailableLogs; // List of available logs to the 'ls' command.
    const float SCROLL_BUTTON_SENSITIVITY = 0.1f;
    const int TEXT_HEIGHT = 18; // how much should be added 
    const string DEFAULT_FILEDIR = "/home/neuros"; // what should be printed in filename text when no files are open

    /// The currently selected command. Used for up-arrow history completion.
    private LinkedListNode<string> currCommand;

    public LinkedList<string> history; // queue of terminal inputs

    // Start is called before the first frame update
    void Start () {
        history = new LinkedList<string> ();
        history.AddFirst ("SENTINEL");
        currCommand = history.First;
        fileNameText.text = DEFAULT_FILEDIR;
        input.onSubmit.AddListener (delegate { SubmitCommand (); });
    }

    // Update is called once per frame
    void Update () {
        if (EventSystem.current.currentSelectedGameObject == input.gameObject) {
            if (history.First == currCommand) {
                history.First.Value = input.text;
            }
            if (Input.GetKeyDown (KeyCode.UpArrow)) {
                if (currCommand.Next != null) {
                    currCommand = currCommand.Next;
                }
            } else if (Input.GetKeyDown (KeyCode.DownArrow)) {
                if (currCommand.Previous != null) {
                    currCommand = currCommand.Previous;
                }
            }

            input.text = currCommand.Value;
        }
    }

    private void SubmitCommand () {
        string cmd = input.text;
        history.AddAfter (history.First, new LinkedListNode<string> (cmd));
        input.text = "";
        EventSystem.current.SetSelectedGameObject (input.gameObject, null);
        input.OnPointerClick (new PointerEventData (EventSystem.current));
        PrintLine ("> " + cmd);
        GetComponent<Commands> ().RunCommand (cmd);
        currCommand = history.First;
    }

    public void PrintLine (string line) {
        output.text += " " + line + "\n";
        output.rectTransform.sizeDelta += new Vector2 (0, 20f);
        scroll.verticalNormalizedPosition = 0.00001f;
    }

    // public void OpenFile ()

    public void ScrollUp () {
        scroll.verticalNormalizedPosition += SCROLL_BUTTON_SENSITIVITY;
    }

    public void ScrollDown () {
        scroll.verticalNormalizedPosition -= SCROLL_BUTTON_SENSITIVITY;
    }
}