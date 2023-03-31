using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
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
        // Keyboard input & Gamepad (for some reasons)
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        //Player & Camera movements
        Vector3 cameraMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        cameraMovement = Quaternion.Euler(0f, mainCameraTransform.rotation.eulerAngles.y, 0f) * cameraMovement;
        cameraMovement.Normalize();
        transform.position += cameraMovement * moveSpeed * Time.deltaTime;

        //Jump if needed
        if (jumpPressed && isGrounded)
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            jumpPressed = false;
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }
}
