using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Will : Character
{
	public enum ActionType
	{
		None,
		Roll,
		Attack,
	}

	private ActionType _nextActionType = ActionType.None;

	public override void StartAction(int newActionType)
	{
		if (true == _isLockState)
		{
			_nextActionType = (ActionType)newActionType;
			return;
		}

		ActionType curActionType = (ActionType)newActionType;

		switch (curActionType)
		{
			case ActionType.Roll:
				_isLockState = true;
				animationComponent.ChangeState(CharacterAnimation.AnimationState.Roll);
				break;
			case ActionType.Attack:
				_isLockState = true;
				animationComponent.ChangeState(CharacterAnimation.AnimationState.Attack);
				break;
		}
	}

	protected override void AnimationEvent(string eventName)
	{
		switch (eventName)
		{
			case "EndAnimation":
				_isLockState = false;
				if (_nextActionType == ActionType.None)
				{
					ResetState();
				}
				else
				{
					StartAction((int)_nextActionType);
					_nextActionType = ActionType.None;
				}
				break;
		}
	}
}
