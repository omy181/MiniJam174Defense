using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuToggleSection : MenuSection
{
    [SerializeField] Toggle toggle;

    protected override void Start()
    {
        base.Start();

        toggle.isOn = PlayerPrefs.GetFloat(Save_Name, DefaultValue) == 1;
    }
}
