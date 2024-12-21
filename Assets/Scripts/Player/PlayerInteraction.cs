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

        if (!CanInterract) return;

        var colliders = Physics.OverlapBox(transform.position, Vector3.one * _capsuleCollider.height / 2);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Interactable interactable))
            {
                _seenInteractable = interactable;
                break;
            }
        }

        colliders = Physics.OverlapBox(transform.position, Vector3.one * _capsuleCollider.height / 4);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Collidable collidable))
            {
                collidable.OnCollided(_player);
            }
        }
    }

    private void _interract()
    {
        if (_seenInteractable != null && _seenInteractable.IsInteractable(_player)) _seenInteractable.Interract(_player);
    }

    private void _stopInterract()
    {
        if (_seenInteractable != null) _seenInteractable.StopInterract(_player);
    }
}
