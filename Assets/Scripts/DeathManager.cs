using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathManager : MonoBehaviour
{
    private GameScript game;
    private bool tooLate = false;

    // Start is called before the first frame update
    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
        StartCoroutine(TimeLoop());
    }

    // Update is called once per frame
    private void Update()
    {
        // check if player is alive as long as player didn't win
        if(!game.DidWin()) IsAlive();
    }

    // tracks everything that kills the player
    private void IsAlive()
    {
        if (transform.position.y < -7.0f || tooLate)
        {
            game.SetAlive(false);
        }
    }

    // TIME LOOP
    private IEnumerator TimeLoop()
    {
        yield return new WaitForSeconds(game.GetTimeLoopDuration());
        tooLate = true;
    }
}