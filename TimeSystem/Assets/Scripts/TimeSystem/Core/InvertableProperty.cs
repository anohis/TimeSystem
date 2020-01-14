using System;

namespace TimeSystem.Core
{
	public class InvertableProperty<T>
	{
		public event Action<Action> OnInvertableEventHandler;

		public T Value
		{
			get { return _getter.Invoke(); }
			set
			{
				var oldValue = Value;
				if (!IsLock && !oldValue.Equals(value))
				{
					OnInvertableEventHandler?.Invoke(() =>
					{
						_setter.Invoke(oldValue);
					});
					_setter.Invoke(value);
				}
			}
		}

		public bool IsLock;

		private Action<T> _setter;
		private Func<T> _getter;

		public InvertableProperty(Action<T> setter, Func<T> getter)
		{
			_setter = setter;
			_getter = getter;
		}
	}
}