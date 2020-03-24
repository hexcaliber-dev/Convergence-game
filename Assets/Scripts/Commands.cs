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
        ls();
    }

    public List<string> ls()
    {
        string read_text = userAvailableFiles.text;
        List<string> file_list = read_text.Split('\n').ToList<string>(); // Split using Newline
        return file_list;
    }


}
