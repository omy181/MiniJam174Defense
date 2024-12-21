using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : NetworkSingleton<ShipManager>
{
    [SerializeField] private Slider _roadSlider;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _heatSlider;
    [Space]
    [SerializeField] private ShieldDevice _topShield;
    [SerializeField] private ShieldDevice _bottomShield;
    [SerializeField] private ThrusterDevice _thruster;


    private List<MetalPanel> _metalPanels;

    [SyncVar(hook = nameof(_onHealthChanged))] private float _health;
    public float Health { get => _health; set {
            _health = Mathf.Clamp01(value);
            _healthSlider.value = _health; 
            
            if(_health == 0)
            {
                GameManager.instance.GameOver(false);
            }
        } }

    private void _onHealthChanged(float oldHealth, float newHealth)
    {
        _healthSlider.value = newHealth;
    }

    [SyncVar(hook = nameof(_onRoadChanged))] private float _road;
    public float Road
    {
        get => _road; set
        {
            _road = Mathf.Clamp01(value);
            _roadSlider.value = _road;

            if (_road == 1)
            {
                GameManager.instance.GameOver(true);
            }
        }
    }

    private void _onRoadChanged(float oldRoad, float newRoad)
    {
        _roadSlider.value = newRoad;
    }

    [SyncVar(hook = nameof(_onHeatChanged))] private float _heat;
    public float Heat
    {
        get => _heat; set
        {
            _heat = Mathf.Clamp01(value);
            _heatSlider.value = _heat;

            if (_heat == 0)
            {
                GameManager.instance.GameOver(false);
            }
        }
    }

    private void _onHeatChanged(float oldHeat,float newHeat)
    {
        _heatSlider.value = newHeat;
    }

    //                      electrycity bar da ekle, bisiklet cevirdikce biriksin

    // gemi hizlaninca fov degissin
    // kamera cok hafif oyuncuyu takip etsin

    public bool IsTopShieldActive => _topShield.Power;
    public bool IsBottomShieldActive => _bottomShield.Power;
    public float MetalPanelBrokenPercent => _metalPanels.Sum(p=>!p.isFixed?1:0)/ _metalPanels.Count;
    public bool IsThrusterActive => _thruster.Power;

    private void Start()
    {
        Road = 0;
        Health = 1;
        Heat = 1;

        _metalPanels = FindObjectsOfType<MetalPanel>().ToList();
    }

    void Update()
    {
        if(isServer)
            MakeShipColder();
    }

    [Server]
    public void RunShip()
    {
        if (!GameManager.instance.IsGameRunning) return;
        Road += Time.deltaTime * (0.01f - MetalPanelBrokenPercent * 0.008f);
    }

    [Server]
    public void HeatShip()
    {
        if (!GameManager.instance.IsGameRunning) return;
        Heat += Time.deltaTime * 0.3f;
    }

    [Server]
    public void MakeShipColder()
    {
        if (!GameManager.instance.IsGameRunning) return;
        Heat -= Time.deltaTime * (0.02f +( MetalPanelBrokenPercent * 0.08f)+(0.02f* (IsThrusterActive ? 1:0)));
    }

    [Server]
    public void CollideWithAsteroid()
    {
        Health -= 0.2f;
    }
}
