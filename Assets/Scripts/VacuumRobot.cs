using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class VacuumRobot : MonoBehaviour {
    // speed of robot in game
    public float movementSpeed;

    // for debugging moving state
    public bool moving = true;

    // for flipping robot 
    public bool facingRight = false;

    public CanvasGroup panOverlay;

    // to change animation states, if needed
    public Animator baseAnimator;

    // to move robot across 2D space
    private Rigidbody2D rigidBody;

    private int horiz, vert;

    // runs at start of game
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        horiz = vert = 0;
    }

    // runs every frame
    void Update()
    {
        horiz = vert = 0;

        horiz = -1; // move left

        // Vector Change
        Vector2 move = new Vector2(horiz, vert);
        move = move.normalized * movementSpeed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.position + move);
    }

}