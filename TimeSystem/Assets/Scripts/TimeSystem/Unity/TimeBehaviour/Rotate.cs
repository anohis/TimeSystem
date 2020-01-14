using TimeSystem.Core;
using UnityEngine;

namespace TimeSystem.Unity
{
	public class Rotate : TimeBehaviourComponent
	{
		public Vector3 Forward
		{
			get { return _forward.Value; }
			set
			{
				if (value != Vector3.zero)
				{
					_forward.Value = value.normalized;
				}
			}
		}

		[SerializeField] private float _speed;
		private InvertableTarget<Vector3> _forward;

		protected override void OnTimeUpdate(float deltaTime)
		{
			var rotation = Quaternion.LookRotation(Forward);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Mathf.Abs(deltaTime) * _speed);
			if (transform.forward == Forward)
			{
				_forward.Finish();
			}
		}
		protected override void OnForward()
		{
			if (_forward != null)
			{
				_forward.IsLock = false;
			}
		}
		protected override void OnBackward()
		{
			if (_forward != null)
			{
				_forward.IsLock = true;
			}
		}
		protected override void OnPause()
		{
			if (_forward != null)
			{
				_forward.IsLock = true;
			}
		}

		private void Awake()
		{
			_forward = new InvertableTarget<Vector3>(transform.forward);
			_forward.OnInvertableEventHandler += ReceiveInvertableEvent;
			_forward.Finish();
		}
	}
}
