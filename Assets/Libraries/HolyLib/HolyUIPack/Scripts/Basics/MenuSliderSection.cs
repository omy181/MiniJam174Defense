using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSliderSection : MenuSection
{
    [SerializeField] Slider slider;

    protected override void Start()
    {
        base.Start();

        slider.value = PlayerPrefs.GetFloat(Save_Name, DefaultValue);
    }
}
