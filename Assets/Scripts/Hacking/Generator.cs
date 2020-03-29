using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : HackableObject {

    // Powered objects matched with their ID's
    public Dictionary<int, PoweredObject> connectedObjs;

    void Start() {
        panelNo = 2;
    }

    // Sets the power state of object ID to POWER_STATE.
    void SetPower(int id, bool powerState) {
        connectedObjs[id].SetPower(powerState);
    }
}