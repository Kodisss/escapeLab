using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    private GameScript game;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    private void OnTriggerEnter(Collider playerCollider)
    {
        game.YouWin();
    }
}
