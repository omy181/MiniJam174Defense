using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterDevice : Device
{
    [SerializeField] private GameObject _fire;
    protected override void _onPowerOff()
    {
        _fire.gameObject.SetActive(false);
    }

    protected override void _onPowerOn()
    {
        _fire.gameObject.SetActive(true);
    }

    protected override void _run()
    {
        ShipManager.instance.RunShip();
    }
}
