using System;
using System.Collections.Generic;

namespace TimeSystem.Core
{
	public partial class TimeController
	{
		private static Dictionary<string, TimeController> _controllers = new Dictionary<string, TimeController>();

		public static void Register(string groupName, TimeObject obj)
		{
			if (_controllers.TryGetValue(groupName, out var controller))
			{
				controller.AddObject(obj);
			}
		}
		public static void Unregister(string groupName, TimeObject obj)
		{
			if (_controllers.TryGetValue(groupName, out var controller))
			{
				controller.RemoveObject(obj);
			}
		}
		public static void SetTimeScale(float timeScale)
		{
			foreach (var controller in _controllers.Values)
			{
				controller.TimeScale = timeScale;
			}
		}
		public static void SetTimeScale(string groupName, float timeScale)
		{
			if (_controllers.TryGetValue(groupName, out var controller))
			{
				controller.TimeScale = timeScale;
			}
		}
		public static void PlayForward()
		{
			foreach (var controller in _controllers.Values)
			{
				controller.Forward();
			}
		}
		public static void PlayForward(string groupName)
		{
			if (_controllers.TryGetValue(groupName, out var controller))
			{
				controller.Forward();
			}
		}
		public static void PlayBackward()
		{
			foreach (var controller in _controllers.Values)
			{
				controller.Backward();
			}
		}
		public static void PlayBackward(string groupName)
		{
			if (_controllers.TryGetValue(groupName, out var controller))
			{
				controller.Backward();
			}
		}
		public static void DoPause()
		{
			foreach (var controller in _controllers.Values)
			{
				controller.Pause();
			}
		}
		public static void DoPause(string groupName)
		{
			if (_controllers.TryGetValue(groupName, out var controller))
			{
				controller.Pause();
			}
		}

		private static void Register(string groupName, TimeController controller)
		{
			if (!_controllers.ContainsKey(groupName))
			{
				_controllers.Add(groupName, controller);
			}
		}
		private static void Unregister(string groupName)
		{
			_controllers.Remove(groupName);
		}
	}
}