using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public NetworkManager _networkManager;

    public Player LocalePlayer { get; set; }
    public int PlayerCount => _networkManager.numPlayers;

    public void SetLocalePlayer(Player player)
    {
        LocalePlayer = player;
    }
}
