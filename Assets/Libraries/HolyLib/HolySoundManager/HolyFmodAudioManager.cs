using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Holylib.HolySoundEffects;

namespace Holylib.HolySoundEffects
{
    public class HolyFmodAudioManager : Singleton<HolyFmodAudioManager>
    {

        public List<EventInstance> EventInstances = new();

        protected override void Awake()
        {
            base.Awake();

            _masterBus = RuntimeManager.GetBus("bus:/");
            _musicBus = RuntimeManager.GetBus("bus:/Music");
            _sfxBus = RuntimeManager.GetBus("bus:/SFX");
        }

        // Master
        private Bus _masterBus;
        private float _masterVolume;
        public float MasterVolume { get => _masterVolume; set { _masterBus.setVolume(value); _masterVolume = value; } }

        // Music
        private Bus _musicBus;
        private float _musicVolume;
        public float MusicVolume { get => _musicVolume; set { _musicBus.setVolume(value); _musicVolume = value; } }

        // SFX
        private Bus _sfxBus;
        private float _sfxVolume;
        public float SFXVolume { get => _sfxVolume; set { _sfxBus.setVolume(value); _sfxVolume = value; } }

        private void CleanUp()
        {
            foreach (var eventInstance in EventInstances)
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            CleanUp();
        }
    }
}

public static class HolyFmodAudioController
{
    public static void PlayOneShot(EventReference eventReference,Vector3 pos)
    {
        RuntimeManager.PlayOneShot(eventReference,pos);
    }

    public static EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        HolyFmodAudioManager.instance.EventInstances.Add(eventInstance);
        return eventInstance;
    }
}
