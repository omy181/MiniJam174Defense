using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    void Awake()
    {
        Instance= this;
    }

    private List<object> _cursorLockers = new();

    public void SetCursorFree(object locker, bool state)
    {
        if (state)
        {
            if (!_cursorLockers.Contains(locker))
                _cursorLockers.Add(locker);
        }
        else
        {
            if (_cursorLockers.Contains(locker))
                _cursorLockers.Remove(locker);
        }

        _updateCursorLock();
    }
    public bool IsCursorLocked { get { return _cursorLockers.Count == 0; } }

    private void _updateCursorLock()
    {
        if (IsCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private List<object> _inputLockers = new();

    public void SetInputLock(object locker, bool state)
    {
        if (state)
        {
            if (!_inputLockers.Contains(locker))
                _inputLockers.Add(locker);
        }
        else
        {
            if (_inputLockers.Contains(locker))
                _inputLockers.Remove(locker);
        }
    }
    public bool CanMove { get { return _inputLockers.Count == 0; } }


    void FixedUpdate()
    {
        MovementInputs();      
    }

    private void Update()
    {
        PressShift();
        UnPressShift();
        PressSpace();
        PressE();
        UnPressE();
        UnPressESC();
        PressX();
        UnPressX();
        PressR();
        UnPressR();
        PressLeftMouse();
        PressRightMouse();
        UnPressLeftMouse();
        UnPressRightMouse();
        ScrollForwatd();
        ScrollBackward();
        PressF();
        UnPressF();
        PressQ();
        UnPressQ();
        PressA();
        UnPressA();
        PressD();
        UnPressD();
    }

    public Action<float,float> OnMovement;
    void MovementInputs()
    {
        if (!CanMove) return;

        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        if(OnMovement!= null) OnMovement(hor,ver);
    }

    public Action OnPressShift;
    void PressShift()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(OnPressShift != null) OnPressShift();
        }
    }

    public Action OnUnPressShift;
    void UnPressShift()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (OnUnPressShift != null) OnUnPressShift();
        }
    }

    public Action OnPressSpace;
    void PressSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (OnPressSpace != null) OnPressSpace();
        }
    }

    public Action OnPressE;
    void PressE()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (OnPressE != null) OnPressE();
        }
    }

    public Action OnUnPressE;
    void UnPressE()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (OnUnPressE != null) OnUnPressE();
        }
    }

    public Action OnPressA;
    void PressA()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (OnPressA != null) OnPressA();
        }
    }

    public Action OnUnPressA;
    void UnPressA()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (OnUnPressA != null) OnUnPressA();
        }
    }

    public Action OnPressD;
    void PressD()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (OnPressD != null) OnPressD();
        }
    }

    public Action OnUnPressD;
    void UnPressD()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (OnUnPressD != null) OnUnPressD();
        }
    }

    public Action OnPressQ;
    void PressQ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (OnPressQ != null) OnPressQ();
        }
    }

    public Action OnUnPressQ;
    void UnPressQ()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (OnUnPressQ != null) OnUnPressQ();
        }
    }

    public Action OnUnPressESC;
    void UnPressESC()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OnUnPressESC?.Invoke();
        }
    }

    public Action OnPressX;
    void PressX()
    {
        if (Input.GetKeyDown(KeyCode.X)) OnPressX?.Invoke();       
    }

    public Action OnUnPressX;
    void UnPressX()
    {
        if (Input.GetKeyUp(KeyCode.X)) OnUnPressX?.Invoke();       
    }

    public Action OnPressR;
    void PressR()
    {
        if (Input.GetKeyDown(KeyCode.R)) OnPressR?.Invoke();
    }

    public Action OnUnPressR;
    void UnPressR()
    {
        if (Input.GetKeyUp(KeyCode.R)) OnUnPressR?.Invoke();
    }

    public Action OnPressF;
    void PressF()
    {
        if (Input.GetKeyDown(KeyCode.F)) OnPressF?.Invoke();
    }

    public Action OnUnPressF;
    void UnPressF()
    {
        if (Input.GetKeyUp(KeyCode.F)) OnUnPressF?.Invoke();
    }

    public Action OnPressLeftMouse;
    void PressLeftMouse()
    {
        if (Input.GetMouseButtonDown(0)) OnPressLeftMouse?.Invoke();
    }

    public Action OnUnPressLeftMouse;
    void UnPressLeftMouse()
    {
        if (Input.GetMouseButtonUp(0)) OnUnPressLeftMouse?.Invoke();
    }

    public Action OnPressRightMouse;
    void PressRightMouse()
    {
        if (Input.GetMouseButtonDown(1)) OnPressRightMouse?.Invoke();
    }

    public Action OnUnPressRightMouse;
    void UnPressRightMouse()
    {
        if (Input.GetMouseButtonUp(1)) OnUnPressRightMouse?.Invoke();
    }

    public Action OnScrollForward;

    void ScrollForwatd()
    {
        float val = Input.GetAxis("Mouse ScrollWheel");
        if (val > 0.08f)
        {
            OnScrollForward?.Invoke();
        }
    }

    public Action OnScrollBackward;

    void ScrollBackward()
    {
        float val = Input.GetAxis("Mouse ScrollWheel");
        if (val < -0.08f)
        {
            OnScrollBackward?.Invoke();
        }
    }
}
