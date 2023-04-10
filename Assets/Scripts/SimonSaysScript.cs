using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysScript : MonoBehaviour
{
    [SerializeField] private string answer = "213"; // 1 = green, 2 = red, 3 = blue
    [SerializeField] private bool debugMode = false;
    private GameScript game;

    private string inputPassword = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Resolve();
    }

    public void SendInput(string input)
    {
        inputPassword += input;
    }

    private void Resolve()
    {
        if (!(answer?[0..Math.Min(answer.Length, inputPassword.Length)]).Equals(inputPassword)) // password wrong
        {
            inputPassword = string.Empty;
            if (debugMode) Debug.Log("Wrong Password");
        }
        else
        {
            if (answer.Length == inputPassword.Length) // password correct
            {
                game.OpenSimonDoor();
                if (debugMode) Debug.Log("Correct Pasword");
            }
        }
    }
}
