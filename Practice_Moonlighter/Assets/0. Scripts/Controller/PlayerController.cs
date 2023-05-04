using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Character _character;

	private Vector2 _moveInputVec;

    void Awake()
    {
		_character = GetComponentInChildren<Character>();
	}

	private void Update()
	{
		UpdateInput();
	}

	private void UpdateInput()
	{
		// 움직임 입력 값 처리..
		_moveInputVec.y = Input.GetAxisRaw("Vertical");
		_moveInputVec.x = Input.GetAxisRaw("Horizontal");

		_character.SetMoveDirection(_moveInputVec);

		// 구르기 입력 값 처리..
		if (Input.GetButtonDown("Roll"))
		{
			_character.StartAction((int)Will.ActionType.Roll);
		}

		if(Input.GetButtonDown("Attack"))
		{
			_character.StartAction((int)Will.ActionType.Attack);
		}
	}
}
