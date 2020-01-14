using TimeSystem.Core;
using UnityEngine;

namespace TimeSystem.Unity
{
	public abstract class TimeEngineBase : MonoBehaviour
	{
		public float TimeScale
		{
			set
			{
				_timeScale = value;
				if (_controller != null)
				{
					_controller.TimeScale = _timeScale;
				}
			}
		}

		[SerializeField] private string _timeGroup;
		[SerializeField] private float _timeScale;

		protected TimeController _controller { get; private set; }

		[ContextMenu("Forward")]
		public void Forward()
		{
			_controller.Forward();
		}
		[ContextMenu("Backward")]
		public void Backward()
		{
			_controller.Backward();
		}
		[ContextMenu("Pause")]
		public void Pause()
		{
			_controller.Pause();
		}

		protected virtual void Awake()
		{
			_controller = new TimeController(_timeGroup);
			TimeScale = _timeScale;
			Forward();
		}
		protected virtual void OnDestroy()
		{
			_controller.Dispose();
		}
		protected virtual void OnValidate()
		{
			TimeScale = _timeScale;
		}
	}
}