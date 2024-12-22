using Mirror;
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
    }

}
