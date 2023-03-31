using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool jumpPressed;
    private Transform mainCameraTransform;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        mainCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Keyboard input
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        // Gamepad input
        /*if (xInput == 0)
        {
            xInput = Input.GetAxisRaw("GamepadHorizontal");
        }
        if (yInput == 0)
        {
            yInput = Input.GetAxisRaw("GamepadVertical");
        }*/

        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        movement = Quaternion.Euler(0f, mainCameraTransform.rotation.eulerAngles.y, 0f) * movement;
        movement.Normalize();
        rb.velocity = movement * moveSpeed;

        if (jumpPressed && isGrounded)
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            jumpPressed = false;
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }
}
