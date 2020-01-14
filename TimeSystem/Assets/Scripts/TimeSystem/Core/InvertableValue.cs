using System;

namespace TimeSystem.Core
{
	public class InvertableValue<T>
	{
		public event Action<Action> OnInvertableEventHandler;

		public T Value
		{
			get { return _value; }
			set
			{
				if (!IsLock && !_value.Equals(value))
				{
					var oldValue = _value;
					OnInvertableEventHandler?.Invoke(() =>
					{
						_value = oldValue;
					});
					_value = value;
				}
			}
		}

		public bool IsLock;

		private T _value;

		public InvertableValue()
		{
			_value = default;
		}
		public InvertableValue(T value)
		{
			_value = value;
		}
	}
}