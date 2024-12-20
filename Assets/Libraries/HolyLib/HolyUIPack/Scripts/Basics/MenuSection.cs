using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuSection : MonoBehaviour
{
    [SerializeField] public string Save_Name;
    [SerializeField] protected float DefaultValue;

    public Action<float> OnValueSet;

    
    protected virtual void Start()
    {
        InitializeValue();
    }

    void InitializeValue()
    {
        OnValueSet += (val) => GlobalManager.instance.OnSettingsChanged?.Invoke();

        if (!PlayerPrefs.HasKey(Save_Name))
        {
            PlayerPrefs.SetFloat(Save_Name, DefaultValue);
        }
    }

    public void SetValue(float value)
    {
        if (OnValueSet != null) OnValueSet(value);
        PlayerPrefs.SetFloat(Save_Name, value);
    }

    public void SetValue(bool value)
    {
        if (OnValueSet != null) OnValueSet(value ? 1:0);
        PlayerPrefs.SetFloat(Save_Name, value ? 1 : 0);
    }

    public void SetValue(int value)
    {
        if (OnValueSet != null) OnValueSet(value);
        PlayerPrefs.SetFloat(Save_Name, value);
    }
}
