using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _isgameRunning = true;
    public bool IsGameRunning => _isgameRunning;

    private List<IngameEvent> _ingameEvents = new();

    public void AddEvent(IngameEvent inGameEvent)
    {
        _ingameEvents.Add(inGameEvent);
    }

    private void Start()
    {
        StartCoroutine(_runEvents());
    }

    private IEnumerator _runEvents()
    {
        yield return new WaitForSeconds(1);

        float totalProbability = _ingameEvents.Sum(eventItem => eventItem.EventProbability());

        while (_isgameRunning)
        {
            float randomValue = Random.Range(0f, totalProbability);
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

            float randomInterval = Random.Range(10f, 15f);
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
