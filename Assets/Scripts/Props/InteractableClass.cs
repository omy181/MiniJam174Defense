using Mirror;
using UnityEngine;

public interface Interactable
{
    public void Interract(Player player);
    public void StopInterract(Player player);
    public bool IsInteractable(Player player);
}

public interface IngameEvent
{
    public float EventProbability();
    [ClientRpc] public void RpcActEvent();
}

public interface Collidable
{
    [ClientRpc] public void OnCollided(Player player);
}
