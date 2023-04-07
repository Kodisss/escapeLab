using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // respawn button
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true); // display Game Over Screen
    }
}
