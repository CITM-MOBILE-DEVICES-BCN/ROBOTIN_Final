using UnityEngine;
using Microlight.MicroAudio;

public class RobotinMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int direction = 1; // 1 for right, -1 for left
    [SerializeField] private float directionChangeCooldown = 0.2f; // Cooldown time in seconds
    public Rigidbody2D rb;
    public RobotinCollision playerCollision;
    public RobotinJump robotinJump;


    [SerializeField] private MicroSoundGroup _walkingSounds;

    private float lastDirectionChangeTime;

    public void Move()
    {
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    public void ChangeDirection()
    {
        // Check if enough time has passed since the last direction change
        if (Time.time - lastDirectionChangeTime > directionChangeCooldown)
        {
            direction *= -1;
            // Flip the sprite
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            lastDirectionChangeTime = Time.time; // Update the last change time
        }
    }

    private void Update()
    {
        // Skip direction change if jumping
        if (!robotinJump.isJumping && playerCollision.IsGrounded)
        {
            if (playerCollision.IsAtEdge || playerCollision.IsNearWall)
            {
                ChangeDirection();
                playerCollision.IsAtEdge = false;
                playerCollision.IsNearWall = false;
            }

            if (!robotinJump.isJumping) Move();
            MicroAudio.PlayEffectSound(_walkingSounds.GetRandomClip, 0.5f,1, 1, true);
        }

        if (robotinJump.isJumpButtonPressed) rb.velocity = Vector2.zero; 
    }

    public int GetDirection()
    {
        return direction;
    }

}