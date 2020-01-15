using System;
using TimeSystem.Core;
using UnityEngine;
using UnityEngine.Events;

namespace TimeSystem.Unity
{
    [Serializable]
    public class FloatUnityEvent : UnityEvent<float> { }

    public class TimeObjectEventHandler : TimeBehaviourComponent
    {
        [SerializeField] private FloatUnityEvent _onTimeUpdateHandler;
        [SerializeField] private UnityEvent _onForwardHandler;
        [SerializeField] private UnityEvent _onBackwardHandler;
        [SerializeField] private UnityEvent _onPauseHandler;

        protected override void OnTimeUpdate(float deltaTime)
        {
            _onTimeUpdateHandler?.Invoke(deltaTime);
        }
        protected override void OnForward()
        {
            _onForwardHandler?.Invoke();
        }
        protected override void OnBackward()
        {
            _onBackwardHandler?.Invoke();
        }
        protected override void OnPause()
        {
            _onPauseHandler?.Invoke();
        }
    }
}