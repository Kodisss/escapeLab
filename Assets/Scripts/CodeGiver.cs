using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CodeGiver : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private TextMeshProUGUI textBox;
    private GameScript game;

    private bool playerInRange = false;
    private bool inMenu = false;

    private void Start()
    {
        interactUI.SetActive(false);
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(game.GetKeyStatus()) DisplayDigicode();
    }

    // display message when close
    private void OnTriggerEnter(Collider playerCollider)
    {
        if (!game.GetKeyStatus())
        {
            textBox.text = "Need Key";
        }
        else
        {
            textBox.text = "Get Digicode";
        }
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
    private void DisplayDigicode()
    {
        if (playerInRange && Input.GetButtonDown("Interact") && !inMenu)
        {
            game.ShowDigicode(true);
            interactUI.SetActive(false);
            inMenu = true;
        }
        if (playerInRange && Input.GetButtonDown("Back") && inMenu)
        {
            game.ShowDigicode(false);
            interactUI.SetActive(true);
            inMenu = false;
        }
    }
}
