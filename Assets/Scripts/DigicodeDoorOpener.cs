using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigicodeDoorOpener : Openers
{
    protected override void TriggerAnimation()
    {
        if (game.GetDigicodeDoor()) base.TriggerAnimation();
    }
}
