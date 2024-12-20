using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : Singleton<StarManager>
{
    [SerializeField] private ParticleSystem[] _particleSystems;

    private float _slowSpeed = 0.2f;
    private float _fastSpeed = 8;

    private float _currentSpeed;
    private float _desSpeed;

    private void Update()
    {
        if (Mathf.Abs(_desSpeed - _currentSpeed) > 0.1f)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, _desSpeed, Time.deltaTime * (_fastSpeed - _slowSpeed)/2);
            _setSpeed();
        }

    }
    private void _setSpeed()
    {
        foreach (var system in _particleSystems)
        {
            var main = system.main;
            main.simulationSpeed = _currentSpeed;
        }

    }
    public void SetSpeedFast()
    {
        _desSpeed = _fastSpeed;
    }

    public void SetSpeedSlow()
    {
        _desSpeed = _slowSpeed;
    }
}
