using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 
public class HackableCamera : HackableObject {

    // Start is called before the first frame update

    public bool panful;
    public float speed;

    void Start () {
        panful = false;
        panelNo = 1;
        speed = 1f;
        command_library = new Dictionary<string, Command> {
            {"pan", new Pan(GameObject.FindObjectOfType<Commands>(), this)}
        };
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(panful);
        if (panful)
        {
            if (Input.GetKey(KeyCode.A)) { GetComponent<Camera>().transform.Translate(Vector2.left * Time.deltaTime * speed);}
            else if (Input.GetKey(KeyCode.D)) { GetComponent<Camera>().transform.Translate(Vector2.right * Time.deltaTime * speed);}
        }
    }

    class Pan : Command {

        private HackableCamera camera;
        
        public int speed;
        public Pan (Commands com, HackableCamera target_camera) : base (com) {
            name = "pan";
            description = "turns pan mode on. Move camera left with A, and left with D. Press Q to exit pan mode";
            usage = "pan";
            camera = target_camera;
            speed = 1;
        }

        
        public override void Action(string[] args) {
            camera.panful = true;
            Debug.Log("HELOOOOO");
        }

    }


    public override void AddCommands(Dictionary<string, Command> library)
    {
        foreach (KeyValuePair<string, Command> entry in command_library)
                library.Add(entry.Key, entry.Value);
    }  

    // public override GameObject AddObjects () { return this.gameObject; }
     

}