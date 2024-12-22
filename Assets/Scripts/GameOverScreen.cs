using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _restartButton2;
    [SerializeField] private GameObject _gameover;
    [SerializeField] private GameObject _win;
    void Start()
    {
        _restartButton.onClick.AddListener(LevelManager.instance.RestartLevel);
        _restartButton2.onClick.AddListener(LevelManager.instance.RestartLevel);
        _gameover.gameObject.SetActive(false);
        _win.gameObject.SetActive(false);
    }

    public void OnGameOver(bool isWin)
    {
        _gameover.gameObject.SetActive(!isWin);
        _win.gameObject.SetActive(isWin);
    }

    public void HideScreen()
    {
        _gameover.gameObject.SetActive(false);
        _win.gameObject.SetActive(false);
    }
}
