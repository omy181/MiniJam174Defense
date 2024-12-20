using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device : MonoBehaviour
{
    private bool _power;
    private void Update()
    {
        if(_power)
            _run();
    }
    protected abstract void _run();

    public void PowerOn()
    {
        _power = true;
    }

    public void PowerOff()
    {
        _power = false;
    }


    //              device secmek icin collision kullanma, onun yerine bir dictionaryde her aci icin bir device olsun, snaplendiginde o device acilsin
}
