using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Window _pauseMenu;
    public void RestartLevel()
    {
        GameManager.instance.CmdResetGame();
        //SpesificLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        InputManager.Instance.OnUnPressESC += _openPauseMenu;
    }

    private void _openPauseMenu()
    {
        WindowManager.instance.OpenWindow(_pauseMenu);
    }

    public void QuitToMenu()
    {
        PlayerManager.instance._networkManager.StopClient();
        PlayerManager.instance._networkManager.StopHost();

        GameManager.instance.StopGameForMe();

        WindowManager.instance.OpenWindow(WindowManager.instance._mainMenu);
    }

    public void SpesificLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void NextLevel()
    {
        SpesificLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void PreviousLevel()
    {
        SpesificLevel(SceneManager.GetActiveScene().buildIndex -1);
    }
}
