using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Computer : HackableObject {

    // Start is called before the first frame update
    void Start () {
        panelNo = 2;
    }

    // Update is called once per frame
    void Update () {

    }

    public void ClickAction() {
        SceneManager.LoadScene(2);
    }
}