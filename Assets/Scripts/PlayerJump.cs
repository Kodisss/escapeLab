using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rb;

    // variables to jump
    [Header("Jumping Variables")]
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    // debug mode
    [Header("")]
    [SerializeField] private bool debugMode;

    // start function
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
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
            // rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.velocity = Vector3.up * jumpForce;
        }
    }
}
