using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Any object that can be powered by a generator should inherit this class.
public class PoweredObject : MonoBehaviour {
    public int id;
    public string objName;
    public Generator generator;
    private bool powered;

    public void SetPower(bool powerState) {
        powered = powerState;
        if (powered) {
            PowerOnAction();
        } else {
            PowerOffAction();
        }
    }

    public void TogglePower() {
        SetPower(!powered);
    }

    public bool IsPowered() {
        return powered;
    }
    
    public virtual void PowerOnAction() {}
    public virtual void PowerOffAction() {}

    public override string ToString() {
        return id + " (" + objName + ")";
    }
}