using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Digicode : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private GameObject computerLight;
    private GameScript game;

    [SerializeField] bool debugMode = false;

    private bool playerInRange = false;
    private bool inMenu = false;

    private string password = "4895";
    private string inputPassword = string.Empty;

    private bool initializeComputer = true;

    private void Start()
    {
        interactUI.SetActive(false);
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
        computerLight.SetActive(false);
        this.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!game.GetLights())
        {
            if(initializeComputer) Initialization();
            
            DisplayDigicode();
            if (inMenu) WorkDigicode();
        }
    }

    private void Initialization()
    {
        computerLight.SetActive(true);
        this.GetComponent<Collider>().enabled = true;
        initializeComputer = false;
    }

    // display message when close
    private void OnTriggerEnter(Collider playerCollider)
    {
        interactUI.SetActive(true);
        playerInRange = true;
    }

    // deletes message when leaving
    private void OnTriggerExit(Collider playerCollider)
    {
        interactUI.SetActive(false);
        playerInRange = false;
    }

    // display digicode when interacting and close enough
    private void DisplayDigicode()
    {
        if (playerInRange && Input.GetButtonDown("Interact") && !inMenu)
        {
            game.SetDigicode(true);
            inMenu = true;
        }
        if (playerInRange && Input.GetButtonDown("Back") && inMenu)
        {
            game.SetDigicode(false);
            inMenu = false;
        }
    }

    private void WorkDigicode()
    {
        if (!(password?[0..Math.Min(password.Length, inputPassword.Length)]).Equals(inputPassword)) // password wrong
        {
            inputPassword = string.Empty;
            if(debugMode) Debug.Log("Wrong Password");
        }
        else
        {
            if (password.Length == inputPassword.Length) // password correct
            {
                if (debugMode) Debug.Log("Correct Pasword");

                // if password correct break everything
                DisableDigicode();
            }
        }
    }

    // detects button and get number
    public void Button()
    {
        inputPassword += EventSystem.current.currentSelectedGameObject.name;
        if (debugMode) Debug.Log(inputPassword);
    }

    // disable everything and the script
    private void DisableDigicode()
    {
        game.OpenDigicodeDoor();
        game.SetDigicode(false);
        inMenu = false;
        this.GetComponent<Collider>().enabled = false;
        interactUI.SetActive(false);
        computerLight.SetActive(false);
        this.enabled = false;
    }
}
