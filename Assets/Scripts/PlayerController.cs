using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private Vector3 lastPosition;

    // variables to move
    [Header("Moving Variables")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 1080f;
    private Vector3 input;
    private bool canMove = true;

    // variables to dash
    [Header("Dashing Variables")]
    [SerializeField] private float dashDistance = 3f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 1.5f;
    [SerializeField] private int maxDashes = 2;

    private bool isDashing = false;
    private bool isCooldown = false;
    private float dashStartTime = 0f;
    private float lastDashTime = -999f;
    private int currentDashes = 0;

    // debug mode
    [Header("")]
    [SerializeField] private bool debugMode;

    // start function
    private void Start()
    {
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
        updateAnimator();
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

        var rot = Quaternion.LookRotation(input.ToIso().ToLook(), Vector3.up); // gathers the rotation from controller inputs and offsets it by 45° with the ToIso function
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime); // applies the rotation to the player at a given speed
    }

    // makes the player move
    private void Move()
    {
        rb.MovePosition(transform.position + input.ToIso() * speed * Time.deltaTime); // use the input offseted by 45° with ToIso and make the player move to given speed
    }

    private void updateAnimator()
    {
        Vector3 currentPosition = transform.position;
        Vector3 movementVector = currentPosition - lastPosition;
        float velocity = movementVector.magnitude / Time.deltaTime;
        animator.SetFloat("Velocity", velocity);
        lastPosition = currentPosition;
    }

    // dash function
    private void Dash()
    {
        isCooldown = Time.time - lastDashTime > dashCooldown; // tracks the cooldown

        if (Input.GetButtonDown("Fire2") && (isCooldown || currentDashes < maxDashes))
        {
            // Allow dashing and kills the input gathering
            isDashing = true;
            canMove = false;

            

            // Variables to track Cooldown
            dashStartTime = Time.time;
            lastDashTime = Time.time;

            currentDashes++; // counts the number of dashes

            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0, 1, 0)); // sets velocity to 0 before dashing so it doesn't accelerate weirdly
            if (debugMode) Debug.Log("Tried to Dash");
        }

        if (isCooldown && currentDashes == maxDashes)
        {
            currentDashes = 0;
            if (debugMode) Debug.Log("Reset Dash Count");
        }

        // If dashing, move the player in the dash direction
        if (isDashing && Time.time - dashStartTime < dashDuration)
        {
            //animator.SetBool("Dash", true);
            rb.velocity = transform.forward * dashDistance / dashDuration;
            //animator.SetBool("Dash", false);
            //StartCoroutine(StopDashAfterDelay());
            if (debugMode) Debug.Log("Dashed");
            
        }
        else
        {
            if (isCooldown)
            {
                currentDashes = 0;
                if (debugMode) Debug.Log("Reset Dash Count");
            }
            // resets everything so we can dash again
            isDashing = false;
            canMove = true;
            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0,1,0)); // sets velocity to 0 after dashing so we can't build up momentum
        }
    }

}