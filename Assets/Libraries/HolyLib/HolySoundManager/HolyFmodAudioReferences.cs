using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyFmodAudioReferences : Singleton<HolyFmodAudioReferences>
{

    [field: Header("References")]
    [field: Header("UI")]
    [field: SerializeField] public EventReference UIClickSound { get; private set; }

    [field: Header("Props")]
    [field: SerializeField] public EventReference MeteorWarning { get; private set; }
    [field: SerializeField] public EventReference FireWarning { get; private set; }
    [field: SerializeField] public EventReference HealthWarning { get; private set; }
    [field: SerializeField] public EventReference Thruster { get; private set; }
    [field: SerializeField] public EventReference ActivateDevice { get; private set; }
    [field: SerializeField] public EventReference ElectricZap { get; private set; }
    [field: SerializeField] public EventReference Repair { get; private set; }
    [field: SerializeField] public EventReference Crash { get; private set; }

    [field: Header("Music")]

    [field: SerializeField] public EventReference GameMusicStatus { get; private set; }
    [field: SerializeField] public EventReference InGameMusic { get; private set; }
}
