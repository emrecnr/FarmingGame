using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSowingState : State
{
    #region REFERENCES
    private PlayerController _player;
    #endregion

    public PlayerSowingState(PlayerController player)
    {
        _player = player;
    }

    #region METHODS
    public override void EnterState()
    {
        SeedsParticles.onSeedsCollided += SeedsCollidedHandler;
        CropField.OnFullySown += CropFieldFullySownHandler;
    }

    public override void TickState()
    {
        _player.Mover.Move(_player.MoveSpeed, _player.Joystick.MoveDirection);
        _player.PlayerAnimator.Action(_player.Mover.Direction);
        if (_player.CropInteract.IsInteractingWithCropField && !_player.CropInteract.CurrentCrop.IsFieldFullySown())
            _player.PlayerAnimator.PlaySowAnimation();

        else
            _player.PlayerAnimator.StopSowAnimation();

    }

    public override void ExitState()
    {
        SeedsParticles.onSeedsCollided -= SeedsCollidedHandler;
        CropField.OnFullySown -= CropFieldFullySownHandler;
        _player.PlayerAnimator.StopSowAnimation();
    }

    private void SeedsCollidedHandler(Vector3[] seedPositions)
    {
        if (_player.CropInteract.CurrentCrop == null) return;

        _player.CropInteract.CurrentCrop.SeedsCollidedCallback(seedPositions);
    }
    private void CropFieldFullySownHandler(CropField cropField)
    {
        if (cropField == _player.CropInteract.CurrentCrop)
        {   
            _player.PlayerAnimator.StopSowAnimation();
        }
    }
    #endregion

}
