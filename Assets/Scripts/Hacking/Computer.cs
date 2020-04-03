using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Computer : HackableObject {

    // Start is called before the first frame update
    void Start () {
        panelNo = 2;
    }

    // Update is called once per frame
    void Update () {

    }

    public override void SetEnabled (bool enabled) {
        base.SetEnabled (enabled);
        
    }
}