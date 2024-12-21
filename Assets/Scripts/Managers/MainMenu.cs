using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Window _mainMenu;
    private void Start()
    {
        WindowManager.instance.OpenWindow(_mainMenu);
    }
    public void PlaySinglePlayer()
    {
        Host();
    }

    public void Host()
    {
        WindowManager.instance.CloseWindows();
    }

    public void Join()
    {

    }
}
