using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void Enter(RobotinPlayerController player);
    void Update(RobotinPlayerController player);
    void Exit(RobotinPlayerController player);
}

