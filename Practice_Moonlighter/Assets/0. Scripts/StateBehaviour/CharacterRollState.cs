using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class CharacterRollState : StateMachineBehaviour
{
    private Character _character;
    private CharacterAnimation _characterAniamtion;
    private Animator _animator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (null == _characterAniamtion)
        {
			_character = animator.GetComponentInParent<Character>();
			_characterAniamtion = animator.GetComponent<CharacterAnimation>();
        }

        _characterAniamtion.OnOccurAnimationEvent -= OnOccurAnimationEvent;
        _characterAniamtion.OnOccurAnimationEvent += OnOccurAnimationEvent;

		_animator = animator;

		Debug.Log("Roll On");
	}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _character.MoveToDirection();
	}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		_characterAniamtion.OnOccurAnimationEvent -= OnOccurAnimationEvent;

		_animator.SetBool("isEndAnimation", false);
	}

    private void OnOccurAnimationEvent(string eventName)
    {
        if (eventName == "EndAnimation")
        {
            _animator.SetBool("isEndAnimation", true);
		}
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
