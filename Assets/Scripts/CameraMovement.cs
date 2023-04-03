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
    [SerializeField] private float bonkSize; // size that should be bonked to
    [SerializeField] private bool debugMode;

    private bool[] bonkDirection = new bool[4]; // saves the 4 directions of the player to know if they bonked a wall
    private float[] bonkDistance = new float[4]; // saves the 4 distances in the 4 directions

    private Vector3 input; // saves the user's inputs
    private Vector3 currentVelocity = Vector3.zero; // a variable set to zero to use the smoothDamp function 

    private void Update()
    {
        // game loop to gather everything we need
        GatherInput(); // user input to calculate the offset
        DoesBonk(ref bonkDirection, ref bonkDistance, bonkSize); // direction and distance from walls to stop moving camera

        // check if we're in debug mode
        if (debugMode) debugFunction();
    }

    // moves the camera smoothly with an offset so you see where you go
    private void LateUpdate()
    {
        // game loop to do stuff based on the information we got from Update
        MoveCamera();
    }

    // does the camera bonk a wall and in wich direction?
    private void DoesBonk(ref bool[] bonkBool, ref float[] bonkFloat, float size)
    {
        bonkBool[0] = Physics.Raycast(target.position, Vector3.right, out RaycastHit hit0, size, mask);
        bonkFloat[0] = hit0.distance;
        bonkBool[1] = Physics.Raycast(target.position, Vector3.forward, out RaycastHit hit1, size, mask);
        bonkFloat[1] = hit1.distance;
        bonkBool[2] = Physics.Raycast(target.position, Vector3.left, out RaycastHit hit2, size, mask);
        bonkFloat[2] = hit2.distance;
        bonkBool[3] = Physics.Raycast(target.position, Vector3.back, out RaycastHit hit3, size, mask);
        bonkFloat[3] = hit3.distance;
    }

    // makes the camera follow the player with a collision detection so it cannot hover over walls, holes and OOB in general
    private void MoveCamera()
    {
        // if no collision move as normal
        if (!bonkDirection[0] && !bonkDirection[1] && !bonkDirection[2] && !bonkDirection[3])
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position + input.ToIso().normalized * offset, ref currentVelocity, smoothTime); // move camera as normal
        }
        else
        {
            // this one is top right
            if (bonkDirection[0])
            {
                if(debugMode) Debug.Log("I bonk topright");
                transform.position = Vector3.SmoothDamp(transform.position, target.position + input.ToIso().normalized * offset - Vector3.right * (bonkSize - bonkDistance[0]), ref currentVelocity, smoothTime);
            }
            // this one is top left
            if (bonkDirection[1])
            {
                if (debugMode) Debug.Log("I bonk topleft");
                transform.position = Vector3.SmoothDamp(transform.position, target.position + input.ToIso().normalized * offset - Vector3.forward * (bonkSize - bonkDistance[1]), ref currentVelocity, smoothTime);
            }
            // this one is bottom left
            if (bonkDirection[2])
            {
                if (debugMode) Debug.Log("I bonk botleft");
                transform.position = Vector3.SmoothDamp(transform.position, target.position + input.ToIso().normalized * offset - Vector3.left * (bonkSize - bonkDistance[2]), ref currentVelocity, smoothTime);
            }
            // this one is bottom right
            if (bonkDirection[3])
            {
                if (debugMode) Debug.Log("I bonk botright");
                transform.position = Vector3.SmoothDamp(transform.position, target.position + input.ToIso().normalized * offset - Vector3.back * (bonkSize - bonkDistance[3]), ref currentVelocity, smoothTime);
            }
        }
    }

    // gets the input from whatever controller
    private void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // Raw makes it non analogical, wich is weird with a controller but removing it makes the keyboard controll laggy (look into that)
    }

    // debug mode function
    private void debugFunction()
    {
        // draws rays that materialize player collision detection
        Debug.DrawRay(target.position, Vector3.forward * bonkSize, Color.green);
        Debug.DrawRay(target.position, Vector3.right * bonkSize, Color.green);
        Debug.DrawRay(target.position, Vector3.left * bonkSize, Color.green);
        Debug.DrawRay(target.position, Vector3.back * bonkSize, Color.green);
    }
}