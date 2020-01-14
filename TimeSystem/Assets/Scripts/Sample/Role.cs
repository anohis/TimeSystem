using TimeSystem.Unity;
using UnityEngine;

[RequireComponent(typeof(Move))]
[RequireComponent(typeof(Rotate))]
[RequireComponent(typeof(TimeSystem.Unity.Animator))]
public class Role : MonoBehaviour
{
	private static readonly int _animatorHashSpeed = UnityEngine.Animator.StringToHash("Speed");

	private Move _move;
	private Rotate _rotate;
	private TimeSystem.Unity.Animator _animator;

	private void Awake()
	{
		_move = gameObject.GetComponent<Move>();
		_rotate = gameObject.GetComponent<Rotate>();
		_animator = gameObject.GetComponent<TimeSystem.Unity.Animator>();
	}
	private void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			RotateAndMove(new Vector3(-1, 0, 0));
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			RotateAndMove(new Vector3(1, 0, 0));
		}
		else if (Input.GetKey(KeyCode.UpArrow))
		{
			RotateAndMove(new Vector3(0, 0, 1));
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			RotateAndMove(new Vector3(0, 0, -1));
		}
		else
		{
			_move.Direction = Vector3.zero;
			_animator.SetFloat(_animatorHashSpeed, 0);
		}
	}

	private void RotateAndMove(Vector3 forward)
	{
		forward.Normalize();
		_rotate.Forward = forward;
		if (transform.forward == forward)
		{
			_move.Direction = forward;
			_animator.SetFloat(_animatorHashSpeed, 10);
		}
		else
		{
			_move.Direction = Vector3.zero;
			_animator.SetFloat(_animatorHashSpeed, 0);
		}
	}
}