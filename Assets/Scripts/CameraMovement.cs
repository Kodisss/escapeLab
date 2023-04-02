using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // variables
    [SerializeField] private Transform target; // target to be followed

    [SerializeField] private float smoothTime; // adjusts the smoothness of the camera following
    [SerializeField] private float offset; // offset to make the camera a little be early so the user can see where they go
    [SerializeField] private LayerMask mask; // mask of what shoud bonk the camera
    [SerializeField] private float bonkRadius; // radius that should be bonked to

    private Vector3 input;
    private Vector3 currentVelocity = Vector3.zero; // a variable set to zero to use the smoothDamp function

    private void Update()
    {
        GatherInput();
    }

    // moves the camera smoothly with an offset so you see where you go
    private void LateUpdate()
    {
        if (!DoesBonk())
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position + input.ToIso().normalized * offset, ref currentVelocity, smoothTime);
        }
    }

    // does the camera bonk a wall?
    private bool DoesBonk()
    {
        return Physics.CheckSphere(target.transform.position, bonkRadius, mask);
    }

    // gets the input from whatever controller
    private void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // Raw makes it non analogical, wich is weird with a controller but removing it makes the keyboard controll laggy (look into that)
    }
}