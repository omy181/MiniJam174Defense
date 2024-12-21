using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : NetworkBehaviour,Interactable
{

    public bool IsInteractable(Player player)
    {
        return !GameManager.instance.IsGameRunning;
    }

    public void Interract(Player player)
    {
        GameManager.instance.StartGame();
    }

    public void StopInterract(Player player)
    {
       
    }
}
