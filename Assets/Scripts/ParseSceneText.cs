using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseSceneText : MonoBehaviour
{
    string textFileDir = "sceneText.txt";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string parseSceneTextCtx(int selectNum) {
        string rawDialog = File.ReadAllText(textFileDir);

        char delim = ';';
        string[] dialogVals = rawDialog.Split(delim);

        if (selectNum < dialogVals.Length) {
            return dialogVals[selectNum];
        }
        else {
            return "invalid";
        }

    }
}
