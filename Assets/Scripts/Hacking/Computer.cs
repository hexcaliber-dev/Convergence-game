using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Computer : HackableObject {

    public List<TextAsset> files; // files the computer has
    // public TextAsset userAvailableLogs; // Must be passed in... Could make it static since same across all servers
    public TMP_Text incorrectPassText; // incorrect password
    public TMP_Text nameText; // server name text
    // specific state of the server
    public enum State { Locked, Unlocked, InProgram }
    // UIs for locked server, unlocked server, etc. Corresponds to State enum for what UI it is.
    public List<CanvasGroup> screens;
    public State currState; // DO NOT ASSIGN DIRECTLY! Use SetState()
    public TMP_InputField passInput; // Password input field
    private ExplorerFile[] explorerFiles;
    public Button openRouterProgram;
    public bool hasRouterProgram;

    // Start is called before the first frame update
    void Start () {
        panelNo = 2;
    }

    // Update is called once per frame
    void Update () {

    }

    public override void SetEnabled (bool enabled) {
        base.SetEnabled (enabled);
        if (enabled) {
            passInput.onSubmit.RemoveAllListeners ();
            if (hasRouterProgram) {
                openRouterProgram.GetComponent<CanvasGroup> ().alpha = 1f;
                openRouterProgram.GetComponent<CanvasGroup> ().blocksRaycasts = true;
                openRouterProgram.onClick.RemoveAllListeners ();
                openRouterProgram.onClick.AddListener (delegate {
                    SetState (State.InProgram);
                });
            } else {
                openRouterProgram.GetComponent<CanvasGroup> ().alpha = 0f;
                openRouterProgram.GetComponent<CanvasGroup> ().blocksRaycasts = false;
            }
            passInput.text = "";
            incorrectPassText.enabled = false;
            // Unlock by default if no password specified.
            
            nameText.text = uid;
            explorerFiles = panel.GetComponentsInChildren<ExplorerFile> ();
            foreach (ExplorerFile f in explorerFiles) {
                f.GetComponent<CanvasGroup>().alpha = 0f;
            }
            for (int i = 0; i < files.Count; i += 1) {
                // print(files[i]);
                explorerFiles[i].SetFile (files[i]);
            }
        }
        UpdateLight ();
    }

    // sets the state of the server to the given state    
    public void SetState (State newState) {
        currState = newState;
        for (int i = 0; i < screens.Count; i += 1) {
            if (i == (int) (newState)) {
                screens[i].alpha = 1f;
                screens[i].blocksRaycasts = true;
            } else {
                screens[i].alpha = 0f;
                screens[i].blocksRaycasts = false;
            }
        }
        UpdateLight ();
    }

    void UpdateLight () {
        Color color = Color.red;
        if (active) {
            if (currState == State.Locked) {
                color = Color.yellow;
            } else if (currState == State.Unlocked) {
                color = Color.green;
            } else if (currState == State.InProgram) {
                color = Color.blue;
            }
        }
        foreach (UnityEngine.Experimental.Rendering.Universal.Light2D light in GetComponentsInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D> ()) {
            light.color = color;
        }
    }
}