using FMOD.Studio;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterDevice : Device
{
    [SerializeField] private ParticleSystem _fire;
    private EventInstance _thrusterInstance;

    protected override void Start()
    {
        base.Start();
        _thrusterInstance = HolyFmodAudioController.CreateEventInstance(HolyFmodAudioReferences.instance.Thruster);
    }
    protected override void _onPowerOff()
    {
        _fire.Stop();
        StarManager.instance.SetSpeedSlow();
        _thrusterInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }


    protected override void _onPowerOn()
    {
        _fire.Play();
        StarManager.instance.SetSpeedFast();
        _thrusterInstance.start();
    }

    protected override void _run()
    {
        ShipManager.instance.RunShip();
    }
}
