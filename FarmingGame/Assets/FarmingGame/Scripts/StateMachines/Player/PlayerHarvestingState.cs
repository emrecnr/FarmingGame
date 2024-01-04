using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvestingState : State
{
    private PlayerController _player;    

    public PlayerHarvestingState(PlayerController playerController)
     {
        _player = playerController;
     }



    public override void EnterState()
    {
        CropField.OnFullyHarvested += OnFullyHarvestedHandler;
        _player.PlayerAnimationEvents.startHarvestingEvent += StartHarvestingEventHandler;
    }

    public override void TickState()
    {
        _player.Mover.Move(_player.MoveSpeed, _player.Joystick.MoveDirection);
        _player.PlayerAnimator.Action(_player.Mover.Direction);

        if (_player.CropInteract.IsInteractingWithCropField && !_player.CropInteract.CurrentCrop.IsFieldFullyHarvested())
            _player.PlayerAnimator.PlayHarvestingAnimation();

        else
            _player.PlayerAnimator.StopHarvestingAnimation();

    }

    public override void ExitState()
    {
        _player.PlayerAnimator.StopHarvestingAnimation();

        CropField.OnFullyHarvested -= OnFullyHarvestedHandler;
    }

    private void OnFullyHarvestedHandler(CropField cropField)
    {
        _player.PlayerAnimator.StopHarvestingAnimation();
    }
    private void StartHarvestingEventHandler()
    {
        _player.CropInteract.CurrentCrop.HarvestCallback(_player.HarvestSphereArea);        
    }
}
