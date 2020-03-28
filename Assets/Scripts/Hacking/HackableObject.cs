using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HackableObject : MonoBehaviour {

        // UI for Hackable Object
        public CanvasGroup panel;
        // 1 or 2. 1 is the larger panel on the top
        public int panelNo;
        // Is the UI active?
        public bool active = false;
        // whether or not the device can be accessed
        public bool online = true;
        // id for the object -- used to hack object --
        public string uid; // hack id

        public Terminal terminal;

        public Dictionary<string, Command> command_library;

        // public enum Type {Camera, Robot, Phone, Printer, TV, Light, Door, Server, Router};

        // Start is called before the first frame update
        void Start () {
            command_library = new Dictionary<string, Command> ();
        }

        // Update is called once per frame
        void Update () {

        }

        public virtual void HackMessage () {
            terminal.PrintLine ("<color='green'>Successfully connected to " + ToString () + "</color>");
        }

        public void ToggleEnabled () {
            SetEnabled (!active);
        }

        public virtual void SetEnabled (bool enabled) {
            active = enabled;
            if (active) {
                panel.blocksRaycasts = true;
                panel.alpha = 1f;
                AddCommands (Commands.cmds);
            } else {
                panel.blocksRaycasts = false;
                panel.alpha = 0f;
                RemoveCommands (Commands.cmds);
            }
        }

        public override string ToString () {
            return uid + " (" + GetType ().Name + ")";
        }

        // Adds commands to the available command dictionary.
        public virtual void AddCommands (Dictionary<string, Command> library) {
                if (command_library != null) {
                    foreach (KeyValuePair<string, Command> entry in command_library) {
                        library.Add (entry.Key, entry.Value);
                        terminal.PrintLine ("New command detected: " + entry.Key);
                        terminal.PrintLine ("Type 'help " + entry.Key + " for more info.");
            }
        }
    }

    // Removes all commands created by this object from the command library.
    public virtual void RemoveCommands (Dictionary<string, Command> library) {
        if (command_library != null) {
            foreach (KeyValuePair<string, Command> entry in command_library)
                library.Remove (entry.Key);
        }
    }

    public virtual GameObject AddObjects () { return this.gameObject; }

}