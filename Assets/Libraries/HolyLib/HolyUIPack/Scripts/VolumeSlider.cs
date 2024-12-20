using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holylib.HolySoundEffects;
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] MenuSliderSection slider;
    [SerializeField] string volumeName;
    void Start()
    {
        slider.OnValueSet += ChangeVolume;

        ChangeVolume(PlayerPrefs.GetFloat(slider.Save_Name));
    }

    public void ChangeVolume(float value)
    {
        switch (volumeName)
        {
            case "SFXVolume":
                HolyFmodAudioManager.instance.SFXVolume = value;
                break;
            case "MusicVolume":
                HolyFmodAudioManager.instance.MusicVolume = value;
                break;
            case "MasterVolume":
                HolyFmodAudioManager.instance.MasterVolume = value;
                break;
            default:
                Debug.LogError("Unknown volume name");
                break;    
        }
    }
}
