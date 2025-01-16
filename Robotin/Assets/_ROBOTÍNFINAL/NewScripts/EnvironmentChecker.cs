using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentChecker : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float rayRange = 5f;
    [SerializeField] private float rayOffsetX = 0.5f;

    public bool IsGrounded { get; private set; }
    public bool IsTouchingWall { get; private set; }
    public bool IsNearEdge { get; private set; }

    private void Update()
    {
        Vector3 position = transform.position;
        IsGrounded = CheckGround(position);
        IsTouchingWall = CheckWall(position);
        IsNearEdge = CheckEdge(position);
    }

    private bool CheckGround(Vector3 position)
    {
        Vector3 leftOrigin = new Vector3(position.x - rayOffsetX, position.y, position.z);
        Vector3 rightOrigin = new Vector3(position.x + rayOffsetX, position.y, position.z);

        RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, Vector2.down, rayRange, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, Vector2.down, rayRange, groundLayer);

        Debug.DrawRay(leftOrigin, Vector2.down * rayRange, Color.green);
        Debug.DrawRay(rightOrigin, Vector2.down * rayRange, Color.green);

        return leftHit.collider != null || rightHit.collider != null;
    }

    private bool CheckWall(Vector3 position)
    {
        Vector3 frontOrigin = new Vector3(position.x, position.y, position.z);

        RaycastHit2D frontHit = Physics2D.Raycast(frontOrigin, Vector2.left, rayRange, wallLayer);
        Debug.DrawRay(frontOrigin, Vector2.left * rayRange, Color.blue);

        return frontHit.collider != null;
    }

    private bool CheckEdge(Vector3 position)
    {
        Vector3 leftOrigin = new Vector3(position.x - rayOffsetX, position.y, position.z);
        Vector3 rightOrigin = new Vector3(position.x + rayOffsetX, position.y, position.z);

        RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, Vector2.down, rayRange, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, Vector2.down, rayRange, groundLayer);

        return leftHit.collider == null || rightHit.collider == null;
    }
}
