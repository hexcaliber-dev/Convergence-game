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
    public TMP_Text output, fileNameText, fileViewText;
    public ScrollRect scroll, fileScroll;
    public CanvasGroup fileView;
    const int MAX_LINES = 12;
    public Dictionary<string, TextAsset> userAvailableLogs; // Dictionary of available logs to the 'ls' command. Key is the file name (ending in .txt), value is the TextAsset object.
    const float SCROLL_BUTTON_SENSITIVITY = 0.1f;
    const int TEXT_HEIGHT = 25; // how much should be added 
    const string DEFAULT_FILEDIR = "/home/neuros"; // what should be printed in filename text when no files are open

    /// The currently selected command. Used for up-arrow history completion.
    private LinkedListNode<string> currCommand;

    public LinkedList<string> history; // queue of terminal inputs

    // Start is called before the first frame update
    void Start () {
        history = new LinkedList<string> ();
        history.AddFirst ("SENTINEL");
        currCommand = history.First;
        input.onSubmit.AddListener (delegate { SubmitCommand (); });
        CloseFile ();
        userAvailableLogs = new Dictionary<string, TextAsset> ();
        userAvailableLogs.Add ("README.txt", Resources.Load<TextAsset> ("Text/Logs/README"));
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
                    input.text = currCommand.Value;
                }
            } else if (Input.GetKeyDown (KeyCode.DownArrow)) {
                if (currCommand.Previous != null) {
                    currCommand = currCommand.Previous;
                    input.text = currCommand.Value;
                }
            }

            // input.text = currCommand.Value;
        }
    }

    private void SubmitCommand () {
        string cmd = input.text;

        // Entering no command
        if (cmd.Equals("")) {
            PrintLine("> ");
            return;
        }
        history.AddAfter (history.First, new LinkedListNode<string> (cmd));
        input.text = "";
        if (!(cmd.Equals("pan") || cmd.Equals("move"))) {
            EventSystem.current.SetSelectedGameObject (input.gameObject, null);
            input.OnPointerClick (new PointerEventData (EventSystem.current));
        }
        PrintLine ("> " + cmd);
        GetComponent<Commands> ().RunCommand (cmd);
        currCommand = history.First;
    }

    public void PrintLine (string line) {
        output.text += " " + line + "\n";
        // output.rectTransform.sizeDelta += new Vector2 (0, TEXT_HEIGHT);
        StartCoroutine(scrollDownAutomatically());
    }

    IEnumerator scrollDownAutomatically () {
        yield return new WaitForSeconds (0.1f);
        scroll.verticalNormalizedPosition = 0.00001f;
    }

    // public void OpenFile ()

    public void ScrollUp () {
        scroll.verticalNormalizedPosition += SCROLL_BUTTON_SENSITIVITY;
    }

    public void ScrollDown () {
        scroll.verticalNormalizedPosition -= SCROLL_BUTTON_SENSITIVITY;
    }

    public void OpenFile (TextAsset file) {
        fileView.alpha = 1f;
        fileView.blocksRaycasts = true;
        fileNameText.text = DEFAULT_FILEDIR + "/" + file.name + ".txt";
        fileViewText.text = file.text;
        fileScroll.verticalNormalizedPosition = 0.99999f;
    }

    public void CloseFile () {
        fileView.alpha = 0f;
        fileView.blocksRaycasts = false;
        fileNameText.text = DEFAULT_FILEDIR;
    }

    // By yours truly, Omar Amazing Hossain - The DIRECTOR <-- Omar didn't (not) write this
    public void AddLogToFile (TextAsset file) {
        // Add contents of Server to Available Logs File
        // Write to the userAvailableLogs TextAsset
        if (!userAvailableLogs.ContainsValue (file)) {
            userAvailableLogs.Add (file.name + ".txt", file);
            PrintLine ("Added new file to system: " + file.name + ".txt");
        }
    }
}