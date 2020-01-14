using TimeSystem.Core;
using UnityEngine;

namespace TimeSystem.Unity
{
	public class UpdateTimeEngine : TimeEngineBase
	{
		private void Update()
		{
			_controller.TimeUpdate(Time.deltaTime);
		}
	}
}