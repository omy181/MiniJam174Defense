using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _playerInteraction;
    public PlayerInteraction Interaction => _playerInteraction;

    [SerializeField] private PlayerMovement _playerMovement;
    public PlayerMovement Movement => _playerMovement;
}
