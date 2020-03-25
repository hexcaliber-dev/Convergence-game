using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Server : HackableObject {

    // server password
    public string password = "";
    public List<TextAsset> files; // files the SERVER HAS
    // public TextAsset userAvailableLogs; // Must be passed in... Could make it static since same across all servers
    public TMP_Text incorrectPassText; // incorrect password
    public TMP_Text nameText; // server name text
    // specific state of the server
    public enum State { Locked, Unlocked, ViewingFile }
    // UIs for locked server, unlocked server, etc. Corresponds to State enum for what UI it is.
    public List<CanvasGroup> screens;
    public State currState; // DO NOT ASSIGN DIRECTLY! Use SetState()
    public TMP_InputField passInput; // Password input field
    bool addedToLog = false; // have the servers' files been added to the player's log?

    // Start is called before the first frame update
    void Start () {
        incorrectPassText.enabled = false;
        // Unlock by default if no password specified.
        if (password.Length == 0) {
            SetState (State.Unlocked);
        } else {
            SetState (State.Locked);
            passInput.onSubmit.AddListener(delegate {authenticate(passInput.text);});
        }
        nameText.text = uid;
    }

    // Update is called once per frame
    void Update () {

    }

    /*  if password_in matches password, will unlock server

        Pre: password_in    Password used for authentication
        Post: bool          true if success, false if fail
    */
    bool authenticate (string password_in) {
        if (password_in.Equals (password)) {
            // auth success (You a genius!)
            SetState (State.Unlocked);
            incorrectPassText.enabled = false;
            return true;
        }
        // auth failed (You suck!)
        SetState (State.Locked);
        incorrectPassText.enabled = true;
        return false;
    }

    // sets the state of the server to the given state    
    void SetState (State newState) {
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
    }

    // By yours truly, Omar Amazing Hossain - The DIRECTOR <-- Omar didn't write this
    void AddLogToFile () {
        // Add contents of Server to Available Logs File
        // Write to the userAvailableLogs TextAsset
        if (!addedToLog) {
            foreach (TextAsset file in files) {
                terminal.userAvailableLogs.Add (file);
                terminal.PrintLine ("Added new file " + file.name);
            }
            terminal.PrintLine ("Added " + files.Count + " file(s) to log. Type 'ls' to view list.");
        }
    }
}