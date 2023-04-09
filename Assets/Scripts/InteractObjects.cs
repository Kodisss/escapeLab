using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObjects : MonoBehaviour
{
    [SerializeField] protected Collider playerCollider;
    [SerializeField] protected GameObject interactUI;
    protected GameScript game;

    protected bool playerInRange = false;
    protected bool inMenu = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        interactUI.SetActive(false);
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameScript>();
    }

    protected virtual void Update()
    {
        if (playerInRange && Input.GetButtonDown("Interact") && !inMenu)
        {
            Activate();
            inMenu = true;
        }
        if (playerInRange && Input.GetButtonDown("Back") && inMenu)
        {
            Deactivate();
            inMenu = false;
        }
    }

    // display message when close
    protected virtual void OnTriggerEnter(Collider playerCollider)
    {
        interactUI.SetActive(true);
        playerInRange = true;
    }

    // deletes message when leaving
    protected virtual void OnTriggerExit(Collider playerCollider)
    {
        interactUI.SetActive(false);
        playerInRange = false;
    }

    protected virtual void Activate(){}

    protected virtual void Deactivate(){}

    protected virtual void Disable()
    {
        inMenu = false;
        GetComponent<Collider>().enabled = false;
        interactUI.SetActive(false);
        enabled = false;
    }
}
