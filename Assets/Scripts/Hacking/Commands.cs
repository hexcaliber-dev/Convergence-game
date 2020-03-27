using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class Commands : MonoBehaviour {
    public TextAsset allFiles;
    public TextAsset allCommands;

    // For testing purposes
    void Start () {
        
        cmds.Add ("ls", new Ls (this));
        cmds.Add ("help", new Help (this));
        cmds.Add ("read", new Read (this));
        cmds.Add ("hack", new Hack (this));
        cmds.Add ("portscan", new Portscan (this));
        Debug.Log (this);
    }

    /*void MakeMasterCmds()
    {
        master_cmds.Add ("ls", new Ls (this));
        master_cmds.Add ("help", new Help (this));
        master_cmds.Add ("read", new Read (this));
        master_cmds.Add ("hack", new Hack (this));
        master_cmds.Add ("portscan", new Portscan (this));
        master_cmds.Add("pan", Pan);
    }

    
    // public static Dictionary<string, Class> master_cmds = new Dictionary<string, Command> ();
    */
    public static Dictionary<string, Command> cmds = new Dictionary<string, Command> ();
    

    public void RunCommand (string cmd) {
        string[] cmdSplit = cmd.Trim ().Split ();
        if (cmdSplit.Length > 0) {
            if (cmds.ContainsKey (cmdSplit[0])) {
                cmds[cmdSplit[0]].Action (cmdSplit.Skip (1).ToArray ());
            } else {
                PrintToTerminal ("<color=\"red\">Unknown Command: " + cmd + "</color>");
            }
        }
    }

    public void PrintToTerminal (string txt) {
        GetComponent<Terminal> ().PrintLine (txt);
    }

    public Dictionary<string, TextAsset> UserAvailableFiles () {
        return GetComponent<Terminal> ().userAvailableLogs;
    }

    public void Halt () {
        Application.Quit ();
    }

}