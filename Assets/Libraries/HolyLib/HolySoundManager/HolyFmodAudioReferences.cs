using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyFmodAudioReferences : MonoBehaviour
{
    public static HolyFmodAudioReferences instance;

    private void Awake()
    {
        instance = this;
    }

    [field: Header("References")]
    [field: Header("UI")]
    [field: SerializeField] public EventReference UIClickSound { get; private set; }
    [field: SerializeField] public EventReference UIClick2Sound { get; private set; }

    [field: Header("Player")]
    [field: SerializeField] public EventReference ItemGetSound { get; private set; }
    [field: SerializeField] public EventReference ItemDropSound { get; private set; }
    [field: SerializeField] public EventReference WalkSound { get; private set; }
    [field: SerializeField] public EventReference JumpSound { get; private set; }
    [field: SerializeField] public EventReference LandSound { get; private set; }

    [field: Header("Props")]
    [field: SerializeField] public EventReference LeverSound { get; private set; }
    [field: SerializeField] public EventReference ItemBought { get; private set; }
    [field: SerializeField] public EventReference ProccessFinished { get; private set; }
    [field: SerializeField] public EventReference ShopDoor { get; private set; }
    [field: SerializeField] public EventReference ResearchDone { get; private set; }
    [field: SerializeField] public EventReference PageFlip { get; private set; }

    [field: Header("Music")]

    [field: SerializeField] public EventReference GameMusicStatus { get; private set; }
    [field: SerializeField] public EventReference InGameMusic { get; private set; }
}
