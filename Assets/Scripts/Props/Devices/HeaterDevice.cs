using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaterDevice : Device
{
    [SerializeField] private MeshRenderer _glass;

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

    protected override void Update()
    {
        base.Update();
        if(ShipManager.instance.Heat < 0.5f)
        {
            Holylib.Utilities.HolyUtilities.ChangeMaterialColor(_glass,Color.red,1);
        }
        else
        {
            Holylib.Utilities.HolyUtilities.ChangeMaterialColor(_glass, Color.blue,1);
        }
    }
}
