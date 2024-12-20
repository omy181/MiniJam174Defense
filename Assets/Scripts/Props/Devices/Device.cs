using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device : MonoBehaviour
{
    private bool _power;

    private void Start()
    {
        PowerOff();
    }
    private void Update()
    {
        if(_power)
            _run();
    }
    protected abstract void _run();

    public void PowerOn()
    {
        _power = true;
        _onPowerOn();
    }

    protected abstract void _onPowerOn();

    public void PowerOff()
    {
        _power = false;
        _onPowerOff();
    }

    protected abstract void _onPowerOff();
}
