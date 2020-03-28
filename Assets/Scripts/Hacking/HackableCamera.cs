using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 
public class HackableCamera : HackableObject {

    // Start is called before the first frame update
    void Start () {
        panelNo = 1;
        command_library = new Dictionary<string, Command> {
            {"pan", new Pan(GameObject.FindObjectOfType<Commands>(), this.gameObject)}
        };
    }

    // Update is called once per frame
    void Update () {

    }

    class Pan : Command {

        private GameObject camera;
        private bool panful;
        public int speed;
        public Pan (Commands com, GameObject target_camera) : base (com) {
            name = "pan";
            description = "turns pan mode on. Move camera left with A, and left with D. Press Q to exit pan mode";
            usage = "pan";
            camera = target_camera;
            speed = 1;
            panful = false;
        }

        void Update()
        {
            if (panful)
                CameraMovement();
        }
        public override void Action(string[] args) {
            panful = true;
        }

        public void CameraMovement()
        {
            if (Input.GetKey(KeyCode.A)) { camera.transform.Translate(Vector2.left * Time.deltaTime * speed);}
            else if (Input.GetKey(KeyCode.D)) { camera.transform.Translate(Vector2.right * Time.deltaTime * speed);}
        }

    }

    

    // public override GameObject AddObjects () { return this.gameObject; }
     

}