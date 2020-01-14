using TimeSystem.Core;
using UnityEngine;

namespace TimeSystem.Unity
{
	public class Animator : TimeBehaviourComponent
	{
		[SerializeField] private UnityEngine.Animator _animator;

		private float _recordStartTime;
		private float _recordStopTime;
		private float _currentTime;
		private bool _isPlayback;

		public void SetFloat(int id, float value)
		{
			if (!_isPlayback)
			{
				_animator?.SetFloat(id, value);
			}
		}

		protected override void OnTimeUpdate(float deltaTime)
		{
			if (_isPlayback)
			{
				_currentTime = Mathf.Clamp(_currentTime + deltaTime, _recordStartTime, _recordStopTime);
				_animator.playbackTime = _currentTime;
			}
		}
		protected override void OnForward()
		{
			_animator.speed = 1;
			_isPlayback = false;
			_animator.StopPlayback();
			_animator.StartRecording(0);
		}
		protected override void OnBackward()
		{
			_animator.speed = 1;
			_animator.StopRecording();
			_animator.StartPlayback();
			_recordStartTime = _animator.recorderStartTime;
			_recordStopTime = _animator.recorderStopTime;
			_currentTime = _recordStopTime;
			_isPlayback = true;
		}
		protected override void OnPause()
		{
			_animator.speed = 0;
			_animator.StopRecording();
		}

		private void Awake()
		{
		}
	}
}