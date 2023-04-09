using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Generator : InteractObjects
{
    protected override void Activate()
    {
        game.DisableLights();
        Disable();
    }
}
