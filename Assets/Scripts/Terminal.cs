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

    // Start is called before the first frame update
    void Start () {
        input.onSubmit.AddListener (delegate { SubmitCommand (); });
    }

    // Update is called once per frame
    void Update () { }

    private void SubmitCommand () {
        string cmd = input.text;
        input.text = "";
        Debug.Log (cmd);
        input.Select ();
        EventSystem.current.SetSelectedGameObject (input.gameObject, null);
        input.OnPointerClick (new PointerEventData (EventSystem.current));
    }
}