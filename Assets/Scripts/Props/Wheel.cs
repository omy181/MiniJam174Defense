using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wheel : MonoBehaviour, Interactable
{
    private int _playerCount => _players.Count;
    private List<Player> _players = new();

    [SerializeField] private float _speed = 5;
    private Dictionary<Player, int> _playerDir = new();
    private int _dir => _playerDir.Sum(p=>p.Value);

    private void Update()
    {
        if(_playerCount > 0)
        {
            _rotate();
        }

        if (_dir == 0)
        {
            _snap();
        }
    }

    private void _rotate()
    {
        if (_dir == 0) return;

        var forcePower = Mathf.Sign(_dir) * _speed * _playerCount / PlayerManager.instance.PlayerCount;

        transform.Rotate(Vector3.forward, forcePower);
    }

    private void _snap()
    {
        float currentZRotation = transform.rotation.eulerAngles.y;

        float nearestMultipleOf45 = Mathf.Round(currentZRotation / 30f) * 30f;
        if (nearestMultipleOf45 < 0)
        {
            nearestMultipleOf45 += 360f;
        }

        if (Mathf.Abs(currentZRotation - nearestMultipleOf45) <= 5f)
        {
            Vector3 newRotation = new Vector3(transform.rotation.eulerAngles.x, nearestMultipleOf45, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Euler(newRotation);
        }
    }

    public void Interract(Player player)
    {
        if (_players.Contains(player)) {
            _players.Remove(player);
            _playerDir.Remove(player);

            player.transform.SetParent(null);
            InputManager.Instance.SetInputLock(player, false);

            InputManager.Instance.OnPressQ -= _changeDirMinus;
            InputManager.Instance.OnUnPressQ -= _changeDirPlus;

            InputManager.Instance.OnPressE -= _changeDirPlus;
            InputManager.Instance.OnUnPressE -= _changeDirMinus;
        }
        else
        {
            _players.Add(player);
            _playerDir.Add(player,0);

            player.transform.SetParent(transform);
            InputManager.Instance.SetInputLock(player,true);

            InputManager.Instance.OnPressQ += _changeDirMinus;
            InputManager.Instance.OnUnPressQ += _changeDirPlus;

            InputManager.Instance.OnPressE += _changeDirPlus;
            InputManager.Instance.OnUnPressE += _changeDirMinus;
        }
        
    }

    private void _changeDirMinus()
    {
        _playerDir[PlayerManager.instance.LocalePlayer] -= 1;
    }

    private void _changeDirPlus()
    {
        _playerDir[PlayerManager.instance.LocalePlayer] += 1;
    }

}
