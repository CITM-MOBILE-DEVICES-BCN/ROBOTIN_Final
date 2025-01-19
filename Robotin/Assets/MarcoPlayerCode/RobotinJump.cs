using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microlight.MicroAudio;

public class RobotinJump : MonoBehaviour
{
    [SerializeField] private float minJumpForceY = 5f;
    [SerializeField] private float maxJumpForceY = 15f;
    [SerializeField] private float minJumpForceX = 5f;
    [SerializeField] private float maxJumpForceX = 15f;
    [SerializeField] private float maxHoldTime = 1f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] MicroSoundGroup jumpSounds; // always write varibles in camelCase pls

    public float additionalJumpForce = 0f;
    public Rigidbody2D rb;
    public RobotinCollision robotinCollision;
    public RobotinMovement robotinMovement;
    public float jumpHoldTime;
    public bool isJumping;
    public bool isJumpButtonPressed;
    private float jumpBufferCounter;

    private void Update()
    {
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
                GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }

        if (rb.velocity.y < 0.2f && isJumping) isJumping = false;
    }

    private void Jump(float jumpForceX, float jumpForceY)
    {
        isJumping = true;
        rb.velocity = Vector2.zero;

        if (robotinCollision.IsWallSliding)
        {
            robotinMovement.ChangeDirection();
        }
        
        rb.AddForce(new Vector2(jumpForceX * robotinMovement.GetDirection(), jumpForceY + additionalJumpForce), ForceMode2D.Impulse);

        robotinCollision.IsGrounded = false;
        robotinCollision.IsWallSliding = false;

        MicroAudio.PlayEffectSound(jumpSounds.GetRandomClip);

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