using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Represents one file in file explorer.
public class ExplorerFile : MonoBehaviour {
    private TMP_Text label;
    private TextAsset file;
    // Start is called before the first frame update
    void Start () {
        label = GetComponentInChildren<TMP_Text> ();
        GetComponent<Button> ().onClick.AddListener (openFile);
        if (file != null) {
            label.text = file.name + ".txt";
            GetComponent<CanvasGroup> ().alpha = 1f;
        } else {
            GetComponent<CanvasGroup> ().alpha = 0f;
        }
    }

    // Update is called once per frame
    void Update () {

    }

    public void SetFile (TextAsset f) {
        file = f;
        if (label != null) {
            label.text = file.name + ".txt";
            GetComponent<CanvasGroup> ().alpha = 1f;
        }
    }

    public void openFile () {
        if (file != null) {
            GameObject.FindObjectOfType<Terminal> ().OpenFile (file);
        }
    }
}