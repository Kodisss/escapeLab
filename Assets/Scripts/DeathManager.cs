using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private GameScript game;

    // Start is called before the first frame update
    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        // check if player is alive
        IsAlive();
    }

    // tracks everything that kills the player
    private void IsAlive()
    {
        if (transform.position.y < -7.0f)
        {
            game.SetAlive(false);
        }
    }
}