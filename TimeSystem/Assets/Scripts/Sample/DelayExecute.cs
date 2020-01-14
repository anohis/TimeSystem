using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayExecute : MonoBehaviour
{
	[SerializeField] private float _delayTime;
	[SerializeField] private UnityEvent _event;

	private float _time = 0;

	private void Update()
	{
		if (_time >= _delayTime)
		{
			_event?.Invoke();
			Destroy(gameObject);
		}
		_time += Time.deltaTime;
	}
}
