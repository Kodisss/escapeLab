using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private GameScript game;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        IsAlive();
    }

    private void IsAlive()
    {
        if (transform.position.y < -1.0f)
        {
            game.GameOver();
        }
    }
}
