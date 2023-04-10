using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winningScreen;
    [SerializeField] private GameObject lights;
    [SerializeField] private GameObject digicode;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject answerDigicode;
    [SerializeField] private TextMeshProUGUI textPassword;

    [SerializeField] private float timeLoopDuration = 90f;

    private bool alive = true;
    private bool digicodeDisplayed = false;
    private bool digicodeDoor = false;
    private bool lightsOn = true;
    private bool timerDoorStatus = false;
    private bool hasKey = false;
    private bool canControl = true;
    private bool digicodeAnswerVisible = false;
    private bool simonDoor = false;
    private bool win = false;

    [SerializeField] private string password = "4895";

    [SerializeField] private bool debugMode = false;

    // game initialization
    private void Start()
    {
        StartCoroutine(TimeLoop());

        gameOverScreen.SetActive(false);
        winningScreen.SetActive(false);
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
        if (win) WinScreen();
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

    public bool GetTimerDoorStatus()
    {
        return timerDoorStatus;
    }

    public float GetTimeLoopDuration()
    {
        return timeLoopDuration;
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

    private void WinScreen()
    {
        winningScreen.SetActive(true); // display Winning Screen
    }

    // WIN GESTION

    public void YouWin()
    {
        win = true;
    }

    public bool DidWin()
    {
        return win;
    }

    // TIME LOOP
    private IEnumerator TimeLoop()
    {
        yield return new WaitForSeconds(timeLoopDuration - 10f);
        timerDoorStatus = true;
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
        if (!alive || digicodeDisplayed || digicodeAnswerVisible || win) canControl = false; else canControl = true;
    }

    // KEY GESTION
    [ContextMenu("Obtain Key")]
    public void ObtainKey()
    {
        hasKey = true;
    }

    // SIMON SAYS DOOR GESTION
    public void OpenSimonDoor()
    {
        simonDoor = true;
    }

    public bool IsSimonDoorOpened()
    {
        return simonDoor;
    }
}
