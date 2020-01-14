using TimeSystem.Core;
using UnityEngine;

namespace TimeSystem.Unity
{
	public class Move : TimeBehaviourComponent
	{
		public Vector3 Direction
		{
			get { return _direction.Value; }
			set { _direction.Value = value; }
		}

		[SerializeField] private float _speed;
		private InvertableValue<Vector3> _direction = new InvertableValue<Vector3>();

		protected override void OnTimeUpdate(float deltaTime)
		{
			transform.position += deltaTime * _speed * Direction;
		}
		protected override void OnForward()
		{
			if (_direction != null)
			{
				_direction.IsLock = false;
			}
		}
		protected override void OnBackward()
		{
			if (_direction != null)
			{
				_direction.IsLock = true;
			}
		}
		protected override void OnPause()
		{
			if (_direction != null)
			{
				_direction.IsLock = true;
			}
		}

		private void Awake()
		{
			_direction.OnInvertableEventHandler += ReceiveInvertableEvent;
		}
	}
}