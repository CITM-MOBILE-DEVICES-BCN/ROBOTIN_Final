using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : IPlayerState
{
    public void Enter(RobotinPlayerController player)
    {
        player.animator.SetTrigger("walk");
    }

    public void Update(RobotinPlayerController player)
    {
        // Movimiento horizontal
        //float direction = player.inputHandler.IsMovingLeft() ? -1 : 1;
        //Vector2 velocity = new Vector2(direction * player.config.moveSpeed, player.rb.velocity.y);
        //player.rb.velocity = velocity;

        //player.animator.SetBool("isMoving", true);
        //player.FlipSprite(direction);

        // Transiciones de estado
        if (!player.environmentChecker.IsGrounded)
        {
            //player.stateMachine.ChangeState(new FallingState());
        }

        if (player.inputHandler.IsJumping())
        {
            //player.stateMachine.ChangeState(new PreparingToJumpState());
        }
    }

    public void Exit(RobotinPlayerController player)
    {
        player.animator.SetBool("isMoving", false);
    }
}
