using System;

namespace TimeSystem.Core
{
	public class InvertableTarget<T>
	{
		public event Action<Action> OnInvertableEventHandler;

		public T Value
		{
			get { return _value; }
			set
			{
				if (!IsLock && !_value.Equals(value))
				{
					Finish();
					_isFinish = false;
					_cache = _value;
					_value = value;
				}
			}
		}

		public bool IsLock;

		private T _value;
		private T _cache;
		private bool _isFinish;

		public InvertableTarget()
		{
			_value = default;
			_cache = default;
		}
		public InvertableTarget(T value)
		{
			_value = value;
			_cache = value;
		}

		public void Finish()
		{
			if (!_isFinish)
			{
				_isFinish = true;
				var cache = _cache;
				OnInvertableEventHandler?.Invoke(() =>
				{
					_value = cache;
				});
			}
		}
	}
}