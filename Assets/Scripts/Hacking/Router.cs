using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Router : HackableObject
{
    // list of all hackable objects in room
    public List<HackableObject> connections;

    // shows all hackable objects in room/radius
    public TMP_Text txt;

    // Start is called before the first frame update
    void Start()
    {
        string routerText = txt.text;
        // need txt to show all contents of connections
        foreach (HackableObject connection in connections)
            AddConnection(routerText, connection);
        
        txt.text = routerText;
    }

    string AddConnection(string text, HackableObject obj) {
        string color = "red";
        if (obj.online)
            color = "green";
        text += "<color = '" + color + "'>" + obj.ToString() + "</color>";
        return text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHackableConnection(string objUID, bool online) {
        // for all connections matching given UID,
        // set their online state
        foreach (HackableObject obj in connections)
            if (obj.uid == objUID)
                obj.online = online;
    }

    public override void HackMessage()
    {
        base.HackMessage();
        terminal.PrintLine("Found " + connections.Count + " hackable interfaces in current localization.");
    }

}