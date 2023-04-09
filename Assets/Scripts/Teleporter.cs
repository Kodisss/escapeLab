using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : InteractObjects
{
    [SerializeField] private GameObject Light;
    [SerializeField] private Transform player;

    private bool initializeTeleporter = true;

    protected override void Start()
    {
        base.Start();
        Light.SetActive(false);
        GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!game.GetLights())
        {
            if (initializeTeleporter) Initialization();
        }
    }

    private void Initialization()
    {
        Light.SetActive(true);
        GetComponent<Collider>().enabled = true;
        initializeTeleporter = false;
    }

    protected override void Activate()
    {
        player.position = Vector3.zero;
    }
}
