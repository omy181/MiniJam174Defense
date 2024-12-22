using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : NetworkSingleton<GameManager>
{
    [SerializeField] private GameOverScreen _gameOverScreen;

    [SyncVar] private bool _isgameRunning = false;
    public bool IsGameRunning => _isgameRunning;

    private List<IngameEvent> _ingameEvents = new();

    public void AddEvent(IngameEvent inGameEvent)
    {
        _ingameEvents.Add(inGameEvent);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    [Command(requiresAuthority =false)] public void CmdResetGame()
    {
        ShipManager.instance.ResetStats();
        _rpcReset();
    }

    [ClientRpc] private void _rpcReset()
    {
        _gameOverScreen.HideScreen();
    }

    public void StopGameForMe()
    {
        _isgameRunning = false;
        if(_eventroutine != null) StopCoroutine(_eventroutine);
        ShipManager.instance.ResetStatsForMe();
        _gameOverScreen.HideScreen();
    }

    public void StartGame()
    {
        if (_isgameRunning) return;

        _cmdStartGame();
    }

    [Command(requiresAuthority = false)]
    private void _cmdStartGame()
    {
        _isgameRunning = true;

        _eventroutine=StartCoroutine(_runEvents());
    }

    private Coroutine _eventroutine;
    private IEnumerator _runEvents()
    {
        yield return new WaitForSeconds(1);

        float totalProbability = _ingameEvents.Sum(eventItem => eventItem.EventProbability());

        while (_isgameRunning)
        {
            float randomValue = UnityEngine.Random.Range(0f, totalProbability);
            float cumulativeProbability = 0f;
            IngameEvent selectedEvent = null;

            foreach (var ingameEvent in _ingameEvents)
            {
                cumulativeProbability += ingameEvent.EventProbability();
                if (randomValue <= cumulativeProbability)
                {
                    selectedEvent = ingameEvent;
                    break;
                }
            }

            selectedEvent.RpcActEvent();

            float randomInterval = UnityEngine.Random.Range(4f, 10f);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    [Server]
    public void GameOver(bool isWin)
    {
        if (!IsGameRunning) return;

        RpcGameOver(isWin);       
    }

    [ClientRpc]
    private void RpcGameOver(bool isWin) {
        _isgameRunning = false;
        _gameOverScreen.OnGameOver(isWin);
    }

}
