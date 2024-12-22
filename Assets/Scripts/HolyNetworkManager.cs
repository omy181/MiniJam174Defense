using Holylib.UI;
using Mirror;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyNetworkManager : NetworkManager
{
    [SerializeField] private Transform _playerStartPos;

    public override void Awake()
    {
        base.Awake();

        foreach(Transform t in _playerStartPos)
            startPositions.Add(t);
    }
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        var steamId = SteamMatchmaking.GetLobbyMemberByIndex(SteamLobby.LobbyId, numPlayers - 1);

        
        var playerInfoDisplay = conn.identity.GetComponent<PlayerInfoDisplay>();

        playerInfoDisplay.ConnectionID = conn.connectionId;

        playerInfoDisplay.SetSteamId(steamId.m_SteamID);

    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);

        TextManager.instance.ShowText($"{conn.identity.GetComponent<PlayerInfoDisplay>().PlayerName} Disconnected", 4, Color.red);
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        TextManager.instance.ShowText("Disconnected from server", 4, Color.red);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        TextManager.instance.ShowText("Connected to the server", 2, Color.green);


    }

    public override void OnStartClient()
    {
        base.OnStartClient();

    }

    public override void OnStopClient()
    {
        base.OnStopClient();


        TextManager.instance.ShowText("Disconnected from server", 4, Color.red);
    }

}
