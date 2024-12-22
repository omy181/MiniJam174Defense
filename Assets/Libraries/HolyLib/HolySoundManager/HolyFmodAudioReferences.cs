using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyFmodAudioReferences : Singleton<HolyFmodAudioReferences>
{

    [field: Header("References")]
    [field: Header("UI")]
    [field: SerializeField] public EventReference UIClickSound { get; private set; }


    [field: Header("Music")]

    [field: SerializeField] public EventReference GameMusicStatus { get; private set; }
    [field: SerializeField] public EventReference InGameMusic { get; private set; }
}
