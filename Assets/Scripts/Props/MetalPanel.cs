using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MetalPanel : MonoBehaviour,Interactable
{
    [SerializeField] private GameObject _fixedModel;
    [SerializeField] private GameObject _brokenModel;

    private bool _isFixed;
    public bool isFixed => _isFixed;

    private float _timer;
    private float _fixTime = 3;
    private bool _fixing;

    public void Interract(Player player)
    {
        InputManager.Instance.SetInputLock(player,true);
        _fixing = true;
        _timer = _fixTime;
    }

    public void StopInterract(Player player)
    {
        InputManager.Instance.SetInputLock(player, false);
        _fixing = false;
    }

    private void Update()
    {
        if (_fixing)
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;

                if(_timer < 0)
                {
                    _timer = 0;
                    _fixing = false;
                    _fix();
                }
            }
    }

    void Start()
    {
        _break();
    }


    private void _break()
    {
        _fixedModel.SetActive(false);
        _brokenModel.SetActive(true);
        _isFixed = false;
    }

    private void _fix()
    {
        _fixedModel.SetActive(true);
        _brokenModel.SetActive(false);
        _isFixed = true;
    }

    public bool IsInteractable(Player player)
    {
        return !isFixed;
    }
}
