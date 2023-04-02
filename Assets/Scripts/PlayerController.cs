using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variables
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5;
    [SerializeField] private float turnSpeed = 1080;
    private Vector3 input;

    private void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // gets the input from whatever controller
    private void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
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
        rb.MovePosition(transform.position + input.ToIso().normalized * speed * Time.deltaTime); // use the input offseted by 45° with ToIso and make the player move to given speed
    }
}