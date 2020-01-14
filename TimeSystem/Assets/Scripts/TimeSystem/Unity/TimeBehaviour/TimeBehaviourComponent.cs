using System;
using UnityEngine;

namespace TimeSystem.Unity
{
	[RequireComponent(typeof(TimeObjectComponent))]
	public abstract class TimeBehaviourComponent : MonoBehaviour
	{
		private TimeObjectComponent _timeObject;

		public void Initilize()
		{
			if (_timeObject == null)
			{
				_timeObject = gameObject.GetComponent<TimeObjectComponent>();
				_timeObject.OnTimeUpdateHandler += TimeUpdate;
				_timeObject.OnForwardHandler += OnForward;
				_timeObject.OnBackwardHandler += OnBackward;
				_timeObject.OnPauseHandler += OnPause;
			}
		}
		public void Deinitilize()
		{
			if (_timeObject != null)
			{
				_timeObject.OnTimeUpdateHandler -= TimeUpdate;
				_timeObject.OnForwardHandler -= OnForward;
				_timeObject.OnBackwardHandler -= OnBackward;
				_timeObject.OnPauseHandler -= OnPause;
				_timeObject = null;
			}
		}

		protected virtual void OnEnable()
		{
			Initilize();
		}
		protected virtual void OnDisable()
		{
			Deinitilize();
		}
		protected abstract void OnTimeUpdate(float deltaTime);
		protected abstract void OnForward();
		protected abstract void OnBackward();
		protected abstract void OnPause();
		protected void ReceiveInvertableEvent(Action action)
		{
			_timeObject?.ReceiveInvertableEvent(action);
		}

		private void TimeUpdate(float deltaTime)
		{
			OnTimeUpdate(deltaTime);
		}
	}
}