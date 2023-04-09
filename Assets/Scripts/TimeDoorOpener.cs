using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDoorOpener : Openers
{
    protected override void TriggerAnimation()
    {
        if (game.GetTimerDoorStatus()) base.TriggerAnimation();
    }
}
