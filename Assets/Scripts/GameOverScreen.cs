using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private GameObject _panel;
    void Start()
    {
        _restartButton.onClick.AddListener(LevelManager.instance.RestartLevel);
        _panel.gameObject.SetActive(false);
    }

    public void OnGameOver(bool isWin)
    {
        _panel.gameObject.SetActive(true);
        if (isWin)
        {
            _titleText.text = "You Did it !";
        }
        else
        {
            _titleText.text = "You Couldn't Reached Your Destination";
        }
    }

    public void HideScreen()
    {
        _panel.gameObject.SetActive(false);
    }
}
