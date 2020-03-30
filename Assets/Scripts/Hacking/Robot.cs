using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Robot : HackableObject {

    // speed of robot in game
    public float speed;

    private Rigidbody2D rigidBody;

    // runs at start of game
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // runs every frame
    void Update() {
        float horiz = 0, vert = 0;

        // if (Input.GetKey(KeyCode.W))
        //     vert += 1;
        // if (Input.GetKey(KeyCode.S))
        //     vert -= 1;

        if (Input.GetKey(KeyCode.D))
            horiz += 1;
        if (Input.GetKey(KeyCode.A))
            horiz -= 1;
        
        Vector2 move = new Vector2(horiz, vert);
        move = move.normalized * speed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.position + move);
    }

}