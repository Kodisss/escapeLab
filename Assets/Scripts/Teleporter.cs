using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleporter : InteractObjects
{
    [SerializeField] protected Transform player;
    [SerializeField] protected Transform roomToTp;
    [SerializeField] protected TextMeshProUGUI textTp;
    [SerializeField] protected string roomName;

    protected override void Start()
    {
        base.Start();
        textTp.text = "Teleport to " + roomName;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (playerInRange && Input.GetButtonDown("Interact"))
        {
            Activate();
        }
    }

    protected override void Activate()
    {
        player.position = roomToTp.position;
    }
}
