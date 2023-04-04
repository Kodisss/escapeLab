using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables to move
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    private Vector3 input;

    // variables to jump
    private bool isGrounded;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundMask;

    // debug mode
    [SerializeField] private bool debugMode;

    private void Update()
    {
        GatherInput();
        Look();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
        CheckGrounded();
    }

    // gets the input from whatever controller
    private void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // Raw makes it non analogical, wich is weird with a controller but removing it makes the keyboard controll laggy but seems to work anyway
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

    // checks if the player is at 1.1 distance of the ground
    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundMask);
        if (debugMode) if (isGrounded) Debug.Log("Am Grounded");
    }

    // check if the jump button is pressed to jump by addind a force
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}