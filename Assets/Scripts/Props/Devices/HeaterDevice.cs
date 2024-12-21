using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaterDevice : Device
{


    protected override void _onPowerOff()
    {
        
    }


    protected override void _onPowerOn()
    {
        
    }

    protected override void _run()
    {
        ShipManager.instance.HeatShip();
    }
}
