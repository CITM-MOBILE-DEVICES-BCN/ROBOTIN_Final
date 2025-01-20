using UnityEngine;

public class RobotinCollision : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float wallSlideSpeed = 1f;
    [SerializeField] private float edgeCheckDistance = 0.1f;
    [SerializeField] public float downGravity = 5f;
    [SerializeField] private float upGravity = 4f;
    [SerializeField] private float apexGravity = 5f;
    [SerializeField] private float maxVerticalSpeed = 10f;
    [SerializeField] private float wallCheckDistance = 0.1f;
    [SerializeField] private float offsetWallCheck = 0.3f;
    [SerializeField] private float jumpGroundCheckDistance = 0.3f;
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
            if (rb.velocity.y < -1f)
            {
                rb.gravityScale = downGravity;
            }
            else if (rb.velocity.y > 1f)
            {
                rb.gravityScale = upGravity;
            }
            else
            {
                rb.gravityScale = apexGravity;
            }

            if (IsNearWall && rb.velocity.y > 0.5f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.98f);

                if (rb.velocity.y < 0.01f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                }
            }
        }

        CheckForEdge();
        CheckForWall();

        //clamp velocity y to avoid falling through the ground
        if (rb.velocity.y < -maxVerticalSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxVerticalSpeed);
            Debug.Log("Velocity clamped MINOR");
        }
    }

    private void CheckForEdge()
    {
        if (IsGrounded)
        {
            Vector2 edgeCheckLeft = (Vector2)transform.position + new Vector2(-col.bounds.extents.x, -col.bounds.extents.y);
            Vector2 edgeCheckRight = (Vector2)transform.position + new Vector2(col.bounds.extents.x, -col.bounds.extents.y);

            RaycastHit2D hitLeft = Physics2D.Raycast(edgeCheckLeft, Vector2.down, edgeCheckDistance, groundLayer);
            RaycastHit2D hitRight = Physics2D.Raycast(edgeCheckRight, Vector2.down, edgeCheckDistance, groundLayer);

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
        Vector2 wallCheckOrigin = (Vector2)transform.position;
        RaycastHit2D topWallCheck = Physics2D.Raycast(new (wallCheckOrigin.x , wallCheckOrigin.y + offsetWallCheck), new (robotinMovement.GetDirection(), 0), jumpGroundCheckDistance, groundLayer);
        RaycastHit2D middleWallCheck = Physics2D.Raycast(wallCheckOrigin, new (robotinMovement.GetDirection(), 0), wallCheckDistance, groundLayer);
        RaycastHit2D bottomWallCheck = Physics2D.Raycast(new (wallCheckOrigin.x, wallCheckOrigin.y - offsetWallCheck), new (robotinMovement.GetDirection(), 0), jumpGroundCheckDistance, groundLayer);

        IsNearWall = middleWallCheck || topWallCheck || bottomWallCheck;
        if (IsNearWall)
        {
            RaycastHit2D hit = middleWallCheck ? middleWallCheck : (topWallCheck ? topWallCheck : bottomWallCheck);

            if (((1 << hit.collider.gameObject.layer) & noWallJumpLayer) != 0)
            {
                IsWallSliding = true;
            }
            else
            {
                IsWallSliding = rb.velocity.y < 0;
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

        Vector2 edgeCheckLeft = (Vector2)transform.position + new Vector2(-col.bounds.extents.x, -col.bounds.extents.y);
        Gizmos.DrawLine(edgeCheckLeft, edgeCheckLeft + Vector2.down * edgeCheckDistance);

        Vector2 edgeCheckRight = (Vector2)transform.position + new Vector2(col.bounds.extents.x, -col.bounds.extents.y);
        Gizmos.DrawLine(edgeCheckRight, edgeCheckRight + Vector2.down * edgeCheckDistance);

        //wallcheck topmid and bot
        Vector2 wallCheckOrigin = (Vector2)transform.position;
        //Gizmos.DrawLine(wallCheckOrigin, wallCheckOrigin + new Vector2(robotinMovement.GetDirection(), 0) * wallCheckDistance);
        Gizmos.DrawLine(new Vector2(wallCheckOrigin.x, wallCheckOrigin.y + offsetWallCheck), new Vector2(wallCheckOrigin.x, wallCheckOrigin.y + offsetWallCheck) + new Vector2(robotinMovement.GetDirection(), 0) * wallCheckDistance);
        Gizmos.DrawLine(new Vector2(wallCheckOrigin.x, wallCheckOrigin.y - offsetWallCheck), new Vector2(wallCheckOrigin.x, wallCheckOrigin.y - offsetWallCheck) + new Vector2(robotinMovement.GetDirection(), 0) * wallCheckDistance);
    }
#endif
}