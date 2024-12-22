using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : NetworkBehaviour
{
    private Interactable _seenInteractable;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private Player _player;

    private List<object> _interractLockers = new();

    public void SetInterractLock(object locker, bool state)
    {
        if (state)
        {
            if (!_interractLockers.Contains(locker))
                _interractLockers.Add(locker);
        }
        else
        {
            if (_interractLockers.Contains(locker))
                _interractLockers.Remove(locker);
        }
    }
    public bool CanInterract { get { return _interractLockers.Count == 0; } }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!isLocalPlayer) return;
        InputManager.Instance.OnPressF += _interract;
        InputManager.Instance.OnUnPressF += _stopInterract;
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        if (!isLocalPlayer) return;
        InputManager.Instance.OnPressF -= _interract;
        InputManager.Instance.OnUnPressF -= _stopInterract;
    }
    void Update()
    {
        _checkInteractable();
    }

    private void _checkInteractable()
    {

        _seenInteractable = null;

        if (!CanInterract) { InteractionUIManager.instance.DisableInteraction(); return; }

        var colliders = Physics.OverlapBox(transform.position, Vector3.one * _capsuleCollider.height / 4);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Interactable interactable) && interactable.IsInteractable(_player))
            {
                if(isLocalPlayer) InteractionUIManager.instance.EnableInteraction(collider.transform.position + new Vector3(0,2,0));
                _seenInteractable = interactable;
                break;
            }
        }

        if(_seenInteractable == null && isLocalPlayer) InteractionUIManager.instance.DisableInteraction();

        colliders = Physics.OverlapBox(transform.position, Vector3.one * _capsuleCollider.height / 4);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Collidable collidable))
            {
                _onCollided(collidable);
            }
        }
    }

    [Server] private void _onCollided(Collidable collidable)
    {
        if(isServer)
        collidable.OnCollided(_player);
    }


    private void _interract()
    {
        if (_seenInteractable != null && _seenInteractable.IsInteractable(_player)) _cmdInterract(_player);       
    }

    [Command(requiresAuthority = false)] private void _cmdInterract(Player player)
    {
        if (_seenInteractable != null && _seenInteractable.IsInteractable(_player)) _seenInteractable.Interract(player);
    }

    private void _stopInterract()
    {
        _cmdStopInterract(_player); 
    }

    [Command(requiresAuthority = false)] private void _cmdStopInterract(Player player)
    {
        if (_seenInteractable != null) { 
            _seenInteractable.StopInterract(player); 
        }

        _rpcStopInterract(player);
        player.Movement.RpcStopPlayerAnimation();
    }

    [ClientRpc] private void _rpcStopInterract(Player player)
    {
        InputManager.Instance.SetInputLock(player, false);
    }
}
