using FMOD.Studio;
using System;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;

    [SerializeField] CanvasGroup PauseMenuCanvas;

    private EventInstance _inGameMusic;
    private EventInstance _gameMusicStatus;
    void Start()
    {
        

        InputManager.Instance.OnUnPressESC += UnPauseGame;
        UnPauseGame();

        _inGameMusic = HolyFmodAudioController.CreateEventInstance(HolyFmodAudioReferences.instance.InGameMusic);
        _inGameMusic.start();

        _gameMusicStatus = HolyFmodAudioController.CreateEventInstance(HolyFmodAudioReferences.instance.GameMusicStatus);
    }

    public Action OnPaused;
    public void PauseGame()
    {
        InputManager.Instance.OnUnPressESC -= PauseGame;
        InputManager.Instance.OnUnPressESC += UnPauseGame;
        isPaused = true;

        OnPaused?.Invoke();

        Time.timeScale = 0f;
        PauseMenuCanvas.alpha = 1f;
        PauseMenuCanvas.interactable = true;
        PauseMenuCanvas.gameObject.SetActive(true);
        InputManager.Instance.SetCursorFree(this, true);

        _gameMusicStatus.start();
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameStatus", 0);
    }
    public Action OnUnpaused;
    public void UnPauseGame()
    {
        InputManager.Instance.OnUnPressESC += PauseGame;
        InputManager.Instance.OnUnPressESC -= UnPauseGame;
        isPaused = false;

        OnUnpaused?.Invoke();

        Time.timeScale = 1f;
        PauseMenuCanvas.alpha = 0f;
        PauseMenuCanvas.interactable = false;
        PauseMenuCanvas.gameObject.SetActive(false);
        InputManager.Instance.SetCursorFree(this,false);

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("GameStatus", 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }
}
