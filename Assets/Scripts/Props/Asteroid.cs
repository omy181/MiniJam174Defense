using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Vector3 _destiny;
    private Vector3 _start;
    private float _progress;
    private float _reachTime = 5;
    public void Initialize(Vector3 start,Vector3 des)
    {
        _start = start;
        _destiny = des;
        _progress = 0;
    }

    private void Update()
    {
        _progress += Time.deltaTime/_reachTime;
        transform.position = Vector3.Lerp(_start,_destiny, _progress);

        if(_progress > 1)
        {
            if (_isShieldActive())
            {
                _onCollidedWithShield();
            }
            else
            {
                _onCollidedWithShip();
            }
                
        }
    }

    private bool _isShieldActive()
    {
        if((_destiny- _start).z > 0)
        {
            return ShipManager.instance.isTopShieldActive;
        }
        else
        {
            return ShipManager.instance.isBottomShieldActive;
        }
    }

    private void _onCollidedWithShip()
    {
        ShipManager.instance.CollideWithAsteroid();
        // cam shake
        AttentionManager.instance.HideAttention(gameObject);
        Destroy(gameObject);
    }

    private void _onCollidedWithShield()
    {

        AttentionManager.instance.HideAttention(gameObject);
        Destroy(gameObject);
    }
}
