using TimeSystem.Core;
using UnityEngine;

namespace TimeSystem.Unity
{
	public class FixedUpdateTimeEngine : TimeEngineBase
	{
		private void FixedUpdate()
		{
			_controller.TimeUpdate(Time.deltaTime);
		}
	}
}