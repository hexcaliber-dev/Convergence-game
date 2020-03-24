using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

public class Commands : MonoBehaviour
{
    public TextAsset allFiles;
    public TextAsset userAvailableFiles;


    // For testing purposes
    void Start()
    {
        cmds.Add("ls", new Ls());
        // cmds.Add("cd", new Cd());
    }

    private Dictionary<string, Command> cmds = new Dictionary<string, Command>();

    abstract class Command {
        public string name;
        public string description;

        public abstract void action();
    }

    class Ls : Command {
        public Ls() {
            name = "ls";
            description = "Lists the files in the directory";
        }

        public override void action() {
            
        }
    }

    // class Cd : Command {
    //     ...
    // }

    public List<string> ls()
    {
        string read_text = userAvailableFiles.text;
        List<string> file_list = read_text.Split('\n').ToList<string>(); // Split using Newline
        return file_list;
    }

}
