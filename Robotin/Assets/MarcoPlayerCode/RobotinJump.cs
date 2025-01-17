using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotinJump : MonoBehaviour
{
    [SerializeField] private float minJumpForce = 5f;
    [SerializeField] private float maxJumpForce = 15f;
    [SerializeField] private float maxHoldTime = 1f;
    [SerializeField] private float horizontalJumpForce = 5f; // Add horizontal force

    public Rigidbody2D rb;
    public RobotinCollision robotinCollision;
    public RobotinMovement robotinMovement;
    public float jumpHoldTime;
    public bool isJumping;
    public bool isJumpButtonPressed; // Track if the jump button is pressed

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && (robotinCollision.IsGrounded || robotinCollision.IsWallSliding))
        {
            isJumpButtonPressed = true;
            jumpHoldTime = 0f;
            // Optionally, play a "charging" animation
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (isJumpButtonPressed)
        {
            if (Input.GetButton("Jump"))
            {
                jumpHoldTime += Time.deltaTime;

                if (jumpHoldTime >= maxHoldTime)
                {
                    jumpHoldTime = maxHoldTime;
                    isJumpButtonPressed = false;
                    Jump(maxJumpForce);
                }
            }
            else
            {
                isJumpButtonPressed = false; 
            }

            if (!isJumpButtonPressed && jumpHoldTime > 0)
            {
                float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, jumpHoldTime / maxHoldTime);
                Jump(jumpForce);
                GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }

        if (rb.velocity.y < 0.2f && isJumping) isJumping = false;
    }

    private void Jump(float jumpForce)
    {
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        if (robotinCollision.IsWallSliding)
        {
            robotinMovement.ChangeDirection();
            rb.AddForce(new Vector2(robotinMovement.GetDirection() * horizontalJumpForce, 0f), ForceMode2D.Impulse);
        }
        else
        {
            // Add horizontal force based on the current direction
            rb.AddForce(new Vector2(robotinMovement.GetDirection() * horizontalJumpForce, 0f), ForceMode2D.Impulse);
        }

        robotinCollision.IsGrounded = false;
        robotinCollision.IsWallSliding = false;
        // jump animation
    }
}