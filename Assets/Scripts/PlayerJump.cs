using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rb;

    // animator
    private Animator animator;

    // variables to jump
    [Header("Jumping Variables")]
    [SerializeField] private float jumpForce = 6f;

    [Header("Ground Checking Stuffs")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;

    // debug mode
    [Header("")]
    [SerializeField] private bool debugMode;

    // start function
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Jump();
    }

    // checks if the player is at 1.1 distance of the ground
    private bool CheckGrounded()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);
        animator.SetBool("isGrounded", isGrounded); // update the animator's bool
        return isGrounded;
    }

    // check if the jump button is pressed and if player is grounded then jump
    private void Jump()
    {
        if (CheckGrounded() && Input.GetButtonDown("Jump"))
        {
            // rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.velocity = Vector3.up * jumpForce;
        }
        if (debugMode)
        {
            if (CheckGrounded()) Debug.Log("Am Grounded");
        }
    }
}
