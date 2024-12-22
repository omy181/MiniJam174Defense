using UnityEngine;
public class WindowManager : Singleton<WindowManager>
{
    private Window _previousWindow;
    private Window _currentWindow;

    public Window _mainMenu;

    public bool PreviousWindow()
    {
        return _previousWindow != null;
    }

    public void OpenWindow(Window window)
    {
        TryOpenWindow(window);
    }

    public bool TryOpenWindow(Window window)
    {
        if (_currentWindow == window) return false;

        _previousWindow = _currentWindow;
        _currentWindow = window;

        _previousWindow?.Deactivate();
        _currentWindow?.Activate();

        return true;
    }

    public void OpenPreviousWindow()
    {
        OpenWindow(_previousWindow);
    }

    public void CloseWindows()
    {
        _previousWindow?.Deactivate();
        _currentWindow?.Deactivate();

        _currentWindow = null;
        _previousWindow = null;
    }
}
