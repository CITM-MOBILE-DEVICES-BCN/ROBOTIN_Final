using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : IPlayerState
{
    public void Enter(RobotinPlayerController player)
    {
        player.rb.AddForce(Vector2.up * player.config.maxJumpForce, ForceMode2D.Impulse);
        player.animator.SetTrigger("jump");
    }

    public void Update(RobotinPlayerController player)
    {
        if (player.environmentChecker.IsGrounded)
        {
            //player.stateMachine.ChangeState(new IdleState());
        }
    }

    public void Exit(RobotinPlayerController player) { }
}
