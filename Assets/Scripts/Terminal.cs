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
    public TMP_Text output;
    public ScrollRect scroll;
    const int MAX_LINES = 12;
    public List<TextAsset> userAvailableLogs = new List<TextAsset>(); // List of available logs to the 'ls' command.

    /// The currently selected command. Used for up-arrow history completion.
    private LinkedListNode<string> currCommand;

    public LinkedList<string> history; // queue of terminal inputs

    // Start is called before the first frame update
    void Start () {
        history = new LinkedList<string> ();
        history.AddFirst ("SENTINEL");
        currCommand = history.First;
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
        GetComponent<Commands>().RunCommand(cmd);
        currCommand = history.First;
    }

    public void PrintLine (string line) {
        // if (items.Count >= MAX_LINES) {
        //     items.RemoveFirst();
        // }
        // items.AddLast(line);

        // // This could be in a separate method, where we give
        // // the start line and it prints next 12 lines
        // // (when we implement scrolling)
        // output.text = "";
        // foreach (string s in items) {
        //     output.text += s + "\n";
        // }\

        output.text += line + "\n";
        output.rectTransform.sizeDelta += new Vector2 (0, 20f);
        scroll.verticalNormalizedPosition = 0.00001f;

        // A potential option, but it also could make things harder
        // to maintain a similar functionality with scrolling:
        // in the if block:
        // output.text = output.text.substring(output.text.indexOf('\n') + 1);

        // at the end:
        // output.text += line + "\n";
    }
}