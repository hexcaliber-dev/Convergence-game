using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// Init sequence (game startup process)
public class InitSequence : MonoBehaviour {
    public TextAsset startupDump;
    public ScrollRect scroll;
    public TMP_Text initializingTxt, contents, credits;
    public CanvasGroup presents, logo;

    bool sequenceComplete = false;
    // Start is called before the first frame update
    void Start () {
        initializingTxt.text = "";
        logo.alpha = 0f;
        presents.alpha = 0f;
        credits.text = "";
        StartCoroutine (StartupSequence ());
    }

    void Update() {
        if (sequenceComplete && Input.GetKeyDown(KeyCode.A)) {
            StartCoroutine(Complete());
        }
    }

    IEnumerator StartupSequence () {
        char[] initializing = "INITIALIZING...".ToCharArray ();
        foreach (char c in initializing) {
            initializingTxt.text += c;
            yield return new WaitForSeconds (0.1f);
        }
        yield return new WaitForSeconds (1f);
        initializingTxt.text = "";
        yield return new WaitForSeconds (0.5f);


        presents.alpha = 1f;
        yield return new WaitForSeconds (1f);
        logo.alpha = 1f;
        yield return new WaitForSeconds (1f);

        credits.text += "> Ben Cuan\n";
        yield return new WaitForSeconds (0.3f);
        credits.text += "> Omar Hossain\n";
        yield return new WaitForSeconds (0.3f);
        credits.text += "> Akhilan Ganesh\n";
        yield return new WaitForSeconds (0.3f);
        credits.text += "> Kevin Chu\n";
        yield return new WaitForSeconds (0.3f);
        credits.text += "> Eric Qian\n";
        yield return new WaitForSeconds (0.3f);
        credits.text += "> Kapilan Ganesh\n";

        yield return new WaitForSeconds (2f);
        logo.alpha = 0f;
        presents.alpha = 0f;
        credits.text = "";

        string[] dumpLines = Regex.Split (startupDump.text, "\n|\r|\r\n");
        foreach (string s in dumpLines) {
            if (s.Contains ("@@@@@")) {
                Clear();
            } else if (s.Trim().StartsWith("%")) {
                yield return new WaitForSeconds(float.Parse(s.Trim().Substring(2)));
            } else {
                PrintLine (s);
                yield return new WaitForSeconds (0.002f);
            }
        }

        sequenceComplete = true;
    }

    IEnumerator Complete() {
        PrintLine("Good to hear. Transitioning in 3 seconds. Please wait.");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }

    void PrintLine (string line) {
        contents.text += " " + line + "\n";
        contents.rectTransform.sizeDelta += new Vector2 (0, 20f);
        scroll.verticalNormalizedPosition = 0.00001f;
    }

    void Clear () {
        contents.text = "";
        contents.rectTransform.sizeDelta = new Vector2 (contents.rectTransform.sizeDelta.x, 0f);
    }
}