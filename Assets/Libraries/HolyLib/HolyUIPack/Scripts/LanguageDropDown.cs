using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Holylib.Localization;

public class LanguageDropDown : MenuDropdownSection
{
    protected override void Start()
    {

        var langs = (Enum.GetNames(typeof(Languages))).ToList();

        dropdown.AddOptions(langs);

        dropdown.value = (int)(Languages)Enum.Parse(typeof(Languages), PlayerPrefs.GetString("Language"));

        dropdown.onValueChanged.AddListener((int l)=> TranslateManager.instance.ChangeLanguage((Languages)l));
    }

}
