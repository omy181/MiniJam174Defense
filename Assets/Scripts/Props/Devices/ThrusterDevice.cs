using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterDevice : Device
{
    [SerializeField] private ParticleSystem _fire;

    protected override void _onPowerOff()
    {
        _fire.Stop();
        StarManager.instance.SetSpeedSlow();
    }


    protected override void _onPowerOn()
    {
        _fire.Play();
        StarManager.instance.SetSpeedFast();
    }

    protected override void _run()
    {
        ShipManager.instance.RunShip();
    }
}
