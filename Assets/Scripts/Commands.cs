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
        ls();
    }

    public List<string> ls()
    {
        List<string> file_list = ReadToList(userAvailableFiles);
        return file_list;
    }

    public List<string> help()
    {
        List<string> command_list = ReadToList(userAvailableCommands);
        return command_list;
    }

    private List<string> ReadToList(TextAsset ta)
    {
        string read_text = ta.text;
        List<string> read_list = read_text.Split('\n').ToList<string>(); // Split using Newline
        return read_list;
    }

}
