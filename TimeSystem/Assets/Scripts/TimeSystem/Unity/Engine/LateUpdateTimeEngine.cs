using TimeSystem.Core;
using UnityEngine;

namespace TimeSystem.Unity
{
	public class LateUpdateTimeEngine : TimeEngineBase
	{
		private void LateUpdate()
		{
			_controller.TimeUpdate(Time.deltaTime);
		}
	}
}