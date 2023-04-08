using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digicode : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    [SerializeField] private GameObject interactUI;
    private Collider myCollider;
    private GameScript game;

    [SerializeField] bool debugMode = false;

    private bool playerInRange = false;
    private bool inMenu = false;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
        interactUI.SetActive(false);
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayDigicode();
    }

    void OnTriggerEnter(Collider playerCollider)
    {
        interactUI.SetActive(true);
        playerInRange = true;
        if (debugMode) Debug.Log("PLAYER IN RANGE OMG");
    }

    void OnTriggerExit(Collider playerCollider)
    {
        interactUI.SetActive(false);
        playerInRange = false;
        if (debugMode) Debug.Log("PLAYER OUT OF RANGE NOOOO");
    }

    void DisplayDigicode()
    {
        if (playerInRange && Input.GetButtonDown("Interact") && !inMenu)
        {
            game.SetDigicode(true);
            inMenu = true;
            if (debugMode) Debug.Log("AM IN RANGE AND TRYING TO DISPLAY DIGICODE");
        }
        if (playerInRange && Input.GetButtonDown("Back") && inMenu)
        {
            game.SetDigicode(false);
            inMenu = false;
            if (debugMode) Debug.Log("AM WORKING FOR NO REASON");
        }
    }
}
