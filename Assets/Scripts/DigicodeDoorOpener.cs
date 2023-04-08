using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigicodeDoorOpener : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private GameScript game;

    // Start is called before the first frame update
    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (game.GetDigicodeDoor()) animator.SetTrigger("digicode");
    }
}
