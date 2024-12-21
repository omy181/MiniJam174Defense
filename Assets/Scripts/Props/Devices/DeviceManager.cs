using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : Singleton<DeviceManager>
{
    public Device UpperShield;
    public Device BottomShield;
    public Device Heater;
    public Device Thruster;

    private Dictionary<int, Device> _angleToDevice = new();
    private Device _currentOnDevice;
    private void Start()
    {
        _angleToDevice.Add(270,UpperShield);
        _angleToDevice.Add(90, BottomShield);
        _angleToDevice.Add(360, Thruster);
        _angleToDevice.Add(0, Thruster);
        _angleToDevice.Add(180, Heater);
    }


    [Server] public void PowerOnDeviceByAngle(int angle)
    {

        if (angle == -1)
        {
            _currentOnDevice?.RpcPowerOff();
            _currentOnDevice = null;
        }
        else
        {
            if(_angleToDevice.TryGetValue(angle, out Device newDevice)){
                if(newDevice != _currentOnDevice)
                {
                    _currentOnDevice?.RpcPowerOff();
                    _currentOnDevice = newDevice;
                    _currentOnDevice?.RpcPowerOn();
                }
            }
        }
    }

}
