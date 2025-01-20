using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotinJump : MonoBehaviour
{
    [SerializeField] private float minJumpForceY = 5f;
    [SerializeField] private float maxJumpForceY = 15f;
    [SerializeField] private float minJumpForceX = 5f;
    [SerializeField] private float maxJumpForceX = 15f;
    [SerializeField] private float maxHoldTime = 1f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private string jumpChargeSound = "JumpCharge";
    [SerializeField] private string jumpReleaseSound = "JumpRelease";
    [SerializeField] private string landingSound = "Landing";
    [SerializeField] private float landingSoundDelay = 0.1f; // Delay before walking sounds can play

    public float additionalJumpForce = 0f;
    public Rigidbody2D rb;
    public RobotinCollision robotinCollision;
    public RobotinMovement robotinMovement;
    public float jumpHoldTime;
    public bool isJumping;
    public bool isJumpButtonPressed;
    private float jumpBufferCounter;
    private float landingCooldown;

    private void Update()
    {
        if (landingCooldown > 0)
        {
            landingCooldown -= Time.deltaTime;
        }

        // Jump Input Buffering
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Jumping Logic
        if (jumpBufferCounter > 0 && (robotinCollision.IsGrounded || robotinCollision.IsWallSliding))
        {
            isJumpButtonPressed = true;
            jumpBufferCounter = 0;
            jumpHoldTime = 0f;
            // Play charge sound
            SFXManager.Instance.PlayEffect(jumpChargeSound);
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
                    Jump(maxJumpForceX, maxJumpForceY);
                }
            }
            else
            {
                isJumpButtonPressed = false;
            }

            if (!isJumpButtonPressed && jumpHoldTime > 0)
            {
                float jumpForceY = Mathf.Lerp(minJumpForceY, maxJumpForceY, jumpHoldTime / maxHoldTime);
                float jumpForceX = Mathf.Lerp(minJumpForceX, maxJumpForceX, jumpHoldTime / maxHoldTime);
                Jump(jumpForceX, jumpForceY);
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        // Check for landing
        if (rb.velocity.y < 0.2f && isJumping)
        {
            isJumping = false;
            if (robotinCollision.IsGrounded)
            {
                SFXManager.Instance.PlayEffect(landingSound);
                landingCooldown = landingSoundDelay;
            }
        }
    }

    public bool IsInLandingCooldown()
    {
        return landingCooldown > 0;
    }

    private void Jump(float jumpForceX, float jumpForceY)
    {
        isJumping = true;
        rb.velocity = Vector2.zero;

        if (robotinCollision.IsWallSliding)
        {
            robotinMovement.ChangeDirection();
        }
        
        // Play jump release sound
        SFXManager.Instance.PlayEffect(jumpReleaseSound);
        
        rb.AddForce(new Vector2(jumpForceX * robotinMovement.GetDirection(), jumpForceY + additionalJumpForce), ForceMode2D.Impulse);

        robotinCollision.IsGrounded = false;
        robotinCollision.IsWallSliding = false;

        // jump animation
    }

    public void ApplyAdditionalJumpForce(float force)
    {
        additionalJumpForce += force;
    }

    public void RemoveAdditionalJumpForce(float force)
    {
        additionalJumpForce -= force;
    }
}