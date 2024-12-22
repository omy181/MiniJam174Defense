using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Window _mainMenu;
    private void Start()
    {
        WindowManager.instance.OpenWindow(_mainMenu);
    }
    public void PlaySinglePlayer()
    {
        PlayerManager.instance._networkManager.StartHost();
        WindowManager.instance.CloseWindows();
    }

    public void Host()
    {
        //PlayerManager.instance._networkManager.StartHost();
        SteamLobby.instance.HostLobby();
        WindowManager.instance.CloseWindows();
    }

    public void Join()
    {
        //PlayerManager.instance._networkManager.StartClient();
        
        WindowManager.instance.CloseWindows();
    }
}
