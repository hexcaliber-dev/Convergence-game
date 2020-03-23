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
    const int MAX_LINES = 12;

    public LinkedList<string> items; // queue of terminal output

    // Start is called before the first frame update
    void Start () {
        items = new LinkedList<string>();
        input.onSubmit.AddListener (delegate { SubmitCommand (); });
    }

    // Update is called once per frame
    void Update () { }

    private void SubmitCommand () {
        string cmd = input.text;
        input.text = "";
        EventSystem.current.SetSelectedGameObject (input.gameObject, null);
        input.OnPointerClick (new PointerEventData (EventSystem.current));
        printOutput(cmd);
    }

    private void printOutput (string line) {
        if (items.Count >= MAX_LINES) {
            items.RemoveFirst();
        }
        items.AddLast(line);

        output.text = "";
        foreach (string s in items) {
            output.text += s + "\n";
        }
    }
}