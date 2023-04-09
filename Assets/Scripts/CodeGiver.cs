using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CodeGiver : InteractObjects
{
    [SerializeField] private TextMeshProUGUI textBox;

    // display message when close
    protected override void OnTriggerEnter(Collider playerCollider)
    {
        if (!game.GetKeyStatus())
        {
            textBox.text = "Need Key";
        }
        else
        {
            textBox.text = "Get Digicode";
        }
        base.OnTriggerEnter(playerCollider);
    }

    protected override void Activate()
    {
        if(game.GetKeyStatus()) game.ShowDigicode(true);
    }

    protected override void Deactivate()
    {
        game.ShowDigicode(false);
    }
}
