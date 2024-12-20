using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Holylib.Utilities
{
    public class HolyTimer: MonoBehaviour
    {
        private float _durationSeconds;
        private Action _onFinished;
        private bool _destroyWhenOver;

        private float _currentTime;
        public bool IsPlaying { get; private set; }
        public bool IsOver { get; private set; }

        public static HolyTimer CreateNewTimer(float durationSeconds, Action onFinished,bool destroyWhenOver = true)
        {
            var obj = Instantiate(new GameObject("HolyTimer").AddComponent(typeof(HolyTimer)).GetComponent<HolyTimer>());
            obj._initialize(durationSeconds, onFinished, destroyWhenOver);
            return obj;
        }

        private void _initialize(float durationSeconds, Action onFinished, bool destroyWhenOver)
        {
            _durationSeconds = durationSeconds;
            _onFinished = onFinished;
            _currentTime = 0;
            _destroyWhenOver = destroyWhenOver;
        }

        public void StartTimer()
        {
            IsPlaying = true;
            IsOver = false;
        }

        public void CancelTimer()
        {
            _currentTime = 0;
            IsPlaying = false;
            IsOver = true;
        }

        public void StopTimer()
        {
            _currentTime = 0;
            IsPlaying = false;
            _onFinished();
            IsOver = true;
        }

        public void PauseTimer()
        {
            IsPlaying = false;
        }

        public void DestroyTimer()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            if (IsPlaying)
            {
                _currentTime += Time.deltaTime;

                if(_currentTime >= _durationSeconds)
                {
                    StopTimer();

                    if (_destroyWhenOver)
                    {
                        DestroyTimer();
                    }
                }
            }
        }
    }
}
