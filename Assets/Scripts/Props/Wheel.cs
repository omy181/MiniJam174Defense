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
            var angle = _snap();
            DeviceManager.instance.PowerOnDeviceByAngle((int)angle);
        }
        else
        {
            DeviceManager.instance.PowerOnDeviceByAngle(-1);
        }
    }

    private void _rotate()
    {
        if (_dir == 0) return;

        var forcePower = Mathf.Sign(_dir) * _speed * _playerCount * Time.deltaTime / PlayerManager.instance.PlayerCount;

        transform.Rotate(Vector3.forward, forcePower);
    }

    private float _snap()
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

            return nearestMultiple;
        }

        return -1;
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
