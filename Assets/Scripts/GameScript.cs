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

        if (debugMode) Debug.Log(isDigicode);
    }

    ///////////////////// GETTERS AND SETTERS ////////////////////////

    public bool GetControl()
    {
        return canControl;
    }

    public bool GetAlive()
    {
        return alive;
    }

    public void SetAlive(bool input)
    {
        alive = input;
    }

    public bool GetDigicode()
    {
        return isDigicode;
    }

    public void SetDigicode(bool input)
    {
        isDigicode = input;
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
    private void DisableLights()
    {
        lights.SetActive(false);
    }

    // DIGICODE GESTION
    public bool GetDigicodeDoor()
    {
        return digicodeDoor;
    }

    public void OpenDigicodeDoor()
    {
        digicodeDoor = true;
        if(debugMode) Debug.Log("I OPENED THE DIGICODE DOOR");
    }
}
