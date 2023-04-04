using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    // variables to move
    [Header("Moving Variables")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 1080f;
    private Vector3 input;
    private bool canMove = true;

    // variables to dash
    [Header("Dashing Variables")]
    [SerializeField] private float dashDistance = 3f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 1.5f;

    private bool isDashing = false;
    private float dashStartTime = 0f;
    private float lastDashTime = -999f;

    // debug mode
    [Header("")]
    [SerializeField] private bool debugMode;

    // start function
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GatherInput();
        Look();
        Dash();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // gets the input from whatever controller
    private void GatherInput()
    {
        if (canMove) input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // Raw makes it non analogical, wich is weird with a controller but removing it makes the keyboard controll laggy but seems to work anyway
    }

    // makes the player look towards the direction of the movement
    private void Look()
    {
        if (input == Vector3.zero) return; // this is used so the player doesn't default to a looking position after moving

        var rot = Quaternion.LookRotation(input.ToIso(), Vector3.up); // gathers the rotation from controller inputs and offsets it by 45° with the ToIso function
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime); // applies the rotation to the player at a given speed
    }

    // makes the player move
    private void Move()
    {
        rb.MovePosition(transform.position + input.ToIso() * speed * Time.deltaTime); // use the input offseted by 45° with ToIso and make the player move to given speed
    }

    // dash function
    private void Dash()
    {
        if (Input.GetButtonDown("Fire2") && Time.time - lastDashTime > dashCooldown)
        {
            isDashing = true;
            canMove = false;
            dashStartTime = Time.time;
            lastDashTime = Time.time;

            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0, 1, 0));
        }

        // If dashing, move the player in the dash direction
        if (isDashing && Time.time - dashStartTime < dashDuration)
        {
            rb.velocity = transform.forward * dashDistance / dashDuration;
        }
        else
        {
            isDashing = false;
            canMove = true;
            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0,1,0));
        }
    }
}