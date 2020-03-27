using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Router : HackableObject {

    //list of all routers in the game so far.
    public static List<Router> unlockedRouters;
    //list of all routers' names regardless of if they are unlocked or not.
    // TODO fix the list
    public static string[] allRouterNames = new string[] { "router0", "router1", "router2" };
    // list of all hackable objects in room
    public List<HackableObject> connections;

    // shows all hackable objects in room/radius
    public TMP_Text nameTxt, txt;

    // Start is called before the first frame update
    void Start () {
        panelNo = 2;
        nameTxt.text = uid;
        string routerText = "";
        // need txt to show all contents of connections
        connections.Insert (0, this);
        foreach (HackableObject connection in connections) {
            routerText = AddConnection (routerText, connection);
            txt.rectTransform.sizeDelta += new Vector2 (0, 30f);
            connection.SetEnabled (false);
        }

        unlockedRouters = new List<Router> ();
        unlockedRouters.Add (GameObject.Find ("Router0").GetComponent<Router> ());
        txt.text = routerText;
    }

    string AddConnection (string text, HackableObject obj) {
        string color = "red";
        if (obj.online)
            color = "green";
        text += "<color=\"" + color + "\"> " + obj.ToString () + "</color>\n";
        return text;
    }

    // Update is called once per frame
    void Update () {

    }

    public void setHackableConnection (string objUID, bool online) {
        // for all connections matching given UID,
        // set their online state
        foreach (HackableObject obj in connections)
            if (obj.uid == objUID)
                obj.online = online;
    }

    public override void HackMessage () {
        base.HackMessage ();
        terminal.PrintLine ("Found " + connections.Count + "connectable interfaces in current subnet.");
    }

    // Closes the selected panel (1 or 2; 1 is the big one on the top)
    public void ClosePanel (int panel) {
        if (panel < 1 || panel > 2) {
            Debug.LogError("Error: Panel number " + panel + " is invalid!");
        }
        
        foreach (HackableObject obj in connections) {
            if (obj.panelNo == panel) {
                obj.SetEnabled(false);
            }
        }
    }

}