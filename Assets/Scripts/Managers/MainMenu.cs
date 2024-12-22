using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Window _mainMenu;

    private EventInstance _inGameMusic;
    private EventInstance _gameMusicStatus;
    private void Start()
    {
        WindowManager.instance.OpenWindow(_mainMenu);

        _inGameMusic = HolyFmodAudioController.CreateEventInstance(HolyFmodAudioReferences.instance.InGameMusic);
        _inGameMusic.start();

        //_gameMusicStatus = HolyFmodAudioController.CreateEventInstance(HolyFmodAudioReferences.instance.GameMusicStatus);
    }

    public void SetMusicGameStarted()
    {
        _inGameMusic.setParameterByName("Game Started", 1);
    }

    public void SetMusicMainMenu()
    {
        _inGameMusic.setParameterByName("Game Started", 0);
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

        //WindowManager.instance.CloseWindows();

        TextManager.instance.ShowText("Accept your friends invite on steam",4,Color.cyan);
    }
}
