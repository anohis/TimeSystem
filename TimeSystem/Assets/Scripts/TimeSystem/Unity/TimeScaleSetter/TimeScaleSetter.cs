using TimeSystem.Core;
using UnityEngine;

namespace TimeSystem.Unity
{
	public class TimeScaleSetter : MonoBehaviour
	{
		[SerializeField] private float _timeScale;

		public void Set()
		{
			TimeController.SetTimeScale(_timeScale);
		}
	}
}
