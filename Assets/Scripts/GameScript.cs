using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject lights;
    [SerializeField] private GameObject digicode;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject answerDigicode;
    [SerializeField] private TextMeshProUGUI textPassword;

    private bool alive = true;
    private bool digicodeDisplayed = false;
    private bool digicodeDoor = false;
    private bool lightsOn = true;

    private bool digicodeAnswerVisible = false;
    [SerializeField] private string password = "4895";

    private bool hasKey = false;

    private bool canControl = true;

    [SerializeField] private bool debugMode = false;

    // game initialization
    private void Start()
    {
        gameOverScreen.SetActive(false);
        lights.SetActive(true);
        digicode.SetActive(false);
        key.SetActive(false);

        digicodeDoor = false;
        canControl = true;
        textPassword.text = password;
    }

    // game loop
    private void Update()
    {
        if (!alive) GameOver();
        digicode.SetActive(digicodeDisplayed);
        answerDigicode.SetActive(digicodeAnswerVisible);
        key.SetActive(hasKey);
        IsControlOn();
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

    public bool GetKeyStatus()
    {
        return hasKey;
    }

    public string GetPassword()
    {
        return password;
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
    [ContextMenu("Disable Lights")]
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

    public void DisplayDigicode(bool input)
    {
        digicodeDisplayed = input;
    }

    public void OpenDigicodeDoor()
    {
        digicodeDoor = true;
        if(debugMode) Debug.Log("I OPENED THE DIGICODE DOOR");
    }

    public void ShowDigicodeAnswer(bool input)
    {
        digicodeAnswerVisible = input;
    }

    // CONTROLS GESTION
    private void IsControlOn()
    {
        if (!alive || digicodeDisplayed || digicodeAnswerVisible) canControl = false; else canControl = true;
    }

    // KEY GESTION
    [ContextMenu("Obtain Key")]
    public void ObtainKey()
    {
        hasKey = true;
    }
}
