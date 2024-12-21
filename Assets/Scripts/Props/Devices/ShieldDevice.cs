using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDevice : Device
{
    [SerializeField] private GameObject _shieldObj;

    protected override void _onPowerOff()
    {
        _shieldObj.gameObject.SetActive(false);
    }


    protected override void _onPowerOn()
    {
        _shieldObj.gameObject.SetActive(true);
    }

    protected override void _run()
    {

    }
}
