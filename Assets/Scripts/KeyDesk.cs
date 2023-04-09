using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyDesk : InteractObjects
{
    protected override void Activate()
    {
        game.ObtainKey();
        Disable();
    }
}
