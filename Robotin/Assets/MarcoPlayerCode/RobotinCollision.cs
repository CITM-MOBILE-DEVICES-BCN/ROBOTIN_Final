using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class RobotinCollision : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float wallSlideSpeed = 1f;
    [SerializeField] private float edgeCheckDistance = 0.1f;
    [SerializeField] private float downGravity = 5f;
    [SerializeField] private float upGravity = 4f;
    [SerializeField] private float wallCheckDistance = 0.1f;
    [SerializeField] private float jumpGroundCheckDistance = 0.3f; // Distance to check for ground to confirm landing
    [SerializeField] private LayerMask noWallJumpLayer;
    [SerializeField] private string wallSlideSound = "WallSlide";

    public bool isSticked = false;


    public bool IsGrounded;
    public bool IsWallSliding;
    public bool IsAtEdge;
    public bool IsNearWall;
    public bool isJumping;

    public Rigidbody2D rb;
    public Collider2D col;
    public RobotinMovement robotinMovement;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
        IsWallSliding = false;
    }

    private void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            float upAngle = Vector2.Angle(Vector2.up, normal);

            if (upAngle < 45f)
            {
                IsGrounded = true;
                isSticked = false;
                isJumping = false;
            }
        }
    }

    private void Update()
    {
        if (IsWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            rb.gravityScale = 0f;

            if(isSticked == false)
            {
                SFXManager.Instance.PlayEffect(wallSlideSound);
                isSticked = true;
            }
        }
        else
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = downGravity;
            }
            else
            {
                rb.gravityScale = upGravity;
            }
        }

        CheckForEdge();
        CheckForWall();
    }

    private void CheckForEdge()
    {
        if (IsGrounded)
        {
            Vector2 edgeCheckLeft = (Vector2)transform.position + new Vector2(-col.bounds.extents.x, -col.bounds.extents.y);
            Vector2 edgeCheckRight = (Vector2)transform.position + new Vector2(col.bounds.extents.x, -col.bounds.extents.y);

            RaycastHit2D hitLeft = Physics2D.Raycast(edgeCheckLeft, Vector2.down, edgeCheckDistance, groundLayer);
            RaycastHit2D hitRight = Physics2D.Raycast(edgeCheckRight, Vector2.down, edgeCheckDistance, groundLayer);

            //check for direcction too when checking for edge
            int direction = robotinMovement.GetDirection();
            if (direction == -1)
            {
                IsAtEdge = !hitLeft && hitRight;
            }
            else if (direction == 1)
            {
                IsAtEdge = !hitRight && hitLeft;
            }
        }
        else
        {
            IsAtEdge = false;
        }
    }

    private void CheckForWall()
    {
        // Cast a ray horizontally in the direction of movement to check for walls
        Vector2 wallCheckOrigin = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(wallCheckOrigin, new Vector2(robotinMovement.GetDirection(), 0), wallCheckDistance, groundLayer);

        IsNearWall = hit;
        if (IsNearWall)
        {
            if (((1 << hit.collider.gameObject.layer) & noWallJumpLayer) != 0)
            {
                IsWallSliding = true;
            }
            else
            {
                if (rb.velocity.y < 0)
                {
                    IsWallSliding = true;
                }
                else
                {
                    IsWallSliding = false;
                }
            }
        }
        else
        {
            IsWallSliding = false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        // Left edge check
        Vector2 edgeCheckLeft = (Vector2)transform.position + new Vector2(-col.bounds.extents.x, -col.bounds.extents.y);
        Gizmos.DrawLine(edgeCheckLeft, edgeCheckLeft + Vector2.down * edgeCheckDistance);

        // Right edge check
        Vector2 edgeCheckRight = (Vector2)transform.position + new Vector2(col.bounds.extents.x, -col.bounds.extents.y);
        Gizmos.DrawLine(edgeCheckRight, edgeCheckRight + Vector2.down * edgeCheckDistance);

        // Draw wall check ray
        Vector2 wallCheckOrigin = (Vector2)transform.position;
        Gizmos.DrawLine(wallCheckOrigin, wallCheckOrigin + new Vector2(robotinMovement.GetDirection(), 0) * wallCheckDistance);
    }
#endif
}