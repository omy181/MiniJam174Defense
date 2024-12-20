using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPanel : MonoBehaviour,Interactable,IngameEvent
{
    [SerializeField] private GameObject _fixedModel;
    [SerializeField] private GameObject _brokenModel;

    private bool _isFixed;
    public bool isFixed => _isFixed;

    private float _timer;
    private float _fixTime = 1.5f;
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
        _fix();
        GameManager.instance.AddEvent(this);
    }


    private void _break()
    {
        if (!isFixed) return;

        _fixedModel.SetActive(false);
        _brokenModel.SetActive(true);
        _isFixed = false;

        AttentionManager.instance.ShowAttention(this,transform.position);
    }

    private void _fix()
    {
        if (isFixed) return;

        _fixedModel.SetActive(true);
        _brokenModel.SetActive(false);
        _isFixed = true;

        AttentionManager.instance.HideAttention(this);
    }

    public bool IsInteractable(Player player)
    {
        return !isFixed;
    }

    private IEnumerator _breakRandomly()
    {
        while (GameManager.instance.IsGameRunning)
        {
            if (isFixed)
            {
                yield return new WaitForSeconds(Random.Range(5, 30));
                _break();
            }
            else
            {
                yield return new WaitForSeconds(5);
            }
        }
    }

    public float EventProbability()
    {
        return 0.5f;
    }

    public void ActEvent()
    {
        _break();
    }
}
