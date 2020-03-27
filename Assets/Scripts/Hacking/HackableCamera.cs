using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using 
public class HackableCamera : HackableObject {

    // Start is called before the first frame update
    void Start () {
        panelNo = 1;
        command_library = new string[1] {"pan"};
    }

    // Update is called once per frame
    void Update () {

    }

    public override GameObject AddObjects () { return this.gameObject; }
     

}