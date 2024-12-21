using Holylib.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCable : MonoBehaviour, Collidable
{
    private const float _zapTime = 4;
    public void OnCollided(Player player)
    {
        player.Interaction.SetInterractLock(this,true);
        InputManager.Instance.SetInputLock(this,true);

        player.Movement.CmdZapPlayer();
        player.Movement.AddForceImpulse(Vector3.Project((player.transform.position - transform.position).normalized, transform.up).normalized  *50);

        HolyTimer.CreateNewTimer(_zapTime, ()=> _stopZappingPlayer(player)).StartTimer();
    }

    private void _stopZappingPlayer(Player player)
    {
        player.Interaction.SetInterractLock(this, false);
        InputManager.Instance.SetInputLock(this, false);
        player.Movement.CMDStopPlayerAnimation();
    }
}
