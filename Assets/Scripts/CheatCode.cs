using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCode : MonoBehaviour {

    private List<KeyCode> correctKeys;
    private bool awaitingActivation = false;
    private bool keyPressed = false;

    private int ptr = 0;
    // Start is called before the first frame update
    void Start () {
        DontDestroyOnLoad (this.gameObject);
        correctKeys = new List<KeyCode> ();
        correctKeys.Add (KeyCode.UpArrow);
        correctKeys.Add (KeyCode.UpArrow);
        correctKeys.Add (KeyCode.DownArrow);
        correctKeys.Add (KeyCode.DownArrow);
        correctKeys.Add (KeyCode.LeftArrow);
        correctKeys.Add (KeyCode.RightArrow);
        correctKeys.Add (KeyCode.LeftArrow);
        correctKeys.Add (KeyCode.RightArrow);
        correctKeys.Add (KeyCode.B);
        correctKeys.Add (KeyCode.A);
        correctKeys.Add (KeyCode.Return);
    }

    void Update () {
        if (SceneManager.GetActiveScene ().buildIndex == 1 && awaitingActivation) {
            GameObject.Find ("router1").GetComponent<Router> ().online = true;
            GameObject.Find ("router2").GetComponent<Router> ().online = true;
            Router.unlockedRouters.Add (GameObject.Find ("router1").GetComponent<Router> ());
            Router.unlockedRouters.Add (GameObject.Find ("router2").GetComponent<Router> ());
            awaitingActivation = false;
            Destroy (this.gameObject);
        }
    }
    void OnGUI () {
        Event e = Event.current;
        if (e.isKey && e.keyCode != KeyCode.None) {
            if (!keyPressed) {
                keyPressed = true;
                // print (e.keyCode);

                if (!awaitingActivation && correctKeys[ptr] == e.keyCode) {
                    ptr += 1;
                    if (ptr == correctKeys.Count) {
                        awaitingActivation = true;
                        if (SceneManager.GetActiveScene ().buildIndex == 0) {
                            SceneManager.LoadScene (1);
                        }
                    }
                } else {
                    ptr = 0;
                }
            } else {
                keyPressed = false;
            }
        }
    }
}