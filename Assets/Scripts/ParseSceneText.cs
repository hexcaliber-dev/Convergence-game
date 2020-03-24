using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ParseSceneText : MonoBehaviour
{
    string textFileDir = "Assets/TextData/sceneText.txt";
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(parseSceneTextCtx(0));
        Debug.Log(parseSceneTextCtx(1));
        Debug.Log(parseSceneTextCtx(2));
        Debug.Log(parseSceneTextCtx(3));
        Debug.Log(parseSceneTextCtx(4));
        Debug.Log(parseSceneTextCtx(5));
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
