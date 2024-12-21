using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wheel : NetworkBehaviour, Interactable
{
    private int _playerCount => _playerDir.Count;

    [SerializeField] private float _speed = 5;
    private Dictionary<Player, int> _playerDir = new();
    private int _dir => _playerDir.Sum(p=>p.Value);
    private Player _holdingPlayer;

    private void Update()
    {
        _updateState();
    }

    [Server] private void _updateState()
    {
        if (_playerCount > 0)
        {
            _rotate();
        }

        if (_dir == 0)
        {
            var angle = _snap();
            DeviceManager.instance.PowerOnDeviceByAngle((int)angle);
        }
        else
        {
            DeviceManager.instance.PowerOnDeviceByAngle(-1);
        }
    }

    [Server] private void _rotate()
    {
        if (_dir == 0) return;

        var forcePower = Mathf.Sign(_dir) * _speed * _playerCount * Time.deltaTime / PlayerManager.instance.PlayerCount;

        transform.Rotate(Vector3.forward, forcePower);

        _rpcSetRotation(transform.rotation);
    }

    [ClientRpc]private void _rpcSetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    [Server] private float _snap()
    {
        float currentZRotation = transform.rotation.eulerAngles.y;

        float nearestMultiple = Mathf.Round(currentZRotation / 30f) * 30f;
        if (nearestMultiple < 0)
        {
            nearestMultiple += 360f;
        }

        if (Mathf.Abs(currentZRotation - nearestMultiple) <= 5f)
        {
            Vector3 newRotation = new Vector3(transform.rotation.eulerAngles.x, nearestMultiple, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Euler(newRotation);

            _rpcSetRotation(transform.rotation);

            return nearestMultiple;
        }

        return -1;
    }


    public void Interract(Player player)
    {
        if (_holdingPlayer) {
            _cmdOnPlayerStoppedHolding(player);
            _holdingPlayer = null;
            player.transform.SetParent(null);
            InputManager.Instance.SetInputLock(player, false);

            InputManager.Instance.OnPressA -= _changeDirMinus;
            InputManager.Instance.OnUnPressA -= _changeDirPlus;

            InputManager.Instance.OnPressD -= _changeDirPlus;
            InputManager.Instance.OnUnPressD -= _changeDirMinus;
        }
        else
        {
            _cmdOnPlayerStartedHolding(player);
            _holdingPlayer = player;
            player.transform.SetParent(transform);
            InputManager.Instance.SetInputLock(player,true);

            InputManager.Instance.OnPressA += _changeDirMinus;
            InputManager.Instance.OnUnPressA += _changeDirPlus;

            InputManager.Instance.OnPressD += _changeDirPlus;
            InputManager.Instance.OnUnPressD += _changeDirMinus;
        }
        
    }

    [Command(requiresAuthority = false)]
    private void _cmdOnPlayerStartedHolding(Player player)
    {
        _playerDir[player] = 0;
    }

    [Command(requiresAuthority = false)]
    private void _cmdOnPlayerStoppedHolding(Player player)
    {
        _playerDir.Remove(player);
    }

    private void _changeDirMinus()
    {
        _cmdChangeDirMinus(_holdingPlayer);        
    }

    private void _changeDirPlus()
    {
        _cmdChangeDirPlus(_holdingPlayer);
    }

    [Command(requiresAuthority =false)] private void _cmdChangeDirMinus(Player player)
    {
        _playerDir[player] -= 1;
    }

    [Command(requiresAuthority = false)] private void _cmdChangeDirPlus(Player player)
    {
        _playerDir[player] += 1;
    }

    public void StopInterract(Player player)
    {
        
    }

    public bool IsInteractable(Player player)
    {
        return true;
    }
}
