using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
	[SerializeField, Range(1f, 10f)] private float _moveSpeed;

	protected CharacterAnimation animationComponent { get; private set; }
	private Vector2 _moveDirection;
	protected bool _isLockState;

	protected void Awake()
	{
		animationComponent = GetComponentInChildren<CharacterAnimation>();

		animationComponent.OnOccurAnimationEvent -= AnimationEvent;
		animationComponent.OnOccurAnimationEvent += AnimationEvent;
	}

	public void SetMoveDirection(Vector2 moveDirection)
	{
		if (true == _isLockState)
			return;

		if (moveDirection.x != 0f || moveDirection.y != 0f)
		{
			this._moveDirection = moveDirection;

			animationComponent.ChangeState(CharacterAnimation.AnimationState.Move);
			animationComponent.SetMoveDirection(this._moveDirection);
		}
		else
		{
			ResetState();
		}
	}

	public void MoveToDirection()
	{
		transform.Translate(Time.deltaTime * _moveSpeed * this._moveDirection.normalized);
	}

	protected void ResetState()
	{
		if (_moveDirection.y != 0f)
		{
			_moveDirection.Set(0f, _moveDirection.y);
		}

		animationComponent.ChangeState(CharacterAnimation.AnimationState.Idle);
		animationComponent.SetMoveDirection(_moveDirection);
	}

	public abstract void StartAction(int newActionType);

	protected abstract void AnimationEvent(string eventName);
}
