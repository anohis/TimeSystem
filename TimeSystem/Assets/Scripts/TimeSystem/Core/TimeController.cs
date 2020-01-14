using System;
using System.Collections.Generic;

namespace TimeSystem.Core
{
	public partial class TimeController : TimeObject, IDisposable
	{
		private abstract class State
		{
			protected TimeObject _timeObject;

			public State(TimeObject timeObject)
			{
				_timeObject = timeObject;
			}

			public abstract void OnAddObject(TimeObject obj);
		}
		private class ForwardState : State
		{
			public ForwardState(TimeObject timeObject) : base(timeObject)
			{
			}

			public override void OnAddObject(TimeObject obj)
			{
				obj.Forward();
			}
		}
		private class BackwardState : State
		{
			public BackwardState(TimeObject timeObject) : base(timeObject)
			{
			}

			public override void OnAddObject(TimeObject obj)
			{
				obj.Backward();
			}
		}
		private class PauseState : State
		{
			public PauseState(TimeObject timeObject) : base(timeObject)
			{
			}

			public override void OnAddObject(TimeObject obj)
			{
				obj.Pause();
			}
		}

		private struct EventData
		{
			public float Time;
			public Action InvertAction;
		};

		public float Time
		{
			get { return _time; }
			set
			{
				var deltaTime = value - _time;
				OnTimeUpdate(deltaTime);
			}
		}

		private string _groupName;
		private HashSet<TimeObject> _objects = new HashSet<TimeObject>();
		private float _time = 0;
		private Stack<EventData> _events = new Stack<EventData>();
		private State _currentState;
		private ForwardState _forwardState;
		private BackwardState _backwardState;
		private PauseState _pauseState;

		public TimeController(string groupName)
		{
			_forwardState = new ForwardState(this);
			_backwardState = new BackwardState(this);
			_pauseState = new PauseState(this);

			_currentState = _pauseState;
			_groupName = groupName;
			Register(_groupName, this);
		}
		public void AddObject(TimeObject obj)
		{
			obj.OnInvertableEventHandler += OnInvertableEvent;
			_objects.Add(obj);
			_currentState.OnAddObject(obj);
		}
		public void RemoveObject(TimeObject obj)
		{
			obj.OnInvertableEventHandler -= OnInvertableEvent;
			_objects.Remove(obj);
		}
		public void Dispose()
		{
			Unregister(_groupName);
		}
		public override void Forward()
		{
			base.Forward();
			foreach (var obj in _objects)
			{
				obj.Forward();
			}
			_currentState = _forwardState;
		}
		public override void Backward()
		{
			base.Backward();
			foreach (var obj in _objects)
			{
				obj.Backward();
			}
			_currentState = _backwardState;
		}
		public override void Pause()
		{
			base.Pause();
			foreach (var obj in _objects)
			{
				obj.Pause();
			}
			_currentState = _pauseState;
		}
		public override void OnInvertableEvent(Action action)
		{
			_events.Push(new EventData
			{
				Time = Time,
				InvertAction = action
			});
		}

		protected override void OnTimeUpdate(float deltaTime)
		{
			if (deltaTime > 0)
			{
				ForwardUpdateTime(deltaTime);
			}
			else if (deltaTime < 0)
			{
				BackwardUpdateTime(deltaTime);
			}
		}

		private void UpdateTime(float deltaTime)
		{
			var absDeltaTime = Math.Abs(deltaTime);
			foreach (var obj in _objects)
			{
				obj.TimeUpdate(absDeltaTime);
			}
			_time += deltaTime;
		}
		private void ForwardUpdateTime(float deltaTime)
		{
			UpdateTime(deltaTime);
		}
		private void BackwardUpdateTime(float deltaTime)
		{
			var value = Math.Max(_time + deltaTime, 0);
			while (_events.Count > 0 && _events.Peek().Time >= value)
			{
				var timeEvent = _events.Pop();
				var eventDeltaTime = timeEvent.Time - _time;
				UpdateTime(eventDeltaTime);
				timeEvent.InvertAction?.Invoke();
				_time = timeEvent.Time;
			}
			if (value < _time)
			{
				deltaTime = value - _time;
				UpdateTime(deltaTime);
			}
		}
	}
}