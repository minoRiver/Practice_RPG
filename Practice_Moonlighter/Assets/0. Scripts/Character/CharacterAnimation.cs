using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
	public event Action<string> OnOccurAnimationEvent;

	public enum AnimationState
	{
		Idle,
		Move,
		Roll,
		Attack
	}

	private Animator _animator;
	private Animator _weaponAnimator;

	private AnimationState _curAnimState;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_weaponAnimator = transform.GetChild(0).GetComponent<Animator>();

		_weaponAnimator.gameObject.SetActive(false);
	}

	public void SetMoveDirection(Vector2 moveDirection)
	{
		_animator.SetFloat("Horizontal", moveDirection.x);
		_animator.SetFloat("Vertical", moveDirection.y);
	}

	public void ChangeState(AnimationState newAnimState)
	{
		switch (_curAnimState)
		{
			case AnimationState.Idle:
				break;
			case AnimationState.Move:
				break;
			case AnimationState.Roll:
				break;
			case AnimationState.Attack:
				_weaponAnimator.gameObject.SetActive(false);
				break;
		}

		_curAnimState = newAnimState;

		switch (newAnimState)
		{
			case AnimationState.Idle:
				_animator.SetBool("isMove", false);
				break;
			case AnimationState.Move:
				_animator.SetBool("isMove", true);
				break;
			case AnimationState.Roll:
				_animator.SetTrigger("OnRoll");
				break;
			case AnimationState.Attack:
				_animator.SetTrigger("OnAttack");
				_weaponAnimator.gameObject.SetActive(true);
				_weaponAnimator.SetFloat("Horizontal", _animator.GetFloat("Horizontal"));
				_weaponAnimator.SetFloat("Vertical", _animator.GetFloat("Vertical"));
				break;
		}
	}

	public void OnAnimationEvent(string eventName)
	{
		OnOccurAnimationEvent?.Invoke(eventName);
	}
}
