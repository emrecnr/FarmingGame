using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimator 
{
    private Animator _playerAnimator;

    private const string _runAnimationName = "Running";
    private const string _idleAnimationName = "Idle";

    public PlayerAnimator(Animator animator)
    {
        _playerAnimator = animator;
    }


    public void Action(Vector3 moveDirection)
    {
        PlayAnimation(moveDirection);
    }

    private void PlayAnimation(Vector3 moveDirection)
    {
        if(moveDirection.magnitude >0)
        {
            RunAnimation();
            _playerAnimator.gameObject.transform.forward = moveDirection;
        }
        else
        {
            IdleAnimation();
        }
        
    }

    private void IdleAnimation()
    {
        _playerAnimator.Play(_idleAnimationName);
    }
    private void RunAnimation()
    {
        _playerAnimator.Play(_runAnimationName);        
    }
}
