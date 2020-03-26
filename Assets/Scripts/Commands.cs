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

    private Dictionary<string, Command> cmds = new Dictionary<string, Command> ();

    abstract class Command {
        public string name;
        public string description;
        public string usage;

        protected Commands comRef;

        public abstract void Action (string[] args);

        public Command (Commands com) {
            comRef = com;
        }

        protected List<string> ReadToList (TextAsset ta) {
            string read_text = ta.text;
            List<string> read_list = read_text.Split ('\n').ToList<string> (); // Split using Newline
            return read_list;
        }
    }

    class Ls : Command {
        public Ls (Commands com) : base (com) {
            name = "ls";
            description = "lists the files in the specific directory/folder you're in";
            usage = "ls";
        }

        public override void Action (string[] args) {
            foreach (string s in comRef.GetComponent<Terminal> ().userAvailableLogs.Keys) { comRef.PrintToTerminal (s); }
        }
    }

    class Help : Command {
        public Help (Commands com) : base (com) {
            name = "help";
            description = "lists the available commands you can execute. Will also provide commands for external devices";
            usage = "help <command>";
        }

        public override void Action (string[] args) {
            print (args);
            if (args.Length == 0) {
                comRef.PrintToTerminal ("List of available commands:");
                foreach (var item in comRef.cmds) {
                    comRef.PrintToTerminal (item.Key);
                }
            } else {
                if (comRef.cmds.ContainsKey (args[0])) {
                    comRef.PrintToTerminal (args[0] + " - " + comRef.cmds[args[0]].description);
                    comRef.PrintToTerminal ("Usage: " + comRef.cmds[args[0]].usage);
                } else
                    comRef.PrintToTerminal ("Command doesn't exist...");
            }
        }
    }

    class Read : Command {
        public Read (Commands com) : base (com) {
            name = "read";
            description = "specified file is read and outputted into terminal";
            usage = "read <filename>";
        }

        public override void Action (string[] args) {
            /*  DEBUG
            foreach (string i in args) {Debug.Log(i);}
            Debug.Log($"{args[0]}.txt");
            Debug.Log(ReadToList(comRef.userAvailableFiles)[0]);
            Debug.Log( $"{args[0]}.txt".Equals(ReadToList(comRef.userAvailableFiles)[0].TrimEnd(new char[] { '\r', '\n' })) );

            Debug.Log( $"{args[0]}.txt".Equals(ua_files[0]) );
            // foreach (string i in ReadToList(comRef.userAvailableFiles)) {Debug.Log(i);}
            */
            // List<string> ua_files = comRef.UserAvailableFiles ();
            // for (int i = 0; i < ua_files.Count; i++) { ua_files[i] = ua_files[i].TrimEnd (new char[] { '\r', '\n' }); }
            if (args.Length == 0) {
                comRef.PrintToTerminal ("Please input file name after \"read\" command.");
            } else if (comRef.UserAvailableFiles ().ContainsKey ($"{args[0]}")) {
                comRef.GetComponent<Terminal> ().OpenFile (comRef.UserAvailableFiles () [args[0]]);
            } else if (comRef.UserAvailableFiles ().ContainsKey ($"{args[0]}.txt")) {
                comRef.GetComponent<Terminal> ().OpenFile (comRef.UserAvailableFiles () [args[0] + ".txt"]);
            } else { comRef.PrintToTerminal ("File not found: " + args[0]); }
        }
    }

    class Hack : Command {
        public Hack (Commands com) : base (com) {
            name = "hack";
            description = "hack online devices in vicinity";
            usage = "hack <devicename>";
        }

        public override void Action (string[] args) {
            Router router = (Router) GameObject.FindObjectOfType (typeof (Router));
            int hackInd;
            HackableObject toHack = router;
            // invalid arguments
            if (args.Length != 1) {
                comRef.PrintToTerminal ("Please input device name after \"hack\" command.");
            }
            // everything good!
            else {
                hackInd = FindHackableObjectByUID (router.connections, args[0]);
                if (hackInd > -1) {
                    toHack = router.connections[hackInd];

                    // SUCCESS PATH
                    if (toHack.online && !toHack.active) { // hackable object found and not 
                        // already active
                        // for loop through all hackable objects and turn them off
                        /// TODO: Make sure to check if object should be made inactive
                        /// or not (i.e. camera, etc. should not be when others are hacked)
                        foreach (HackableObject obj in router.connections) {
                            if (obj.active)
                                obj.SetEnabled (false);
                        }

                        toHack.SetEnabled (true);

                        comRef.PrintToTerminal ("<color=\"green\">" + toHack.ToString () + " successfully hacked.</color>");

                        // FAILURE PATH
                    } else if (toHack.online) { // hackable object active
                        comRef.PrintToTerminal ("Already connected to " + toHack.ToString ());
                    } else { // hackable object offline
                        comRef.PrintToTerminal ("<color=\"red\">ERROR: " + toHack.ToString () + " is offline.</color>");
                    }
                } else { // hackable object not found
                    comRef.PrintToTerminal ("<color=\"red\">ERROR: Connection doesn't exist.</color>");
                }
            }
        }

        // to find the object to hack
        // returns the index, or -1 if it does not exist
        private int FindHackableObjectByUID (List<HackableObject> list, string objUID) {
            // look through all sequentially
            for (int i = 0; i < list.Count; i++) {
                // return index if found
                if (objUID.Equals (list[i].uid))
                    return i;
            }
            return -1;
        }
    }

    class Portscan : Command {
        public Portscan (Commands com) : base (com) {
            name = "portscan";
            description = "scans environment and finds all routers in broad localization";
            usage = "portscan";
        }

        public override void Action (string[] args) {
            if (args.Length == 0) {
                comRef.PrintToTerminal ("Networks Found:");
                for (int i = 0; i < Router.allRouterNames.Length; i += 1) {
                    if (i < Router.unlockedRouters.Count) {
                        comRef.PrintToTerminal ("<color=\"green\">" + Router.allRouterNames[i] + " (ONLINE)</color>");
                    } else {
                        comRef.PrintToTerminal ("<color=\"red\">" + Router.allRouterNames[i] + " (OFFLINE)</color>");
                    }
                }
                comRef.PrintToTerminal ("To connect, type 'hack <devicename>'.");
            } else {
                comRef.PrintToTerminal ("Usage: portscan");
            }
        }
    }

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

    private void PrintToTerminal (string txt) {
        GetComponent<Terminal> ().PrintLine (txt);
    }

    private Dictionary<string, TextAsset> UserAvailableFiles () {
        return GetComponent<Terminal> ().userAvailableLogs;
    }

    public void Halt () {
        Application.Quit ();
    }

}