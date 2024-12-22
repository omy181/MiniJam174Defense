using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidTopEvent : NetworkBehaviour, IngameEvent
{
    void Start()
    {
        GameManager.instance.AddEvent(this);
    }
    public float EventProbability()
    {
        return 0.3f;
    }

    [ClientRpc]
    public void RpcActEvent()
    {
        AsteroidManager.instance.SendAsteroidToTop();
    }
}
