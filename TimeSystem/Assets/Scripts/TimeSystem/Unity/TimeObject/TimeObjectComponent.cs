using System;
using TimeSystem.Core;
using UnityEngine;

namespace TimeSystem.Unity
{
	[DisallowMultipleComponent]
	public class TimeObjectComponent : MonoBehaviour
	{
		public event Action<float> OnTimeUpdateHandler;
		public event Action OnForwardHandler
		{
			add
			{
				if (_timeObject != null)
				{
					_timeObject.OnForwardHandler += value;
				}
				else
				{
					_onForwardHandlerCache += value;
				}
			}
			remove
			{
				if (_timeObject != null)
				{
					_timeObject.OnForwardHandler -= value;
				}
			}
		}
		public event Action OnBackwardHandler
		{
			add
			{
				if (_timeObject != null)
				{
					_timeObject.OnBackwardHandler += value;
				}
				else
				{
					_onBackwardHandlerCache += value;
				}
			}
			remove
			{
				if (_timeObject != null)
				{
					_timeObject.OnBackwardHandler -= value;
				}
			}
		}
		public event Action OnPauseHandler
		{
			add
			{
				if (_timeObject != null)
				{
					_timeObject.OnPauseHandler += value;
				}
				else
				{
					_onPauseHandlerCache += value;
				}
			}
			remove
			{
				if (_timeObject != null)
				{
					_timeObject.OnPauseHandler -= value;
				}
			}
		}

		public float TimeScale
		{
			set
			{
				_timeScale = value;
				if (_timeObject != null)
				{
					_timeObject.TimeScale = _timeScale;
				}
			}
		}

		[SerializeField] private string _timeGroup;
		[SerializeField] private float _timeScale;

		private TimeObject _timeObject = null;
		private Action _onForwardHandlerCache;
		private Action _onBackwardHandlerCache;
		private Action _onPauseHandlerCache;

		public void ReceiveInvertableEvent(Action action)
		{
			_timeObject?.OnInvertableEvent(action);
		}

		protected virtual void OnTimeUpdate(float deltaTime)
		{
		}

		private void Awake()
		{
			_timeObject = new TimeObject();
			_timeObject.OnTimeUpdateHandler += TimeUpdate;
			_timeObject.OnForwardHandler += _onForwardHandlerCache;
			_timeObject.OnBackwardHandler += _onBackwardHandlerCache;
			_timeObject.OnPauseHandler += _onPauseHandlerCache;

			_onForwardHandlerCache = delegate { };
			_onBackwardHandlerCache = delegate { };
			_onPauseHandlerCache = delegate { };

			TimeScale = _timeScale;
		}
		private void OnEnable()
		{
			TimeController.Register(_timeGroup, _timeObject);
		}
		private void OnDisable()
		{
			TimeController.Unregister(_timeGroup, _timeObject);
		}
		private void OnValidate()
		{
			TimeScale = _timeScale;
		}
		private void OnDestroy()
		{
			if (_timeObject != null)
			{
				_timeObject.OnTimeUpdateHandler -= TimeUpdate;
				_timeObject = null;
			}

			_onForwardHandlerCache = delegate { };
			_onBackwardHandlerCache = delegate { };
			_onPauseHandlerCache = delegate { };
		}
		private void TimeUpdate(float deltaTime)
		{
			OnTimeUpdate(deltaTime);
			OnTimeUpdateHandler?.Invoke(deltaTime);
		}
	}
}