using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Represents one file in file explorer.
public class RouterConnectProgram : MonoBehaviour {
    Server server;
    public TMP_InputField passInput, nameInput;
    public Button submit, close;
    public CanvasGroup correctOverlay, incorrectOverlay;
    // Start is called before the first frame update
    void Start () {
        submit.onClick.AddListener (authenticate);
        close.onClick.AddListener (delegate { server.SetState (Server.State.Unlocked); });
    }

    public void OpenPanel (Server s) {
        server = s;
        passInput.text = "";
        nameInput.text = "";
        correctOverlay.alpha = 0;
        incorrectOverlay.alpha = 0;
    }

    // Update is called once per frame
    void Update () {

    }

    void authenticate () {
        for (int i = 0; i < Router.allRouterNames.Length; i += 1) {
            if (nameInput.text.Equals (Router.allRouterNames[i]) && passInput.text.Equals (Router.allRouterCodes[i])) {
                // auth success (You a genius!)
                server.terminal.PrintLine ("<color=\"green\">Router " + nameInput.text + " is now online! " + "</color>");
                AudioHelper.PlaySound ("correct", false);
                Router newRouter = GameObject.Find (nameInput.text).GetComponent<Router> ();
                newRouter.online = true;
                Router.unlockedRouters.Add (newRouter);
                StartCoroutine (Overlay (correctOverlay, 3f));
                return;
            }
        }

        // auth failed (You suck!)
        passInput.text = "";
        StartCoroutine (Overlay (incorrectOverlay, 3f));
        AudioHelper.PlaySound ("error", false);
    }

    IEnumerator Overlay (CanvasGroup overlay, float seconds) {
        overlay.alpha = 1;
        yield return new WaitForSeconds (seconds);
        overlay.alpha = 0;
    }
}