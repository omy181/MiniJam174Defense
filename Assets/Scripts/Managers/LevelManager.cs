using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{

    public void RestartLevel()
    {
        SpesificLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void SpesificLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void NextLevel()
    {
        SpesificLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void PreviousLevel()
    {
        SpesificLevel(SceneManager.GetActiveScene().buildIndex -1);
    }
}
