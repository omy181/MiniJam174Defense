using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable _seenInteractable;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private Player _player;
    private void Start()
    {
        InputManager.Instance.OnPressF += _interract;
    }
    void Update()
    {
        _checkInteractable();
    }

    private void _checkInteractable()
    {
        var colliders = Physics.OverlapBox(transform.position, Vector3.one * _capsuleCollider.height/2);

        _seenInteractable = null;

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Interactable interactable))
            {
                _seenInteractable = interactable;
                break;
            }
        }
    }

    private void _interract()
    {
        if (_seenInteractable != null) _seenInteractable.Interract(_player);
    }
}
