using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

public class Commands : MonoBehaviour
{
    public TextAsset allFiles;
    public TextAsset allCommands;
    public TextAsset userAvailableFiles;
    public TextAsset userAvailableCommands;


    // For testing purposes
    void Start()
    {
        cmds.Add("ls", new Ls(this));
        cmds.Add("help", new Help(this));
    }

    private Dictionary<string, Command> cmds = new Dictionary<string, Command>();

    abstract class Command {
        public string name;
        public string description;
        public string usage;

        protected Commands comRef;

        public abstract void action(string[] args);

        public Command(Commands com) {
            comRef = com;
        }

        protected List<string> ReadToList(TextAsset ta) {
            string read_text = ta.text;
            List<string> read_list = read_text.Split('\n').ToList<string>(); // Split using Newline
            return read_list;
        }
    }

    class Ls : Command {
        public Ls(Commands com) : base(com) {
            name = "ls";
            description = "lists the files in the specific directory/folder you're in";
            usage = "ls";
        }

        public override void action(string[] args) {
            List<string> file_list = ReadToList(comRef.userAvailableFiles);
            foreach (string s in file_list){ comRef.PrintToTerminal(s); }
        }
    }

    class Help : Command {
        public Help(Commands com) : base(com) {
            name = "help";
            description = "lists the available commands you can execute. Will also provide commands for external devices";
            usage = "help <command>";
        }

        public override void action(string[] args) {
            if (args.Length == 0) {
                comRef.PrintToTerminal("List of available commands:");
                foreach (var item in comRef.cmds) {
                    comRef.PrintToTerminal(item.Key + " - " + item.Value);
                }
            } else {
            if (comRef.cmds.ContainsKey(args[0]))
                comRef.PrintToTerminal(args[0] + " - " + comRef.cmds[args[0]]);
            else
                comRef.PrintToTerminal("Command doesn't exist...");
            }
        }
    }

    public void RunCommand(string cmd) {
        string[] cmdSplit = cmd.Trim().Split();
        if (cmdSplit.Length > 0) {
            if (cmds.ContainsKey(cmdSplit[0])) {
                cmds[cmdSplit[0]].action(cmdSplit.Skip(1).ToArray());
            } else {
                PrintToTerminal("Unknown Command: " + cmd);
            }
        }
    }

    private void PrintToTerminal(string txt) {
        GetComponent<Terminal>().PrintOutput(txt);
    }

    public void halt()
    {
        Application.Quit();
    }

}
