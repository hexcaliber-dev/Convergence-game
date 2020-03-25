using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Represents one file in file explorer.
public class ExplorerFile : MonoBehaviour {
    private TMP_Text label;
    private TextAsset file;
    // Start is called before the first frame update
    void Start () {
        label = GetComponentInChildren<TMP_Text>();
        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<Button>().onClick.AddListener(openFile);
    }

    // Update is called once per frame
    void Update () {

    }

    public void SetFile(TextAsset f) {
        file = f;
        label.text = file.name + ".txt";
        GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void openFile() {
        GameObject.FindObjectOfType<Terminal>().OpenFile(file);
        GameObject.FindObjectOfType<Terminal>().AddLogToFile(file);
    }
}