using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimator 
{
    private Animator _playerAnimator;

    private const string _runAnimationName = "Running";
    private const string _idleAnimationName = "Idle";
    private const string _sowAnimationName = "Sow";

    private ParticleSystem _waterParticles;

    public PlayerAnimator(Animator animator, ParticleSystem waterParticles)
    {
        _playerAnimator = animator;
        _waterParticles = waterParticles;
    }


    public void Action(Vector3 moveDirection)
    {
        PlayAnimation(moveDirection);
    }

    private void PlayAnimation(Vector3 moveDirection)
    {
        if(moveDirection.magnitude >0)
        {
            PlayRunAnimation();
            _playerAnimator.gameObject.transform.forward = moveDirection;
        }
        else
        {
            PlayIdleAnimation();
        }
        
    }

    private void PlayIdleAnimation()
    {
        _playerAnimator.Play(_idleAnimationName);
    }
    private void PlayRunAnimation()
    {
        _playerAnimator.Play(_runAnimationName);        
    }
    public void PlaySowAnimation()
    {
        _playerAnimator.SetLayerWeight(1,1);
    }
    public void StopSowAnimation()
    {
        _playerAnimator.SetLayerWeight(1, 0);
    }
    public void PlayWateringAnimation()
    {
        _playerAnimator.SetLayerWeight(2,1);
    }
    public void StopWateringAnimation()
    {
        _playerAnimator.SetLayerWeight(2, 0);
        _waterParticles.Stop();
    }

}
