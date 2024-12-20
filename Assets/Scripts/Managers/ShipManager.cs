using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : Singleton<ShipManager>
{
    [SerializeField] private Slider _roadSlider;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _heatSlider;
    [Space]
    [SerializeField] private ShieldDevice _topShield;
    [SerializeField] private ShieldDevice _bottomShield;

    private float _health;
    public float Health { get => _health; set {
            _health = Mathf.Clamp01(value);
            _healthSlider.value = _health; } }

    private float _road;
    public float Road
    {
        get => _road; set
        {
            _road = Mathf.Clamp01(value);
            _roadSlider.value = _road;
        }
    }

    private float _heat;
    public float Heat
    {
        get => _heat; set
        {
            _heat = Mathf.Clamp01(value);
            _heatSlider.value = _heat;
        }
    }

    public bool isTopShieldActive => _topShield.Power;
    public bool isBottomShieldActive => _bottomShield.Power;

    private void Start()
    {
        Road = 0;
        Health = 1;
        Heat = 1;
    }

    void Update()
    {
        MakeShipColder();
    }

    public void RunShip()
    {
        Road += Time.deltaTime * 0.01f;
    }

    public void HeatShip()
    {
        Heat += Time.deltaTime * 0.3f;
    }

    public void MakeShipColder()
    {
        Heat -= Time.deltaTime * 0.01f;
    }

    public void CollideWithAsteroid()
    {
        Health -= 0.2f;
    }
}
