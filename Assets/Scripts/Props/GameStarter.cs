using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour, Interactable
{
    public void Interract(Player player)
    {
        GameManager.instance.StartGame();
    }

    public bool IsInteractable(Player player)
    {
        return !GameManager.instance.IsGameRunning;
    }

    public void StopInterract(Player player)
    {

    }
}
