using UnityEngine;

public class RobotinMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int direction = 1; // 1 for right, -1 for left
    [SerializeField] private float directionChangeCooldown = 0.2f; // Cooldown time in seconds
    [SerializeField] private string walkingSoundGroup = "Walking";
    [SerializeField] private string turnSoundGroup = "Turn";
    [SerializeField] private ParticleSystem movementParticles;
    public Rigidbody2D rb;
    public RobotinCollision playerCollision;
    public RobotinJump robotinJump;

    private float lastDirectionChangeTime;
    private Vector2 windForce = Vector2.zero;

    private void Start()
    {
        FindObjectOfType<WindSystem>().OnWindStart += ApplyWindForce;
        FindObjectOfType<WindSystem>().OnWindStop += StopWindForce;

        if (movementParticles == null)
        {
            Debug.LogError("Movement particles not assigned!");
        }
        else
        {
            movementParticles.Stop(); // Ensure particles are disabled at the start
            Debug.Log("Movement particles assigned and stopped at start.");
        }
    }
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

            // Play turn sound
            SFXManager.Instance.PlayEffect(turnSoundGroup);

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

            if (!robotinJump.isJumping)
            {
                Move();
                // Play walking sound when moving and grounded, but not during landing
                if (Mathf.Abs(rb.velocity.x) > 0.1f && !robotinJump.IsInLandingCooldown())
                {
                    SFXManager.Instance.PlayEffect(walkingSoundGroup);

                    if (movementParticles != null && !movementParticles.isPlaying)
                    {
                        movementParticles.Play();
                        Debug.Log("Movement particles started.");
                    }

                }

                else
                {
                    // Stop particles when not moving
                    if (movementParticles != null && movementParticles.isPlaying)
                    {
                        movementParticles.Stop();
                    }
                }
            }
        }

        if (robotinJump.isJumpButtonPressed)
        {
            rb.velocity = Vector2.zero;

            if (movementParticles != null && movementParticles.isPlaying)
            {
                movementParticles.Stop();
            }


        }
    }

    private void FixedUpdate()
    {
        if (windForce != Vector2.zero)
        {
            rb.AddForce(windForce, ForceMode2D.Force);
        }
    }

    private void ApplyWindForce(Vector2 force)
    {
        windForce = force;
    }

    private void StopWindForce()
    {
        windForce = Vector2.zero;
    }

    public int GetDirection()
    {
        return direction;
    }

}