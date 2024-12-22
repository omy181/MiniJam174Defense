using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDevice : Device
{
    [SerializeField] private GameObject _shieldObj;
    protected Vector3 _shieldScale;
    protected override void Start()
    {
        base.Start();
        _shieldScale = _shieldObj.transform.localScale;
    }
    protected override void _onPowerOff()
    {
        _shieldObj.gameObject.LeanCancel();
        _shieldObj.LeanScale(new Vector3(_shieldScale.x,0, _shieldScale.z), 0.2f).setEaseOutQuint();
        //_shieldObj.gameObject.SetActive(false);
    }


    protected override void _onPowerOn()
    {
        //_shieldObj.gameObject.SetActive(true);
        _shieldObj.gameObject.LeanCancel();
        _shieldObj.LeanScale(_shieldScale, 0.2f).setEaseOutQuint();
    }

    protected override void _run()
    {

    }
}
