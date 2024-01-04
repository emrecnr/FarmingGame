using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private PlayerController _player;
    public IdleState(PlayerController player)
    {
        _player = player;
    }

    public override void EnterState()
    {
        
    }

    public override void TickState()
    {
        _player.Mover.Move(_player.MoveSpeed,_player.Joystick.MoveDirection);
        _player.PlayerAnimator.Action(_player.Mover.Direction);
    }

    public override void ExitState()
    {
        
    }
}
