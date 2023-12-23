using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWateringState : State
{
    private PlayerController _player;

    public PlayerWateringState(PlayerController playerController)
    {
        _player = playerController;
    }
    #region METHODS
    public override void EnterState()
    {
        WaterParticles.onWaterCollided += WaterCollidedHandler;
        _player.CropInteract.OnTriggerCrop += OnTriggerCropHandler;
        CropField.OnFullyWatered += CropFieldFullyWaterHandler;
    }

    public override void TickState()
    {
        _player.Mover.Move(_player.MoveSpeed, _player.Joystick.MoveDirection);
        _player.PlayerAnimator.Action(_player.Mover.Direction);
        if (_player.CropInteract.IsInteractingWithCropField && !_player.CropInteract.CurrentCrop.IsFieldFullyWatered())
        {
            _player.PlayerAnimator.PlayWateringAnimation();
            _player.WateringCan.SetActive(true);
        }
        else
        {
            _player.PlayerAnimator.StopWateringAnimation();
            _player.WateringCan.SetActive(false);
        }
    }

    public override void ExitState()
    {
        WaterParticles.onWaterCollided -= WaterCollidedHandler;
        CropField.OnFullySown -= CropFieldFullyWaterHandler;
        _player.WateringCan.SetActive(false);
    }

    private void OnTriggerCropHandler(CropField cropField)
    {

    }
    private void WaterCollidedHandler(Vector3[] waterPositions)
    {

        if (_player.CropInteract.CurrentCrop == null) return;

        _player.CropInteract.CurrentCrop.WaterCollidedCallback(waterPositions);
    }

    private void CropFieldFullyWaterHandler(CropField cropField)
    {
        if (cropField == _player.CropInteract.CurrentCrop)
        {
            _player.PlayerAnimator.StopWateringAnimation();
        }

    }
    #endregion
}
