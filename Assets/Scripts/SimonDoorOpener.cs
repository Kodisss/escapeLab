using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonDoorOpener : Openers
{
    protected override void TriggerAnimation()
    {
        if (game.IsSimonDoorOpened()) base.TriggerAnimation();
    }
}
