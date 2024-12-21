using Holylib.Utilities;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCable : NetworkBehaviour, Collidable
{
    private const float _zapTime = 4;
    public void OnCollided(Player player)
    {
        player.Interaction.SetInterractLock(this, true);
        _rpcLockPlayer(player.connectionToClient);

        player.Movement.RpcZapPlayer();
        _rpcForceImpulse(player.connectionToClient);

        HolyTimer.CreateNewTimer(_zapTime, ()=> { _rpcStopZap(player.connectionToClient); player.Interaction.SetInterractLock(this, false); }).StartTimer();
    }

    [TargetRpc] private void _rpcForceImpulse(NetworkConnectionToClient target)
    {
        _forceImpulse(PlayerManager.instance.LocalePlayer);
    }

    private void _forceImpulse(Player player)
    {
        player.Movement.AddForceImpulse(Vector3.Project((player.transform.position - transform.position).normalized, transform.up).normalized * 50);
    }

    [TargetRpc] private void _rpcLockPlayer(NetworkConnectionToClient target)
    {
        PlayerManager.instance.LocalePlayer.Interaction.SetInterractLock(this, true);
        InputManager.Instance.SetInputLock(this, true);
    }

    private void _stopZappingPlayer(Player player)
    {
        player.Interaction.SetInterractLock(this, false);
        InputManager.Instance.SetInputLock(this, false);
        player.Movement.CMDStopPlayerAnimation();
    }

    [TargetRpc] private void _rpcStopZap(NetworkConnectionToClient target)
    {
        _stopZappingPlayer(PlayerManager.instance.LocalePlayer);
    }
}
