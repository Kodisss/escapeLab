using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsDoorOpener : Openers
{
    protected override void TriggerAnimation()
    {
        if(!game.GetLights()) base.TriggerAnimation();
    }
}
