using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

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
            if (args.Length == 0) {
                comRef.PrintToTerminal ("List of available commands:");
                foreach (var item in Commands.cmds) {
                    comRef.PrintToTerminal (item.Key);
                }
            } else {
                if (Commands.cmds.ContainsKey (args[0])) {
                    comRef.PrintToTerminal (args[0] + " - " + Commands.cmds[args[0]].description);
                    comRef.PrintToTerminal ("Usage: " + Commands.cmds[args[0]].usage);
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
            HackableObject toHack = router;

            // invalid arguments
            if (args.Length != 1) {
                comRef.PrintToTerminal ("Please input device name after \"hack\" command.");
            }
            // everything good!
            else {
                toHack = FindHackableObjectByUID (router.connections, args[0]);
                if (toHack != null) {
                    // SUCCESS PATH
                    if (toHack.online && !toHack.active) { // hackable object found and not 

                        comRef.PrintToTerminal("<color=\"green\">" + toHack.ToString() + " successfully hacked.</color>");
                        
                        /*foreach (string i in toHack.AddCommands()) {
                            cmds.Add(i, new Pan());
                        }*/
                        
                        // already active
                        // for loop through all hackable objects and turn them off
                        /// TODO: Make sure to check if object should be made inactive
                        /// or not (i.e. camera, etc. should not be when others are hacked)
                        router.ClosePanel(toHack.panelNo);

                        toHack.SetEnabled (true);


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
        // returns the object, or null if it does not exist
        private HackableObject FindHackableObjectByUID (List<HackableObject> list, string objUID) {
            // look through all sequentially
            for (int i = 0; i < list.Count; i++) {
                // return index if found
                if (objUID.Equals (list[i].uid))
                    return list[i];
            }
            // if all fails, does not exist in current localization
            return null;
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
