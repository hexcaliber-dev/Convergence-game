using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Any object that can be powered by a generator should inherit this class.
public class PoweredObject : MonoBehaviour {
    public int id;
    public Generator generator;
    protected bool powered;

    public void SetPower(bool powerState) {
        powered = powerState;
        if (powered) {
            PowerOnAction();
        } else {
            PowerOffAction();
        }
    }
    
    public virtual void PowerOnAction() {}
    public virtual void PowerOffAction() {}
}