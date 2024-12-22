using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : Singleton<AsteroidManager>
{
    [SerializeField] private GameObject _asteroidOBJ;

    private GameObject _sendAsteroid(Vector3 pos, Vector3 des)
    {
        var obj = Instantiate(_asteroidOBJ,pos,Quaternion.identity);
        obj.GetComponent<Asteroid>().Initialize(pos,des);
        return obj;
    }

    public void SendAsteroidToTop()
    {       
        var asteroid = _sendAsteroid(new Vector3(0,0,-60), new Vector3(0, 0, -15));
        AttentionManager.instance.ShowAttention(asteroid, new Vector3(0, 2, -10));
    }

    public void SendAsteroidToBottom()
    {
        var asteroid = _sendAsteroid(new Vector3(0, 0, 60), new Vector3(0, 0, 15));
        AttentionManager.instance.ShowAttention(asteroid, new Vector3(0, 2, 10));
    }

}