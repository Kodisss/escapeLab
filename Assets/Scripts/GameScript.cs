using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject lights;
    private bool alive = true;

    // game initialization
    private void Start()
    {
        gameOverScreen.SetActive(false);
        lights.SetActive(true);
    }

    // game loop
    private void Update()
    {
        if (!alive) GameOver();
    }

    /// GETTERS AND SETTERS ///

    public bool GetAlive()
    {
        return alive;
    }

    public void SetAlive(bool input)
    {
        alive = input;
    }

    /// GAME METHODS ///

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // respawn button
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true); // display Game Over Screen
    }

    private void DisableLights()
    {
        lights.SetActive(false);
    }
}
