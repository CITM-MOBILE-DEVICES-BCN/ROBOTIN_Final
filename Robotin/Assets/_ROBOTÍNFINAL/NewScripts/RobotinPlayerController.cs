using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnvironmentChecker))]
[RequireComponent(typeof(InputHandler))]
public class RobotinPlayerController : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }

    public PlayerMovementConfig config;
    public InputHandler inputHandler { get; private set; }
    public EnvironmentChecker environmentChecker { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; } 
}
