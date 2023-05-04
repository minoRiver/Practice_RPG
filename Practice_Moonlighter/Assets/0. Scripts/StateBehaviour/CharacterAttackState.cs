using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackState : StateMachineBehaviour
{
    private CharacterAnimation _characterAnimation;

	private Animator _animator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(null == _characterAnimation)
        {
			_characterAnimation = animator.GetComponent<CharacterAnimation>();
        }

		_characterAnimation.OnOccurAnimationEvent -= OnOccurAnimationEvent;
		_characterAnimation.OnOccurAnimationEvent += OnOccurAnimationEvent;

		_animator = animator;

		Debug.Log("Attack On");
	}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		_characterAnimation.OnOccurAnimationEvent -= OnOccurAnimationEvent;

		animator.SetBool("isEndAnimation", false);
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
