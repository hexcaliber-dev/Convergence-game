using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HackableObject : MonoBehaviour {

    // UI for Hackable Object
    public CanvasGroup panel;
    // 1 or 2. 1 is the larger panel on the top
    public int panelNo;
    // Is the UI active?
    public bool active = false;
    // whether or not the device can be accessed
    public bool online = true;
    // id for the object -- used to hack object --
    public string uid; // hack id

    public Terminal terminal;

    // public enum Type {Camera, Robot, Phone, Printer, TV, Light, Door, Server, Router};

    // Start is called before the first frame update
    void Start () { }

    // Update is called once per frame
    void Update () {

    }

    public virtual void HackMessage () {
        terminal.PrintLine ("<color='green'>Successfully connected to " + ToString () + "</color>");
    }

    public void ToggleEnabled () {
        SetEnabled(!active);
    }

    public virtual void SetEnabled (bool enabled) {
        active = enabled;
        if (active) {
            panel.blocksRaycasts = true;
            panel.alpha = 1f;
        } else {
            panel.blocksRaycasts = false;
            panel.alpha = 0f;
        }
    }

    public override string ToString () {
        return uid + " (" + GetType ().Name + ")";
    }
}