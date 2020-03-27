using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public abstract class Command {
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