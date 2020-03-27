using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// Dummy router to connect to when moving onto the next room. When hacked into, switches the scene.
public class DummyRouter : HackableObject {

    void Update() {
        if (active) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}