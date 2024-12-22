using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : Device
{
    [SerializeField] private GameObject _tutorial;
    protected override void _onPowerOn() {
        
        if(isClient)
        startGame();
    }

    private void startGame()
    {
            GameManager.instance.StartGame();
    }

    protected override void _run()
    {
       
    }

    private void Update()
    {
        _tutorial.gameObject.SetActive(!GameManager.instance.IsGameRunning && !Power);
    }
}
