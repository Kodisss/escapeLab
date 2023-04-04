using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    // variables to dash
    [SerializeField] private float dashDistance = 3f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 1.5f;

    private bool isDashing = false;
    private float dashStartTime = 0f;
    private float lastDashTime = -999f;

    // Update is called once per frame
    void Update()
    {
        Dash();
    }

    private void Dash()
    {
        if (Input.GetButtonDown("Fire3") && Time.time - lastDashTime > dashCooldown)
        {
            isDashing = true;
            dashStartTime = Time.time;
            lastDashTime = Time.time;
        }

        // If dashing, move the player in the dash direction
        if (isDashing && Time.time - dashStartTime < dashDuration)
        {
            rb.velocity = transform.forward * dashDistance / dashDuration;
        }
        else
        {
            isDashing = false;
        }
    }
}
