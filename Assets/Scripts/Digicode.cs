using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Digicode : InteractObjects
{
    [SerializeField] private GameObject computerLight;
    [SerializeField] bool debugMode = false;

    private string inputPassword = string.Empty;
    private bool initializeComputer = true;

    protected override void Start()
    {
        base.Start();
        computerLight.SetActive(false);
        GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!game.GetLights())
        {
            if(initializeComputer) Initialization();
            if (inMenu) WorkDigicode();
        }
    }

    private void Initialization()
    {
        computerLight.SetActive(true);
        this.GetComponent<Collider>().enabled = true;
        initializeComputer = false;
    }

    protected override void Activate()
    {
        game.SetDigicode(true);
    }

    protected override void Deactivate()
    {
        game.SetDigicode(false);
    }

    private void WorkDigicode()
    {
        if (!(game.GetPassword()?[0..Math.Min(game.GetPassword().Length, inputPassword.Length)]).Equals(inputPassword)) // password wrong
        {
            inputPassword = string.Empty;
            if(debugMode) Debug.Log("Wrong Password");
        }
        else
        {
            if (game.GetPassword().Length == inputPassword.Length) // password correct
            {
                if (debugMode) Debug.Log("Correct Pasword");

                // if password correct break everything
                Disable();
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
    protected override void Disable()
    {
        game.OpenDigicodeDoor();
        game.SetDigicode(false);
        computerLight.SetActive(false);
        base.Disable();
    }
}
