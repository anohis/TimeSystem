using TimeSystem.Core;
using UnityEngine;

namespace TimeSystem.Unity
{
	public class TimeGroupScaleSetter : MonoBehaviour
	{
		[SerializeField] private string _timeGroup;
		[SerializeField] private float _timeScale;

		public void Set()
		{
			TimeController.SetTimeScale(_timeGroup, _timeScale);
		}
	}
}
