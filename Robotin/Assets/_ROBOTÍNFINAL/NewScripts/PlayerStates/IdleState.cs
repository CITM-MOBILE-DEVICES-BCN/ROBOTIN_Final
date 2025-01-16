using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IPlayerState
{
    public void Enter(RobotinPlayerController player)
    {
        player.animator.SetTrigger("idle");
    }

    public void Update(RobotinPlayerController player)
    {
        if (player.inputHandler.IsMoving())
        {
            //player.stateMachine.ChangeState(new MovingState());
        }

        if (player.inputHandler.IsJumping())
        {
            //player.stateMachine.ChangeState(new PreparingToJumpState());
        }
    }

    public void Exit(RobotinPlayerController player) { }
}
