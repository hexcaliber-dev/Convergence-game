using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class Commands : MonoBehaviour {
    public TextAsset allFiles;
    public TextAsset allCommands;
    public TextAsset userAvailableFiles;
    public TextAsset userAvailableCommands;

    // For testing purposes
    void Start () {
        cmds.Add ("ls", new Ls (this));
        cmds.Add ("help", new Help (this));
        cmds.Add ("read", new Read(this));
        Debug.Log(this);
    }

    private Dictionary<string, Command> cmds = new Dictionary<string, Command> ();

    abstract class Command {
        public string name;
        public string description;
        public string usage;

        protected Commands comRef;

        public abstract void action (string[] args);

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

        public override void action (string[] args) {
            List<string> file_list = ReadToList (comRef.userAvailableFiles);
            foreach (string s in file_list) { comRef.PrintToTerminal (s); }
        }
    }

    class Help : Command {
        public Help (Commands com) : base (com) {
            name = "help";
            description = "lists the available commands you can execute. Will also provide commands for external devices";
            usage = "help <command>";
        }

        public override void action (string[] args) {
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
                }
                else
                    comRef.PrintToTerminal ("Command doesn't exist...");
            }
        }
    }

    class Read : Command {
        public Read (Commands com) : base (com) {
            name = "read";
            description = "specified files is read and outputted into terminal";
            usage = "read <filename>";
        }

        public override void action (string[] args) {
            /*  DEBUG
            foreach (string i in args) {Debug.Log(i);}
            Debug.Log($"{args[0]}.txt");
            Debug.Log(ReadToList(comRef.userAvailableFiles)[0]);
            Debug.Log( $"{args[0]}.txt".Equals(ReadToList(comRef.userAvailableFiles)[0].TrimEnd(new char[] { '\r', '\n' })) );

            Debug.Log( $"{args[0]}.txt".Equals(ua_files[0]) );
            // foreach (string i in ReadToList(comRef.userAvailableFiles)) {Debug.Log(i);}
            */
            List<string> ua_files = ReadToList(comRef.userAvailableFiles);
            for (int i = 0; i < ua_files.Count; i++){ ua_files[i] = ua_files[i].TrimEnd(new char[] { '\r', '\n' }); }
            if (args.Length == 0){ comRef.PrintToTerminal("Please input file name after \"read\" command"); }
            else if ( ua_files.Contains( $"{args[0]}.txt" ) )
            {
                string content = Resources.Load<TextAsset>($"Text/Logs/{args[0]}").text;
                comRef.PrintToTerminal(content);
            }
            else { comRef.PrintToTerminal("File not found."); }
        }

    }

    public void RunCommand (string cmd) {
        string[] cmdSplit = cmd.Trim ().Split ();
        if (cmdSplit.Length > 0) {
            if (cmds.ContainsKey (cmdSplit[0])) {
                cmds[cmdSplit[0]].action (cmdSplit.Skip(1).ToArray ());
            } else {
                PrintToTerminal ("<color=\"red\">Unknown Command: " + cmd + "</color>");
            }
        }
    }

    private void PrintToTerminal (string txt) {
        GetComponent<Terminal> ().PrintLine (txt);
    }

    public void halt () {
        Application.Quit ();
    }

}