using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSaturationChange : SaturationHandler
{
    public override void IncreaseSaturation()
    {
        // this does nothing
        // only needed for debug purposes
    }
}
