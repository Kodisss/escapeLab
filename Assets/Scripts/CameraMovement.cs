using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // variables
    [SerializeField] private Transform target; // target to be followed

    // Communicate With GameManager
    private GameScript game;

    [SerializeField] private float smoothTime; // adjusts the smoothness of the camera following
    [SerializeField] private float offset; // offset to make the camera a little be early so the user can see where they go
    [SerializeField] private LayerMask mask; // mask of what shoud bonk the camera
    [SerializeField] private float bonkSize; // size that should be bonked to

    [SerializeField] private bool debugMode;

    private bool[] bonkDirection = new bool[4]; // saves the 4 directions of the player to know if they bonked a wall
    private float[] bonkDistance = new float[4]; // saves the 4 distances in the 4 directions
    private float precision = 0.001f;

    private Vector3 input; // saves the user's inputs
    private Vector3 currentVelocity = Vector3.zero; // a variable set to zero to use the smoothDamp function

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

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
        if (game.GetAlive()) MoveCamera();
    }

    // does the camera bonk a wall and in wich direction?
    private void DoesBonk(ref bool[] bonkBool, ref float[] bonkFloat, float size)
    {
        // creates a raycast from the players position towards one of the four nonIsometric directions
        // up to a given size saves the distance between the first mask hit and the player in hit.distance
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
        Vector3 playerPos = Vector3.zero;

        // if no collision move as normal
        if (!bonkDirection[0] && !bonkDirection[1] && !bonkDirection[2] && !bonkDirection[3])
        {
            // move camera by moving from its position to the player's position adding an offset in the direction of user's input so they can see were they go
            // currenVelocity is mandatory and needs to be set to 0 for the function to work and smoothTime is the time it takes for the camera to move

            transform.position = Vector3.SmoothDamp(transform.position, target.position + input.ToIso().normalized * offset, ref currentVelocity, smoothTime);
        }
        // if a collision is detected it works the same but instead goes to target position minus an offset that is greater and greater as the player gets closer to
        // the wall, it knows the direction from the bool variable and the distance from the other arrray
        else
        {   
            // this one is up
            if (bonkDirection[0] && bonkDirection[1])
            {
                // if the raycasts are equal it means we are on a straight wall and not a corner that's why we need to divide by 2 the distance
                //so it doesn't make the camera go double the distance backwards
                if (Mathf.Abs(bonkDistance[0] - bonkDistance[1]) < precision)
                {
                    if (debugMode) Debug.Log("I bonk up straight");
                    playerPos = target.position - Vector3.right * (bonkSize - bonkDistance[0]) / 2 - Vector3.forward * (bonkSize - bonkDistance[1]) / 2;
                }
                else
                {
                    if (debugMode) Debug.Log("I bonk up corner");
                    playerPos = target.position - Vector3.right * (bonkSize - bonkDistance[0]) - Vector3.forward * (bonkSize - bonkDistance[1]);
                }
                
            }
            // this one is left
            else if (bonkDirection[1] && bonkDirection[2])
            {
                if (Mathf.Abs(bonkDistance[1] - bonkDistance[2]) < precision)
                {
                    if (debugMode) Debug.Log("I bonk left straight");
                    playerPos = target.position - Vector3.forward * (bonkSize - bonkDistance[1]) / 2 - Vector3.left * (bonkSize - bonkDistance[2]) / 2;
                }
                else
                {
                    if (debugMode) Debug.Log("I bonk left corner");
                    playerPos = target.position - Vector3.forward * (bonkSize - bonkDistance[1]) - Vector3.left * (bonkSize - bonkDistance[2]);
                }
            }
            // this one is down
            else if (bonkDirection[2] && bonkDirection[3])
            {
                if (Mathf.Abs(bonkDistance[2] - bonkDistance[3]) < precision)
                {
                    if (debugMode) Debug.Log("I bonk down straight");
                    playerPos = target.position - Vector3.left * (bonkSize - bonkDistance[2]) / 2 - Vector3.back * (bonkSize - bonkDistance[3]) / 2;
                }
                else
                {
                    if (debugMode) Debug.Log("I bonk down corner");
                    playerPos = target.position - Vector3.left * (bonkSize - bonkDistance[2]) - Vector3.back * (bonkSize - bonkDistance[3]);
                }
            }
            // this one is right
            else if (bonkDirection[3] && bonkDirection[0])
            {
                if (Mathf.Abs(bonkDistance[3] - bonkDistance[0]) < precision)
                {
                    if (debugMode) Debug.Log("I bonk right straight");
                    playerPos = target.position - Vector3.back * (bonkSize - bonkDistance[3]) / 2 - Vector3.right * (bonkSize - bonkDistance[0]) / 2;
                }
                else
                {
                    if (debugMode) Debug.Log("I bonk right corner");
                    playerPos = target.position - Vector3.back * (bonkSize - bonkDistance[3]) - Vector3.right * (bonkSize - bonkDistance[0]);
                }
            }
            // this one is top right
            else if (bonkDirection[0])
            {
                if(debugMode) Debug.Log("I bonk topright");
                playerPos = target.position - Vector3.right * (bonkSize - bonkDistance[0]);
            }
            // this one is top left
            else if (bonkDirection[1])
            {
                if (debugMode) Debug.Log("I bonk topleft");
                playerPos = target.position - Vector3.forward * (bonkSize - bonkDistance[1]);
            }
            // this one is bottom left
            else if (bonkDirection[2])
            {
                if (debugMode) Debug.Log("I bonk botleft");
                playerPos = target.position - Vector3.left * (bonkSize - bonkDistance[2]);
            }
            // this one is bottom right
            else if (bonkDirection[3])
            {
                if (debugMode) Debug.Log("I bonk botright");
                playerPos = target.position - Vector3.back * (bonkSize - bonkDistance[3]);
            }
            
            transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref currentVelocity, smoothTime);
        }
    }

    // gets the input from whatever controller
    private void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
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