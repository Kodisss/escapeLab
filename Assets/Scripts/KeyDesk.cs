using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyDesk : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    [SerializeField] private GameObject interactUI;
    private GameScript game;

    private bool playerInRange = false;

    private void Start()
    {
        interactUI.SetActive(false);
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        WorkDesk();
    }

    // display message when close
    private void OnTriggerEnter(Collider playerCollider)
    {
        interactUI.SetActive(true);
        playerInRange = true;
    }

    // deletes message when leaving
    private void OnTriggerExit(Collider playerCollider)
    {
        interactUI.SetActive(false);
        playerInRange = false;
    }

    // display digicode when interacting and close enough
    private void WorkDesk()
    {
        if (playerInRange && Input.GetButtonDown("Interact"))
        {
            game.GotKey();
            DisableDesk();
        }
    }

    // disable everything and the script
    private void DisableDesk()
    {
        this.GetComponent<Collider>().enabled = false;
        interactUI.SetActive(false);
        this.enabled = false;
    }
}
