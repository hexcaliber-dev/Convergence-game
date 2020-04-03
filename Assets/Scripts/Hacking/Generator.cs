using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Generator : HackableObject {

    // Powered objects matched with their ID's
    private Dictionary<int, PoweredObject> connectedObjs;
    // List in inspector. Dictionary will be populated by this
    public List<PoweredObject> connectedObjList;
    public int currPowered; // The currently powered device (0 if nothing is powered)
    public TMP_Text currPoweredText, numpadDisplay, nameText;
    public Image checkXImage, chargeMeter;
    public Sprite checkImg, xImg, chargeEmpty, chargeHalf, chargeFull;
    public int maxCharge; // number of devices that can be powered

    public Button clearButton, resetButton, enterButton;
    int currCharge;
    void Start () {
        clearButton.onClick.RemoveAllListeners ();
        resetButton.onClick.RemoveAllListeners ();
        enterButton.onClick.RemoveAllListeners ();
        panelNo = 2;
        connectedObjs = new Dictionary<int, PoweredObject> ();
        foreach (PoweredObject obj in connectedObjList) {
            connectedObjs.Add (obj.id, obj);
        }
        checkXImage.enabled = false;
    }

    public override void SetEnabled (bool enabled) {
        base.SetEnabled (enabled);
        if (enabled) {
            UpdateDisplay ();
            nameText.text = uid;
            AudioHelper.PlaySound ("buzzing", true);
            clearButton.onClick.RemoveAllListeners ();
            resetButton.onClick.RemoveAllListeners ();
            enterButton.onClick.RemoveAllListeners ();
            clearButton.onClick.AddListener (ClearNumpad);
            resetButton.onClick.AddListener (Reset);
            enterButton.onClick.AddListener (Submit);
        } else {
            AudioHelper.Stop ();
        }
    }

    // Sets the power state of object ID to POWER_STATE.
    void SetPower (int id, bool powerState) {
        connectedObjs[id].SetPower (powerState);
    }

    // Action of submit button
    public void Submit () {
        if (numpadDisplay.text.Length > 0) {
            int id = int.Parse (numpadDisplay.text);
            if (connectedObjs.ContainsKey (id)) {
                PoweredObject obj = connectedObjs[id];
                if (!obj.IsPowered ()) {
                    if (currCharge < maxCharge) {
                        currCharge += 1;
                        obj.SetPower (true);
                        terminal.PrintLine ("<color=\"green\">" + obj.ToString () + " successfully powered</color>");
                        StartCoroutine (ShowStatusImage (true, 1f));
                        AudioHelper.PlaySound ("correct", false);
                    } else {
                        terminal.PrintLine ("<color=\"red\">Max charge exceeded. Could not enable " + obj.ToString () + "</color>");
                        StartCoroutine (ShowStatusImage (false, 1f));
                        AudioHelper.PlaySound ("error", false);
                    }
                } else {
                    terminal.PrintLine ("<color=\"green\">" + obj.ToString () + " turned off successfully</color>");
                    currCharge -= 1;
                    StartCoroutine (ShowStatusImage (true, 1f));
                    obj.SetPower (false);
                    AudioHelper.PlaySound ("correct", false);
                }

            } else {
                terminal.PrintLine ("<color=\"red\">" + id + " not found in " + uid + "</color>");
                StartCoroutine (ShowStatusImage (false, 1f));
                AudioHelper.PlaySound ("error", false);
            }
        }
        UpdateDisplay ();
        ClearNumpad ();
    }

    public void EnterNumber (int num) {
        if (numpadDisplay.text.Length < 6) {
            numpadDisplay.text += num;
        }
    }

    public void ClearNumpad () {
        numpadDisplay.text = "";
    }

    public void Reset () {
        foreach (PoweredObject obj in connectedObjs.Values) {
            obj.SetPower (false);
        }
        currCharge = 0;
        UpdateDisplay ();
        terminal.PrintLine ("<color=\"yellow\">" + uid + " reset</color>");
    }

    void UpdateDisplay () {
        if (currCharge == maxCharge) {
            chargeMeter.sprite = chargeFull;
        } else if (currCharge == 0) {
            chargeMeter.sprite = chargeEmpty;
        } else {
            chargeMeter.sprite = chargeHalf;
        }

        currPoweredText.text = "";
        foreach (PoweredObject obj in connectedObjs.Values) {
            if (obj.IsPowered ()) {
                currPoweredText.text += obj.ToString () + "\n";
            }
        }
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