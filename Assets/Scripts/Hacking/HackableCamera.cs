using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HackableCamera : HackableObject {

    // Start is called before the first frame update

    public bool panful;
    public float speed;
    public CanvasGroup panOverlay;
    // Bounds for movement
    const float LOWER_BOUND = -5f,
        UPPER_BOUND = 5f;

    void Start () {
        panful = false;
        panelNo = 1;
        command_library = new Dictionary<string, Command> { { "pan", new Pan (GameObject.FindObjectOfType<Commands> (), this) }
        };
    }

    // Update is called once per frame
    void Update () {
        if (panful) {
            if (Input.GetKey (KeyCode.A) && transform.position.x > LOWER_BOUND) {
                GetComponent<Camera> ().transform.Translate (Vector2.left * Time.deltaTime * speed);
            } else if (Input.GetKey (KeyCode.D) && transform.position.x < UPPER_BOUND) {
                GetComponent<Camera> ().transform.Translate (Vector2.right * Time.deltaTime * speed);
            } else if (Input.GetKey (KeyCode.Q)) {
                panOverlay.alpha = 0f;
                panOverlay.blocksRaycasts = false;
                panful = false;
                terminal.PrintLine ("<color=\"blue\">Panning Disabled.</color>");
            }
        }
    }

    class Pan : Command {

        private HackableCamera camera;

        public Pan (Commands com, HackableCamera target_camera) : base (com) {
            name = "pan";
            description = "turns pan mode on. Move camera left with A, and left with D. Press Q to exit pan mode";
            usage = "pan";
            camera = target_camera;
        }

        public override void Action (string[] args) {
            camera.panful = true;
            camera.panOverlay.alpha = 1f;
            camera.panOverlay.blocksRaycasts = true;
            comRef.PrintToTerminal ("<color=\"blue\">Panning Enabled. Press A and D to pan</color>");
        }
    }

    // public override GameObject AddObjects () { return this.gameObject; }

}