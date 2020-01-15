using System;

namespace TimeSystem.Core
{
	public class TimeObject
	{
		private abstract class State
		{
			public abstract float TimeScale { get; set; }

			protected TimeObject _timeObject;

			public State(TimeObject timeObject)
			{
				_timeObject = timeObject;
			}
		}
		private class ForwardState : State
		{
			public override float TimeScale
			{
				get{ return _timeObject._timeScale; }
				set
				{
					if (value > 0)
					{
						var old = _timeObject._timeScale;
						_timeObject._timeScale = value;
						_timeObject.OnInvertableEvent(() =>
						{
							_timeObject._timeScale = old;
						});
					}
				}
			}

			public ForwardState(TimeObject timeObject) : base(timeObject)
			{
			}
		}
		private class BackwardState : State
		{
			public override float TimeScale
			{
				get { return -_timeObject._timeScale; }
				set
				{
					if (value > 0)
					{
						_timeObject._timeScale = value;
					}
				}
			}
			public BackwardState(TimeObject timeObject) : base(timeObject)
			{
			}
		}
		private class PauseState : State
		{
			public override float TimeScale
			{
				get { return 0; }
				set
				{
					if (value > 0)
					{
						_timeObject._timeScale = value;
					}
				}
			}

			public PauseState(TimeObject timeObject) : base(timeObject)
			{
			}
		}

		public event Action<Action> OnInvertableEventHandler;
		public event Action<float> OnTimeUpdateHandler;
		public event Action OnForwardHandler
		{
			add
			{
				_onForwardHandler += value;
				if (_currentState is ForwardState)
				{
					value?.Invoke();
				}
			}
			remove
			{
				_onForwardHandler -= value;
			}
		}
		public event Action OnBackwardHandler
		{
			add
			{
				_onBackwardHandler += value;
				if (_currentState is BackwardState)
				{
					value?.Invoke();
				}
			}
			remove
			{
				_onBackwardHandler -= value;
			}
		}
		public event Action OnPauseHandler
		{
			add
			{
				_onPauseHandler += value;
				if (_currentState is PauseState)
				{
					value?.Invoke();
				}
			}
			remove
			{
				_onPauseHandler -= value;
			}
		}

		public float TimeScale
		{
			get { return _currentState.TimeScale; }
			set { _currentState.TimeScale = value; }
		}

		private float _timeScale;
		private State _currentState;
		private ForwardState _forwardState;
		private BackwardState _backwardState;
		private PauseState _pauseState;
		private Action _onForwardHandler;
		private Action _onBackwardHandler;
		private Action _onPauseHandler;

		public TimeObject()
		{
			_forwardState = new ForwardState(this);
			_backwardState = new BackwardState(this);
			_pauseState = new PauseState(this);

			_currentState = _pauseState;
		}

		public void TimeUpdate(float deltaTime)
		{
			deltaTime *= TimeScale;
			OnTimeUpdate(deltaTime);
			OnTimeUpdateHandler?.Invoke(deltaTime);
		}

		public virtual void OnInvertableEvent(Action action)
		{
			OnInvertableEventHandler?.Invoke(action);
		}
		public virtual void Forward()
		{
            if (_currentState != _forwardState)
            {
                _currentState = _forwardState;
                _onForwardHandler?.Invoke();
            }
		}
		public virtual void Backward()
		{
            if (_currentState != _backwardState)
            {
                _currentState = _backwardState;
                _onBackwardHandler?.Invoke();
            }
		}
        public virtual void Pause()
        {
            if (_currentState != _pauseState)
            {
                _currentState = _pauseState;
                _onPauseHandler?.Invoke();
            }
        }

		protected virtual void OnTimeUpdate(float deltaTime)
		{
		}
	}
}