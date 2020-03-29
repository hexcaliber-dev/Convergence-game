using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Generator : HackableObject {

    // Powered objects matched with their ID's
    public Dictionary<int, PoweredObject> connectedObjs;
    public int currPowered; // The currently powered device (0 if nothing is powered)
    public TMP_Text currPoweredText, numpadDisplay, nameText;
    public Image checkXImage;
    public Sprite checkImg, xImg;
    void Start () {
        panelNo = 2;
    }

    public override void SetEnabled(bool enabled) {

    }

    // Sets the power state of object ID to POWER_STATE.
    void SetPower (int id, bool powerState) {
        connectedObjs[id].SetPower (powerState);
    }

    // Action of submit button
    void Submit () {
        if (numpadDisplay.text.Length > 0) {
            int id = int.Parse (numpadDisplay.text);
            if (connectedObjs.ContainsKey (id)) {
                PoweredObject obj = connectedObjs[id];
                obj.TogglePower ();
                if (obj.IsPowered ()) {
                    terminal.PrintLine ("<color=\"green\">" + obj.ToString () + " successfully powered</color>");
                } else {
                    terminal.PrintLine ("<color=\"green\">" + obj.ToString () + " turned off successfully</color>");
                }
                StartCoroutine (ShowStatusImage (true, 3f));
            } else {
                terminal.PrintLine ("<color=\"red\">" + id + " not found in " + uid + "</color>");
                StartCoroutine (ShowStatusImage (false, 3f));
            }
        }
    }

    public void EnterNumber (int num) {
        if (numpadDisplay.text.Length < 6) {
            numpadDisplay.text += num;
        }
    }

    public void ClearNumpad () {
        numpadDisplay.text = "";
    }

    public IEnumerator ShowStatusImage (bool success, float seconds) {
        if (success) {
            checkXImage.sprite = checkImg;
        } else {
            checkXImage.sprite = xImg;
        }
        checkXImage.enabled = true;
        yield return new WaitForSeconds (seconds);
        checkXImage.enabled = false;
    }
}