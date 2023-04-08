using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject lights;
    [SerializeField] private GameObject digicode;

    private bool alive = true;
    private bool isDigicode = false;
    private bool digicodeDoor = false;
    private bool lightsOn = true;

    private bool canControl = true;

    [SerializeField] private bool debugMode = false;

    // game initialization
    private void Start()
    {
        gameOverScreen.SetActive(false);
        lights.SetActive(true);
        digicode.SetActive(false);
        digicodeDoor = false;
        canControl = true;
    }

    // game loop
    private void Update()
    {
        if (!alive) GameOver();
        digicode.SetActive(isDigicode);
        isControlOn();
    }

    ///////////////////// GETTERS AND SETTERS ////////////////////////

    public bool GetControl()
    {
        return canControl;
    }

    public void SetAlive(bool input)
    {
        alive = input;
    }

    /////////////////////////// GAME METHODS ///////////////////////////

    // GAME OVER AND GAME RESTART GESTION
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // respawn button
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true); // display Game Over Screen
    }


    // LIGHTS GESTION
    public void DisableLights()
    {
        lights.SetActive(false);
        lightsOn = false;
    }

    public bool GetLights()
    {
        return lightsOn;
    }

    // DIGICODE GESTION
    public bool GetDigicodeDoor()
    {
        return digicodeDoor;
    }

    public void SetDigicode(bool input)
    {
        isDigicode = input;
    }

    public void OpenDigicodeDoor()
    {
        digicodeDoor = true;
        if(debugMode) Debug.Log("I OPENED THE DIGICODE DOOR");
    }

    // CONTROLS GESTION
    private void isControlOn()
    {
        if (!alive || isDigicode) canControl = false; else canControl = true;
    } 
}
