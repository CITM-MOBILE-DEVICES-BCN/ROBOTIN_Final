using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    private IPlayerState currentState;

    private readonly RobotinPlayerController playerController;

    public PlayerStateMachine(RobotinPlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void ChangeState(IPlayerState newState)
    {
        if (currentState != null)
        {
            currentState.Exit(playerController);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter(playerController);
        }
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update(playerController);
        }
    }

}
