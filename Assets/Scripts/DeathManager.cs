using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private GameScript game;
    public bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is alive
        IsAlive();
        if (!alive) game.GameOver();
    }

    // tracks everything that kills the player
    private void IsAlive()
    {
        if (transform.position.y < -7.0f)
        {
            alive = false;
        }
    }
}
