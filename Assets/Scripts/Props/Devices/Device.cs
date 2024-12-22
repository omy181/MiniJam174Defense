using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device : NetworkBehaviour
{
    private bool _power;
    public bool Power => _power;

    private void Start()
    {
        _onPowerOff();
    }
    private void Update()
    {
        if(_power)
            _run();
    }
    protected abstract void _run();

    [ClientRpc]
    public void RpcPowerOn()
    {
        _power = true;
        _onPowerOn();

        HolyFmodAudioController.PlayOneShot(HolyFmodAudioReferences.instance.ActivateDevice,Vector3.zero);
    }

    protected virtual void _onPowerOn() { }

    [ClientRpc]
    public void RpcPowerOff()
    {
        _power = false;
        _onPowerOff();
    }

     protected virtual void _onPowerOff() { }
}
