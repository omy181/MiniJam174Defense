using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holylib.UI;
using System;

public class GlobalManager : MonoBehaviour
{

    public static GlobalManager instance;

    private void Awake()
    {
        instance = this;

    #if !UNITY_EDITOR
            // fixes new Unity2021 bug where the secondary monitor was being chosen when the game launches in a release build
            StartCoroutine("MoveToPrimaryDisplay");
    #endif
    }
    IEnumerable MoveToPrimaryDisplay()
    {
        List<DisplayInfo> displays = new List<DisplayInfo>();
        Screen.GetDisplayLayout(displays);
        if (displays?.Count > 0)
        {
            var moveOperation = Screen.MoveMainWindowTo(displays[0], new Vector2Int(displays[0].width / 2, displays[0].height / 2));
            yield return moveOperation;
        }
        yield return null;
    }


    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public Action OnSettingsChanged;
    public void CloseGame()
    {
        MessageBoxManager.Instance.ShowYesNoQuestion("Are you sure about quitting ?",()=>Application.Quit());
    }
}
