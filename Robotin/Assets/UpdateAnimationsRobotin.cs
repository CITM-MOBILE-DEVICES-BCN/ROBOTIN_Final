using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAnimationsRobotin : MonoBehaviour
{
    public string walkAnimationName = "Walk";
    public string upVelocityAnimationName = "Jumping";
    public string downVelocityAnimationName = "Falling";
    public string ChargingAnimationName = "ChargingGround";
    public string ChargingWallAnimationName = "ChargingWall";
    public string WallSlidingAnimationName = "WallSliding";

    public Animator animator;

    public RobotinMovement robotinMovement;
    public RobotinJump robotinJump;
    public RobotinCollision robotinCollision;
    public Rigidbody2D rb;

    public enum PlayerState
    {
        Walking,
        Jumping,
        Falling,
        ChargingGround,
        ChargingWall,
        WallSliding
    }

    public PlayerState playerState;

    private void Update()
    {        
        UpdateRobotinState();
        PlayAnimation();
    }

    private void UpdateRobotinState()
    {
        
        if (robotinJump.isJumpButtonPressed && robotinCollision.IsWallSliding)
        {
            playerState = PlayerState.ChargingWall;
        }
        else if (robotinCollision.IsNearWall && !robotinCollision.IsGrounded)
        {
            playerState = PlayerState.WallSliding;
        }
        else if (robotinCollision.IsWallSliding)
        {
            playerState = PlayerState.WallSliding;
        }
        else if (robotinJump.isJumpButtonPressed && robotinCollision.IsGrounded)
        {
            playerState = PlayerState.ChargingGround;
        }
        else if (rb.velocity.y < 0 && !robotinCollision.IsGrounded && !robotinCollision.IsNearWall)
        {
            playerState = PlayerState.Falling;
        }
        else if (rb.velocity.y > 0 && !robotinCollision.IsGrounded && !robotinCollision.IsNearWall)
        {
            playerState = PlayerState.Jumping;
        }
        else if (robotinCollision.IsGrounded)
        {
            playerState = PlayerState.Walking;
        }
        else
        {
            playerState = PlayerState.Walking;
        }
    }

    private void PlayAnimation()
    {
        switch (playerState)
        {
            case PlayerState.Walking:
                animator.Play(walkAnimationName);
                break;
            case PlayerState.Jumping:
                animator.Play(upVelocityAnimationName);
                break;
            case PlayerState.Falling:
                animator.Play(downVelocityAnimationName);
                break;
            case PlayerState.ChargingGround:
                animator.Play(ChargingAnimationName);
                break;
            case PlayerState.ChargingWall:
                animator.Play(ChargingWallAnimationName);
                break;
            case PlayerState.WallSliding:
                animator.Play(WallSlidingAnimationName);
                break;
            default:
                animator.Play(walkAnimationName);
                break;
        }
    }
}
