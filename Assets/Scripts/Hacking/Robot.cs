using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Robot : HackableObject {

    // speed of robot in game
    public float movementSpeed;

    // for debugging moving state
    public bool moving = false;

    // for flipping robot 
    public bool facingRight = true;

    // to change animation states
    public Animator baseAnimator;

    // to move robot across 2D space
    private Rigidbody2D rigidBody;

    // to maintain moving if A or D is in contact
    private int movingKeys;

    private int horiz, vert;

    // runs at start of game
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        movingKeys = 0;
        horiz = vert = 0;
    }

    // runs every frame
    void Update() {

        // if not active,
        // no functionality available
        if (!active)
            return;

        // variables needed to determine state changes
        int newMovingKeys = movingKeys; // Movement/Idle state change
        bool newDirRight = facingRight; // Left/right facing state change

        // For possible vertical control, here is base code
        // if (Input.GetKey(KeyCode.W))
        //     vert += 1;
        // if (Input.GetKey(KeyCode.S))
        //     vert -= 1;

        // Determine if robot is in state of moving or not
        // Also determine state change
        if (Input.GetKeyDown(KeyCode.A)) {
            newMovingKeys++;
            newDirRight = false;
            horiz -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            newDirRight = true;
            horiz += 1;
            newMovingKeys++;
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            newMovingKeys--;
            horiz += 1;
            if (horiz > 0)
                newDirRight = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            newMovingKeys--;
            horiz -= 1;
            if (horiz < 0)
                newDirRight = false;
        }

        // Flip Robot
        if (newDirRight != facingRight)
        {
            facingRight = newDirRight;
            gameObject.transform.Rotate(new Vector2(0, 180));
        }

        // Movement Animation
        if (newMovingKeys != movingKeys) {
            // only one key (A or D), not both
            // must be active
            if (newMovingKeys == 1) {
                baseAnimator.ResetTrigger("Idle");
                baseAnimator.SetTrigger("Motion");
                baseAnimator.SetBool("Moving", moving=true);
            } else {
                baseAnimator.ResetTrigger("Motion");
                baseAnimator.SetTrigger("Idle");
                baseAnimator.SetBool("Moving", moving=false);
            }
            movingKeys = newMovingKeys;
        }

        // Vector Change
        Vector2 move = new Vector2(horiz, vert);
        move = move.normalized * movementSpeed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.position + move);
    }

}