using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _isgameRunning = false;
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

    public void StartGame()
    {
        _isgameRunning = true;
        StartCoroutine(_runEvents());
    }

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

            selectedEvent.ActEvent();

            float randomInterval = UnityEngine.Random.Range(4f, 10f);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    public void GameOver(bool isWin)
    {
        if (!IsGameRunning) return;

        _isgameRunning = false;
        OnGameOver(isWin);
    }

    public Action<bool> OnGameOver;
}
