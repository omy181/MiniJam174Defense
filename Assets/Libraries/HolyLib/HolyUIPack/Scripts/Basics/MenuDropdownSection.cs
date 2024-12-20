using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuDropdownSection : MenuSection
{
    [SerializeField] protected TMP_Dropdown dropdown;

    protected override void Start()
    {
        base.Start();

        dropdown.value = (int)PlayerPrefs.GetFloat(Save_Name, DefaultValue);
    }
}
