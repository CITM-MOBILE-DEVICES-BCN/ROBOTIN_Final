using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Configs/PlayerMovementConfig")]
public class PlayerMovementConfig : ScriptableObject
{
    public float maxJumpForce = 10f;
    public float dashForce = 20f;
    public float moveSpeed = 5f;
}
