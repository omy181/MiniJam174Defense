using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    private void Start()
    {
        InputManager.Instance.OnMovement += _move;
    }
    private void _move(float hor,float ver)
    {
        _rb.AddForce(new Vector3(hor,0,ver).normalized* -_speed);
    }
}
