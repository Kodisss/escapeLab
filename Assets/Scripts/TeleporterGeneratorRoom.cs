using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterGeneratorRoom : Teleporter
{
    [SerializeField] private GameObject myLight;

    private bool initializeTeleporter = true;

    protected override void Start()
    {
        base.Start();
        myLight.SetActive(false);
        GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!game.GetLights())
        {
            if (initializeTeleporter) Initialization();
            if (playerInRange && Input.GetButtonDown("Interact"))
            {
                Activate();
            }
        }
    }

    private void Initialization()
    {
        myLight.SetActive(true);
        GetComponent<Collider>().enabled = true;
        initializeTeleporter = false;
    }
}
