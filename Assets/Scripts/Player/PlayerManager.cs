using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private Player _localePlayer; // for testing
    public Player LocalePlayer { get; private set; }
    public int PlayerCount => 1;

    protected override void Awake()
    {
        base.Awake();
        LocalePlayer = _localePlayer;
    }
}
