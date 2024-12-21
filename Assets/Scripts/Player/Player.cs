using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private PlayerInteraction _playerInteraction;
    public PlayerInteraction Interaction => _playerInteraction;

    [SerializeField] private PlayerMovement _playerMovement;
    public PlayerMovement Movement => _playerMovement;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        PlayerManager.instance.SetLocalePlayer(this);
    }

    public override void OnStopClient()
    {
        PlayerManager.instance.SetLocalePlayer(null);
    }
}
