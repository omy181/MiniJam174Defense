using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!isLocalPlayer) return;
        InputManager.Instance.OnMovement += _move;
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        if (!isLocalPlayer) return;
        InputManager.Instance.OnMovement -= _move;
    }
    private void _move(float hor,float ver)
    {
        _rb.AddForce(new Vector3(hor,0,ver).normalized* -_speed);
    }

    public void AddForceImpulse(Vector3 force)
    {
        _rb.AddForce(force,ForceMode.Impulse);
    }

    [SerializeField] private Animation _animation;
    [SerializeField] private AnimationClip _zapAnimation;
    [SerializeField] private AnimationClip _repairAnimation;
    [Command] public void CmdZapPlayer()
    {
        RpcZapPlayer();
    }

    [ClientRpc] public void RpcZapPlayer()
    {
        _animation.clip = _zapAnimation;
        _animation.Play();

    }

    [ClientRpc] public void RpcRepairPlayer()
    {
        _animation.clip = _repairAnimation;
        _animation.Play();
    }

    [Command] public void CMDStopPlayerAnimation()
    {
        RpcStopPlayerAnimation();
    }

    [ClientRpc] public void RpcStopPlayerAnimation()
    {
        _animation.Stop();
    }
}
