using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour,IngameEvent
{
    [SerializeField] private GameObject _asteroidOBJ;

    private GameObject _sendAsteroid(Vector3 pos, Vector3 des)
    {
        var obj = Instantiate(_asteroidOBJ,pos,Quaternion.identity);
        obj.GetComponent<Asteroid>().Initialize(pos,des);
        return obj;
    }

    private void _sendAsteroidToTop()
    {       
        var asteroid = _sendAsteroid(new Vector3(0,0,-60), new Vector3(0, 0, -15));
        AttentionManager.instance.ShowAttention(asteroid, new Vector3(0, 0, -15));
    }

    private void _sendAsteroidToBottom()
    {
        var asteroid = _sendAsteroid(new Vector3(0, 0, 60), new Vector3(0, 0, 15));
        AttentionManager.instance.ShowAttention(asteroid, new Vector3(0, 0, 15));
    }

    private void Start()
    {
        GameManager.instance.AddEvent(this);
    }

    public float EventProbability()
    {
        return 0.3f;
    }

    public void ActEvent()
    {
        if (Random.Range(0f, 1f) < 0.5f)
        {
            _sendAsteroidToTop();
        }
        else
        {
            _sendAsteroidToBottom();
        }
        
        
    }
}
