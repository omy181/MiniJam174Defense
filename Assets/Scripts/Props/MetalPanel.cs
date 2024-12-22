using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPanel : NetworkBehaviour,Interactable,IngameEvent
{
    [SerializeField] private GameObject _fixedModel;
    [SerializeField] private GameObject _brokenModel;

    private bool _isFixed;
    public bool isFixed => _isFixed;

    private float _timer;
    private float _fixTime = 3f;
    private bool _fixing;

    public void Interract(Player player)
    {
        //InputManager.Instance.SetInputLock(player,true);
        _lockPlayer(player.connectionToClient,player,true);
        _fixing = true;
        _timer = _fixTime;

        player.Movement.RpcRepairPlayer();
    }

    [TargetRpc] private void _lockPlayer(NetworkConnectionToClient target, Player player,bool state)
    {
        InputManager.Instance.SetInputLock(player, state);
    }

    public void StopInterract(Player player)
    {
        //InputManager.Instance.SetInputLock(player, false);
        _lockPlayer(player.connectionToClient, player, false);
        _fixing = false;

        player.Movement.RpcStopPlayerAnimation();
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
                    _cmdFix();
                }
            }
    }

    void Start()
    {
        _fix();
        if (!isServer) return;
        GameManager.instance.AddEvent(this);
    }


    [Command(requiresAuthority =false)] private void _cmdBreak()
    {
        if (!isFixed) return;

        _rpcBreak();
    }

    [ClientRpc] private void _rpcBreak()
    {
        _fixedModel.SetActive(false);
        _brokenModel.SetActive(true);
        _isFixed = false;

        AttentionManager.instance.ShowAttention(this, transform.position,true);
    }

    [Command(requiresAuthority =false)] private void _cmdFix()
    {
        if (isFixed) return;

        _rpcFix();
    }

    [ClientRpc] private void _rpcFix()
    {
        _fix();
    }

    private void _fix()
    {
        _fixedModel.SetActive(true);
        _brokenModel.SetActive(false);
        _isFixed = true;
        _fixing = false;

        AttentionManager.instance.HideAttention(this);
    }

    [ClientRpc] public void RpcReset()
    {
        _fix();
    }


    public void ResetForMe()
    {
        _fix();
    }

    public bool IsInteractable(Player player)
    {
        return !isFixed;
    }

    public float EventProbability()
    {
        return 0.5f;
    }

    [ClientRpc]
    public void RpcActEvent()
    {
        _cmdBreak();
    }
}
