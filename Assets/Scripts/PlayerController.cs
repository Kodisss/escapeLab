using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // speed of movement
    public float jumpForce = 2.0f; // force of jump
    public float cameraFollowSpeed = 5.0f; // speed of camera follow
    public float cameraDistance = 10.0f; // distance from camera to player
    public float cameraHeight = 5.0f; // height of camera above player

    private bool isGrounded;
    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // calculate movement direction based on input
        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = mainCamera.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;
        moveDirection.Normalize();
        moveDirection *= speed;

        // Check if the character is on the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.5f);
        Debug.Log(isGrounded + "\n");

        // jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // move the player using rigidbody
        rb.MovePosition(transform.position + moveDirection * Time.deltaTime);

        // calculate camera position based on player position
        Vector3 cameraTargetPosition = transform.position - mainCamera.transform.forward * cameraDistance;
        cameraTargetPosition.y = transform.position.y + cameraHeight;

        // smoothly move the camera to the target position
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraTargetPosition, cameraFollowSpeed * Time.deltaTime);

        // rotate the player to face the direction of movement
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
}